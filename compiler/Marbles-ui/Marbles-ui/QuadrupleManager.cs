using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
	public static class QuadrupleManager
	{
		/// <summary>
		/// Stores the list of quadruples generated.
		/// </summary>
		private static List<Quadruple> quadruples = new List<Quadruple>();

		/// <summary>
		/// Stores values referencing <code>Utilities.DataTypes</code>.
		/// </summary>
		private static Stack<SemanticCubeUtilities.DataTypes> typeStack = new Stack<SemanticCubeUtilities.DataTypes>();

        /// <summary>
        /// Stores values referencing memory addresses in which the operators are stored.
        /// </summary>
        private static Stack<int> operandStack = new Stack<int>();

        /// <summary>
        /// Stores values referencing <see cref="SemanticCubeUtilities.Operators"/>.
        /// </summary>
        private static Stack<SemanticCubeUtilities.Operators> operatorStack = new Stack<SemanticCubeUtilities.Operators>();

		/// <summary>
		/// Stores line numbers (within the quadruples list) to which control will jump.
		/// </summary>
		private static Stack<int> jumpStack = new Stack<int>();

		/// <summary>
		/// Maintains the current quadruple line which we are processing.
		/// </summary>
		private static int counter = 0;

		/// <summary>
		/// Tells whether we are currently in a function or not.
		/// </summary>
		private static bool inFunction = false;

		/// <summary>
		/// If inFunction is true, this value holds the ID of the function we are currently in.
		/// </summary>
		private static string functionId = "";

        /// <summary>
        /// Tells whether the function we are currently in has a return statement or not.
        /// </summary>
        private static bool hasReturn = false;

		/// <summary>
		/// An object of the last function that was called.
		/// </summary>
		private static Function LastFunctionCalled;

		/// <summary>
		/// An object of the last asset that was called.
		/// </summary>
		private static Asset LastAssetCalled;

		/// <summary>
		/// Counter used to compare a called function's parameters against the actual called function's parameters
		/// </summary>
		private static int parameterCount = 0;

        /// <summary>
        /// Boolean used to to determine if a function call is recursive
        /// </summary>
        private static bool recursive = false;

        /// <summary>
        /// Stack that stores the era quadruples for recursive calls
        /// </summary>
        public static Stack<int> recursiveCalls = new Stack<int>();

		/// <summary>
		/// Returns a list of all quadruples generated.
        /// Called by <see cref="VirtualMachine"/> before starting execution.
		/// </summary>
		/// <returns>A list of <see cref="Quadruple"/> objects.</returns>
		public static List<Quadruple> GetQuadruples()
		{
			return quadruples;
		}

		/// <summary>
		/// Add the memory address pointing to an operand to the operand stack
		/// and its <see cref="SemanticCubeUtilities.DataTypes"/> to the type stack.
        /// Called by <see cref="QuadrupleManager"/> when an operand is identified.
		/// </summary>
		/// <param name="operand"></param>
		/// <param name="type"></param>
		public static void PushOperand(int operand, SemanticCubeUtilities.DataTypes type)
		{
			operandStack.Push(operand);
			typeStack.Push(type);
		}

        /// <summary>
        /// Add the memory address pointing to a constant to the operand stack
        /// and its <see cref="SemanticCubeUtilities.DataTypes"/> to the type stack.
        /// Called by <see cref="QuadrupleManager"/> when a constant is identified.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public static void PushConstant(int constant, SemanticCubeUtilities.DataTypes type)
		{
            PushOperand(constant, type);
		}

		/// <summary>
		/// Push an operator to the operator stack.
        /// Called by <see cref="Parser"/> when an operator is identified.
		/// </summary>
		/// <param name="op"></param>
		public static void PushOperator(SemanticCubeUtilities.Operators op)
		{
			operatorStack.Push(op);
		}

		/// <summary>
		/// Pop an operator from the operator stack and two corresponding operands from the
		/// operand stack. Verify their compatibility with the Semantic Cube and push the resulting
		/// operand to the operand stack. Adds a quadruple with the operation that was verified.
        /// Called by <see cref="Parser"/> every time it checks for a pending operator on the operator stack.
		/// </summary>
		public static void PopOperator(int priority)
		{
			if (operatorStack.Count == 0)
			{
				// Since it's empty, there cannot be operators with the selected priority pending
				return;
			}

			SemanticCubeUtilities.Operators op = operatorStack.Peek();

			if (SemanticCubeUtilities.OperatorToPriority(op) != priority)
			{
				// This is not the operator we are expecting at this moment
				return;
			}
			operatorStack.Pop();

			SemanticCubeUtilities.DataTypes resultingDataType;
			int addressTemp = -1;

			// Check if we have a negative to apply to our next operand.
			// This is the only unary operator.
			if (op == SemanticCubeUtilities.Operators.negative)
			{
				int unaryOperand = operandStack.Pop();
				SemanticCubeUtilities.DataTypes typeUnary = typeStack.Pop();

				resultingDataType = SemanticCube.AnalyzeSemantics(
					new TypeTypeOperator(typeUnary, SemanticCubeUtilities.DataTypes.invalidDataType, op));

				if (resultingDataType == SemanticCubeUtilities.DataTypes.invalidDataType)
				{
					throw new Exception("Invalid operation: " + SemanticCubeUtilities.GetOperatorVisualRepresentation(op) +
						SemanticCubeUtilities.GetDataTypeFromType(MemoryManager.GetTypeFromAddress(unaryOperand)).ToString());
				}

				// At this point, the only acceptable Data Type is number
				if (resultingDataType == SemanticCubeUtilities.DataTypes.number)
				{
					if (inFunction)
					{
						addressTemp = LastFunctionCalled.memory.GetNextAvailable(FunctionMemory.FunctionMemoryScope.temporary, SemanticCubeUtilities.DataTypes.number);
					}
					else
					{
						addressTemp = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.temporary, SemanticCubeUtilities.DataTypes.number);
					}
					addressTemp = MemoryManager.SetMemory(addressTemp, 0);
				}

				PushOperand(addressTemp, resultingDataType);
				quadruples.Add(new Quadruple(Utilities.operatorToAction[op], unaryOperand, -1, addressTemp));
				counter++;
			}
			else // we have a binary operator
			{
				int operandTwo = operandStack.Pop();
				SemanticCubeUtilities.DataTypes typeTwo = typeStack.Pop();
				int operandOne = operandStack.Pop();
				SemanticCubeUtilities.DataTypes typeOne = typeStack.Pop();

				resultingDataType = SemanticCube.AnalyzeSemantics(
					new TypeTypeOperator(typeOne, typeTwo, op));


				if (resultingDataType == SemanticCubeUtilities.DataTypes.invalidDataType)
				{
					throw new Exception("Invalid operation: " + SemanticCubeUtilities.GetDataTypeFromType(MemoryManager.GetTypeFromAddress(operandOne)).ToString() 
								+ " " + SemanticCubeUtilities.GetOperatorVisualRepresentation(op) + " " + 
								SemanticCubeUtilities.GetDataTypeFromType(MemoryManager.GetTypeFromAddress(operandTwo)).ToString());
				}
				
				if (resultingDataType == SemanticCubeUtilities.DataTypes.number)
				{
					if (inFunction)
					{
						addressTemp = FunctionDirectory.GetFunction(functionId).memory.GetNextAvailable(FunctionMemory.FunctionMemoryScope.temporary, SemanticCubeUtilities.DataTypes.number);
                        addressTemp = FunctionDirectory.GetFunction(functionId).memory.SetMemory(addressTemp, 0);
                    }
					else
					{
						addressTemp = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.temporary, SemanticCubeUtilities.DataTypes.number);
                        addressTemp = MemoryManager.SetMemory(addressTemp, 0);
                    }
				}
				else if (resultingDataType == SemanticCubeUtilities.DataTypes.boolean)
				{
					if (inFunction)
					{
						addressTemp = FunctionDirectory.GetFunction(functionId).memory.GetNextAvailable(FunctionMemory.FunctionMemoryScope.temporary, SemanticCubeUtilities.DataTypes.boolean);
                        addressTemp = FunctionDirectory.GetFunction(functionId).memory.SetMemory(addressTemp, false);
                    }
                    else
					{
						addressTemp = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.temporary, SemanticCubeUtilities.DataTypes.boolean);
                        addressTemp = MemoryManager.SetMemory(addressTemp, false);
                    }
				}
				else
				{
					if (inFunction)
					{
						addressTemp = FunctionDirectory.GetFunction(functionId).memory.GetNextAvailable(FunctionMemory.FunctionMemoryScope.temporary, SemanticCubeUtilities.DataTypes.text);
                        addressTemp = FunctionDirectory.GetFunction(functionId).memory.SetMemory(addressTemp, "");
                    }
                    else
					{
						addressTemp = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.temporary, SemanticCubeUtilities.DataTypes.text);
                        addressTemp = MemoryManager.SetMemory(addressTemp, "");
                    }
				}

				PushOperand(addressTemp, resultingDataType);

				quadruples.Add(new Quadruple(Utilities.operatorToAction[op],
					operandOne, operandTwo, addressTemp));
				counter++;
			}
		}

        /// <summary>
        /// Adds a fake bottom to the operator stack to mark the beginning of a new expression to evaluate.
        /// Called when an open parenthesis is read.
        /// </summary>
        public static void PushFakeBottom()
		{
			operatorStack.Push(SemanticCubeUtilities.Operators.fakeBottom);
		}

        /// <summary>
        /// Pops the opening parenthesis at the top of the operator stack.
        /// Throws an exception if the latter is not present.
        /// Called when a closing parenthesis is read. 
        /// </summary>
        public static void PopFakeBottom()
		{
			if (operatorStack.Count == 0)
			{
				throw new Exception("There are no operators left on the operator stack.");
			}

			if (operatorStack.Peek() != SemanticCubeUtilities.Operators.fakeBottom)
			{
				throw new Exception("You cannot remove the fake bottom at this point.");
			}

			operatorStack.Pop();
		}

        /// <summary>
        /// This function verifies that the IF condition's expression is boolean and adds a
        /// new quadruple with a <see cref="Utilities.QuadrupleAction.GotoF"/> action.
        /// Called when the closing parenthesis of an IF expression is read.
        /// </summary>
        public static void IfAfterCondition()
		{
			SemanticCubeUtilities.DataTypes compareType = typeStack.Pop();
			if (compareType != SemanticCubeUtilities.DataTypes.boolean)
			{
				throw new Exception("All conditions must be of boolean data type.");
			}

			int condition = operandStack.Pop(); // memory address where the boolean result is stored

			// we will have to set this jump's position at the end of the whole IF statement
			jumpStack.Push(counter);
			quadruples.Add(new Quadruple(Utilities.QuadrupleAction.GotoF, condition, -1, -1));
			counter++;
		}

        /// <summary>
        /// Update the jump of the last IF block to jump to this position.
        /// Called when the last block in the IF statement has been processed.
        /// </summary>
        public static void IfEnd()
		{
			int lastJump = jumpStack.Pop();
			quadruples[lastJump].SetAssignee(counter);
		}

        /// <summary>
        /// Sets the point where control will jump to each time after the end of a While loop.
        /// Called after a WHILE expression is detected but before we read its condition.
        /// </summary>
        public static void WhileBeforeCondition()
		{
			jumpStack.Push(counter);
		}

        /// <summary>
        /// This function verifies that the expression is boolean and adds a
        /// new quadruple with a <see cref="Utilities.QuadrupleAction.GotoF"/> action.
        /// Called when we have read the closing parenthesis of a WHILE expression.
        /// </summary>
        public static void WhileAfterCondition()
		{
			SemanticCubeUtilities.DataTypes compareType = typeStack.Pop();
			if (compareType != SemanticCubeUtilities.DataTypes.boolean)
			{
				throw new Exception("All conditions must be of boolean data type.");
			}

			int condition = operandStack.Pop(); // memory address where the boolean result is stored

			// we will have to set this jump's position at the end of the WHILE statement
			jumpStack.Push(counter);
			quadruples.Add(new Quadruple(Utilities.QuadrupleAction.GotoF, condition, -1, -1));
			counter++;
		}

        /// <summary>
        /// Update the jump of the while condition to jump to this position.
        /// Called after the last instruction in the WHILE statement has been processed.
        /// </summary>
        public static void WhileEnd()
		{
			int jumpToOnFalse = jumpStack.Pop();
			int jumpToBeginningOfWhile = jumpStack.Pop();

			// Jump back to where the WHILE condition is evaluated
			quadruples.Add(new Quadruple(Utilities.QuadrupleAction.Goto, -1, -1, jumpToBeginningOfWhile));
			counter++;

			// Update the jump of the while condition to jump here
			quadruples[jumpToOnFalse].SetAssignee(counter);
		}

        /// <summary>
        /// The function verifies that the FOR condition's expression is numeric and sets the <see cref="Utilities.QuadrupleAction.GotoF"/>
        /// that compares the expression's value against zero. Finally, substracts one from the numeric expression.
        /// Called when we have read the closing parenthesis of a FOR expression.
        /// </summary>
        public static void ForAfterCondition()
		{
			SemanticCubeUtilities.DataTypes forType = typeStack.Pop();
			if (forType != SemanticCubeUtilities.DataTypes.number)
			{
				throw new Exception("FOR loop conditions must contain a numeric data type.");
			}

			int numericExpAddress = operandStack.Pop(); // memory address where the numeric expression's result is stored

            int tempAddressWithNumericExp = 0;

            if (inFunction)
            {
                tempAddressWithNumericExp = FunctionDirectory.GetFunction(functionId).memory.GetNextAvailable(FunctionMemory.FunctionMemoryScope.temporary, SemanticCubeUtilities.DataTypes.number);
                tempAddressWithNumericExp = FunctionDirectory.GetFunction(functionId).memory.SetMemory(tempAddressWithNumericExp, 0);
            }
            else
            {
                tempAddressWithNumericExp = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.temporary, SemanticCubeUtilities.DataTypes.number);
                tempAddressWithNumericExp = MemoryManager.SetMemory(tempAddressWithNumericExp, 0);
            }

            quadruples.Add(new Quadruple(Utilities.QuadrupleAction.equals, numericExpAddress, tempAddressWithNumericExp));
            counter++;

            // store the zero constant in memory
            int zeroMem = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.constant, SemanticCubeUtilities.DataTypes.number);
			try { zeroMem = MemoryManager.SetMemory(zeroMem, 0); }
			catch (Exception e) { throw new Exception(e.Message); }

			int conditionMem = 0;

            // store the resulting condition's boolean value in memory
			if (inFunction)
			{
				conditionMem = FunctionDirectory.GetFunction(functionId).memory.GetNextAvailable(FunctionMemory.FunctionMemoryScope.temporary, SemanticCubeUtilities.DataTypes.boolean);
				conditionMem = FunctionDirectory.GetFunction(functionId).memory.SetMemory(conditionMem, false);
			}
			else
			{
				conditionMem = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.temporary, SemanticCubeUtilities.DataTypes.boolean);
				conditionMem = MemoryManager.SetMemory(conditionMem, false);
			}

			// This is where we will jump back to at the end of every FOR loop iteration
			jumpStack.Push(counter);

			quadruples.Add(new Quadruple(Utilities.QuadrupleAction.greaterThan, tempAddressWithNumericExp, zeroMem, conditionMem));
			counter++;

            // we will have to set this jump's position at the end of the FOR statement
            jumpStack.Push(counter);

            quadruples.Add(new Quadruple(Utilities.QuadrupleAction.GotoF, conditionMem, -1, -1));
			counter++;

            int oneMem = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.constant, SemanticCubeUtilities.DataTypes.number);
            oneMem = MemoryManager.SetMemory(oneMem, 1);
            
			// After checking the condition, we can safely substract one from the numeric expression on the FOR condition
			quadruples.Add(new Quadruple(Utilities.QuadrupleAction.minus, tempAddressWithNumericExp, oneMem, tempAddressWithNumericExp));
			counter++;
		}

        /// <summary>
        /// Jumps back to where the FOR condition was compared against 0, and sets the jump
        /// to here when the FOR condition evaluates to false.
        /// Called after every instruction inside the FOR loop hs been executed.
        /// </summary>
        public static void ForEnd()
		{
			int jumpToOnFalse = jumpStack.Pop();
			int jumpToBeginningOfFor = jumpStack.Pop();

			// Jump back to where the FOR condition is evaluated
			quadruples.Add(new Quadruple(Utilities.QuadrupleAction.Goto, -1, -1, jumpToBeginningOfFor));
			counter++;

			quadruples[jumpToOnFalse].SetAssignee(counter);
		}

        /// <summary>
        /// Verifies that the current function being called exists.
        /// Sets the <see cref="recursive"/> flag to true if this call is being
        /// called within itself.
        /// Called after reading a function call's id.
        /// </summary>
        /// <param name="functionBeingCalledId"></param>
		public static void CallFunctionBeforeParameters(string functionBeingCalledId)
		{
			if (!FunctionDirectory.FunctionExists(functionBeingCalledId))
			{
				// make this a semantic error
				throw new Exception("Use of undeclared function " + functionBeingCalledId);
			}
			if (operatorStack.Count > 0 && operatorStack.Peek() == SemanticCubeUtilities.Operators.negative)
			{
				if (FunctionDirectory.GetFunction(functionBeingCalledId).GetReturnType() == SemanticCubeUtilities.DataTypes.text)
				{
					throw new Exception("Invalid operand: cannot apply negative to text.");
				}
				if (FunctionDirectory.GetFunction(functionBeingCalledId).GetReturnType() == SemanticCubeUtilities.DataTypes.boolean)
				{
					throw new Exception("Invalid operand: cannot apply negative to boolean.");
				}
				if (FunctionDirectory.GetFunction(functionBeingCalledId).GetReturnType() == SemanticCubeUtilities.DataTypes.invalidDataType)
				{
					throw new Exception("Invalid operand: cannot apply negative to an invalid data type.");
				}
			}
			LastFunctionCalled = FunctionDirectory.GetFunction(functionBeingCalledId);

            // if we are inside a function, and a function call matches the name
            // of such function, assume it's recursive
            if (functionId == LastFunctionCalled.GetName()) { recursive = true; }
        }

        /// <summary>
        /// Adds an ERA quadruple and resets the <see cref="parameterCount"/> to zero.
        /// If the call is recursive, increments the current level of recursion.
        /// Called before reading a function call's parameters.
        /// </summary>
		public static void CallFunctionOpeningParenthesis()
		{
            if (recursive)
            {
                quadruples.Add(new Quadruple(Utilities.QuadrupleAction.era, -1, LastFunctionCalled.GetLocation(), -1));
                recursiveCalls.Push(counter);
            }
            else
            {
                quadruples.Add(new Quadruple(Utilities.QuadrupleAction.era, LastFunctionCalled.GetFunctionSize(), LastFunctionCalled.GetLocation(), -1));
            }

            counter++;
			parameterCount = 0;
            PushFakeBottom();
        }

		/// <summary>
		/// Compare the current parameter's type against the called functions's parameter type.
		/// If types match, adds a new quadruple with PARAM action.
        /// Called after reading a new parameter in a call to a function.
		/// </summary>
		public static void CallFunctionParameter()
		{
			SemanticCubeUtilities.DataTypes type = typeStack.Pop();
            parameterCount++;
            if (parameterCount > LastFunctionCalled.GetParameters().Count)
            {
                throw new Exception("Number of arguments on function call does not match. Expecting " + LastFunctionCalled.GetParameters().Count + ".");
            }
            if (type != LastFunctionCalled.GetParameters()[parameterCount - 1].GetDataType())
			{
				throw new Exception("Parameter types do not match.");
			}

			int param = operandStack.Pop();

			// create a new parameter indicating what paremeter you are setting
			// and pass on the address of its value
			quadruples.Add(new Quadruple(Utilities.QuadrupleAction.param, param, parameterCount - 1));
			counter++;
        }
        
        /// <summary>
        /// Verifies that the amount of parameters provided in a function call matches
        /// the amount of parameters in that function's definition.
        /// Resets the <see cref="parameterCount"/> back to zero.
        /// Called after reading a call to function's paremeters.
        /// </summary>
		public static void CallFunctionClosingParenthesis()
		{
			if (parameterCount < LastFunctionCalled.GetParameters().Count)
			{
				throw new Exception("Number of arguments on function call does not match. Expecting " + LastFunctionCalled.GetParameters().Count + ".");
			}
            PopFakeBottom();
            parameterCount = 0;
		}

        /// <summary>
        /// Generates a GoSub quadruple and creates a temporary variable in which the call to function's
        /// result will be stored in memory. Resets the <see cref="recursive"/> flag to false.
        /// Called after reading the closing parenthesis of a call to a function.
        /// </summary>
		public static void CallFunctionEnd()
		{
			quadruples.Add(new Quadruple(Utilities.QuadrupleAction.gosub, LastFunctionCalled.GetQuadrupleStart(), -1, -1));
            counter++;

            int memAddressWhereFunctionLives = FunctionDirectory.GlobalFunction().GetGlobalVariables()[LastFunctionCalled.GetName()].GetMemoryAddress();
            int tempGlobal;
            if (inFunction)
            {
                tempGlobal = FunctionDirectory.GetFunction(functionId).memory.GetNextAvailable(FunctionMemory.FunctionMemoryScope.temporary, LastFunctionCalled.GetReturnType());
                FunctionDirectory.GetFunction(functionId).memory.SetMemory(tempGlobal, Utilities.GetDefaultValueFromType(LastFunctionCalled.GetReturnType()));
                quadruples.Add(new Quadruple(Utilities.QuadrupleAction.equals, memAddressWhereFunctionLives, -1, tempGlobal));
                counter++;
            }
            else
            {
                tempGlobal = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.temporary, LastFunctionCalled.GetReturnType());
                MemoryManager.SetMemory(tempGlobal, Utilities.GetDefaultValueFromType(LastFunctionCalled.GetReturnType()));
                quadruples.Add(new Quadruple(Utilities.QuadrupleAction.equals, memAddressWhereFunctionLives, -1, tempGlobal));
                counter++;
            }
            PushOperand(tempGlobal, LastFunctionCalled.GetReturnType());
            recursive = false;
		}

		/// <summary>
        /// Verifies that the current function's name being defined does not already exist.
        /// Separates a space in memory in which to store the function.
		/// Sets the value of InFunction to true, meaning we are now inside a function.
        /// Called by <see cref="Parser"/> when a function id is read.
		/// </summary>
		public static void EnterFunction(Function func)
		{
			inFunction = true;
			functionId = func.GetName();
            hasReturn = false;

			if (FunctionDirectory.FunctionExists(functionId))
			{
				throw new Exception("Function " + functionId + " has been previously defined.");
			}
			else
			{
                FunctionDirectory.InsertFunction(func);
                try
				{
					MemoryManager.AddFunctionAsGlobalVariable(func);
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
			}
		}

		/// <summary>
		/// Loads a parameter into a function's dictionary.
        /// Called by <see cref="Parser"/> when a parameter id is read.
		/// </summary>
		public static void CreateFunction_LoadParameter(string functionID, Variable var)
		{
			inFunction = true;
			functionId = functionID;
			if (FunctionDirectory.FunctionExists(functionId))
			{
				try
				{
					FunctionDirectory.GetFunction(functionID).AddParameter(var);
					int address = FunctionDirectory.GetFunction(functionID).memory.GetNextAvailable(FunctionMemory.FunctionMemoryScope.global, var.GetDataType());
					if (var.GetDataType() == SemanticCubeUtilities.DataTypes.number)
					{
						FunctionDirectory.GetFunction(functionID).memory.SetMemory(address, 0);
					}
					else if (var.GetDataType() == SemanticCubeUtilities.DataTypes.boolean)
					{
						FunctionDirectory.GetFunction(functionID).memory.SetMemory(address, false);
					}
					else if (var.GetDataType() == SemanticCubeUtilities.DataTypes.text)
					{
						FunctionDirectory.GetFunction(functionID).memory.SetMemory(address, "");
					}
					else
					{
						throw new Exception("Invalid data type for variable " + var.GetName());
					}
					var.SetMemoryAddress(address);
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
			}
			else
			{
				throw new Exception("Use of undeclared function " + functionId + ".");
			}
		}

		/// <summary>
		/// Loads a local variable into a function's dictionary.
        /// Called by <see cref="Parser"/> when a local variable's id is read.
		/// </summary>
		public static void CreateFunction_LoadLocalVariable(string functionID, Variable var)
		{
			inFunction = true;
			functionId = functionID;
			if (FunctionDirectory.FunctionExists(functionId))
			{
				try
				{
					FunctionDirectory.GetFunction(functionID).AddLocalVariable(var);
					int address = FunctionDirectory.GetFunction(functionID).memory.GetNextAvailable(FunctionMemory.FunctionMemoryScope.global, var.GetDataType());
					if (var.GetDataType() == SemanticCubeUtilities.DataTypes.number)
					{
						FunctionDirectory.GetFunction(functionID).memory.SetMemory(address, 0);
					}
					else if (var.GetDataType() == SemanticCubeUtilities.DataTypes.boolean)
					{
						FunctionDirectory.GetFunction(functionID).memory.SetMemory(address, false);
					}
					else if (var.GetDataType() == SemanticCubeUtilities.DataTypes.text)
					{
						FunctionDirectory.GetFunction(functionID).memory.SetMemory(address, "");
					}
					else
					{
						throw new Exception("Invalid data type for variable " + var.GetName());
					}
					var.SetMemoryAddress(address);
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
			}
			else
			{
                throw new Exception("Use of undeclared function " + functionId + ".");
            }
		}

        /// <summary>
        /// Sets the value of InFunction to false, meaning we are now not inside a function.
        /// Releases the local variable table of the function we exited, and generates a retorno quadruple.
        /// Called by <see cref="Parser"/> when all the instructions of a function definition have been read.
        /// </summary>
        public static void ExitFunction()
		{
            if (!hasReturn)
            {
                throw new Exception("Function must contain a return statement.");
            }

            // if the function was recursive, fill all pending era quadruples with size 
            while (recursiveCalls.Count > 0)
            {
                quadruples[recursiveCalls.Pop()].SetOperandOne(FunctionDirectory.GetFunction(functionId).GetFunctionSize());
            }

            FunctionDirectory.GetFunction(functionId).memory.PrintMemory(functionId);
            FunctionDirectory.GetFunction(functionId).ReleaseLocalVariables();
            quadruples.Add(new Quadruple(Utilities.QuadrupleAction.endProc, -1, -1, -1));
			counter++;
			inFunction = false;
			functionId = "";
		}

        /// <summary>
        /// Actual verification of an variables's ID existence.
        /// Called by <see cref="ReadIDVariable(string)"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="idMemoryAddress"></param>
        /// <returns>Returns the memory address of the variable found, or -1 otherwise.</returns>
        public static bool VerifyVariableIDExists(string id, out SemanticCubeUtilities.DataTypes type, out int idMemoryAddress)
		{
            // initialize parameters passed by reference
			type = SemanticCubeUtilities.DataTypes.invalidDataType;
			idMemoryAddress = -1;

			bool existsLocalVar = false, existsLocalParam = false, existsGlobal = false;

			if (inFunction)
			{
                // we have to search in the local variables table and in parameters table

				Function current = FunctionDirectory.GetFunction(functionId);

				existsLocalVar = current.GetLocalVariables().ContainsKey(id);
				if (existsLocalVar)
				{
					type = current.GetLocalVariables()[id].GetDataType();
					idMemoryAddress = current.GetLocalVariables()[id].GetMemoryAddress();
				}

				existsLocalParam = current.GetParameters().Any((p) => p.GetName() == id);
				if (existsLocalParam)
				{
					type = current.GetParameter(id).GetDataType();
					idMemoryAddress = current.GetParameter(id).GetMemoryAddress();
				}

                existsGlobal = FunctionDirectory.GlobalFunction().GetGlobalVariables().ContainsKey(id) && !FunctionDirectory.FunctionExists(id);
                if (existsGlobal)
                {
                    type = FunctionDirectory.GlobalFunction().GetGlobalVariables()[id].GetDataType();
                    idMemoryAddress = FunctionDirectory.GlobalFunction().GetGlobalVariables()[id].GetMemoryAddress();
                }
            }
			else
			{
                // we only have to search in global variables table
				existsGlobal = FunctionDirectory.GlobalFunction().GetGlobalVariables().ContainsKey(id) && !FunctionDirectory.FunctionExists(id);
				if (existsGlobal)
				{
					type = FunctionDirectory.GlobalFunction().GetGlobalVariables()[id].GetDataType();
					idMemoryAddress = FunctionDirectory.GlobalFunction().GetGlobalVariables()[id].GetMemoryAddress();
				}
			}

			return existsLocalVar || existsLocalParam || existsGlobal;
		}

		/// <summary>
		/// This method is called when we read an id and it is not followed by a point (meaning it is a variable).
		/// Verifies that the variable exists either locally or globally and, if it exists, returns its memory address.
        /// Called by <see cref="Parser"/> when an id is read.
		/// </summary>
		/// <param name="id"></param>
		public static void ReadIDVariable(string id)
		{
			// When we read a variable ID, we have to verify that it is valid (existent)
			SemanticCubeUtilities.DataTypes idType;
			int idMemoryAddress;
			bool exists = VerifyVariableIDExists(id, out idType, out idMemoryAddress);
			if (!exists)
			{
				throw new Exception("Use of undeclared identifier.");
			}
			else
			{
				if (operatorStack.Count > 0 && operatorStack.Peek() == SemanticCubeUtilities.Operators.negative)
				{
					if (idType == SemanticCubeUtilities.DataTypes.text)
					{
						throw new Exception("Invalid operand: cannot apply negative to text.");
					}
					else if (idType == SemanticCubeUtilities.DataTypes.boolean)
					{
						throw new Exception("Invalid operand: cannot apply negative to boolean.");
					}
					else if (idType == SemanticCubeUtilities.DataTypes.invalidDataType)
					{
						throw new Exception("Invalid operand: cannot apply negative to an invalid data type.");
					}
				}
			}

			PushOperand(idMemoryAddress, idType);
		}

        /// <summary>
        /// Verifies that an assignment matches its data types and generates a new quadruple with this instruction.
        /// Called by <see cref="Parser"/> after reading the expression in an assign instruction.
        /// </summary>
        public static void AssignEnd()
		{
			if (operandStack.Count < 2)
			{
				throw new Exception("Error in assignment");
			}

			int expressionResult = operandStack.Pop();
			SemanticCubeUtilities.DataTypes expressionType = typeStack.Pop();

			int assigneeID = operandStack.Pop();
			SemanticCubeUtilities.DataTypes assigneeType = typeStack.Pop();

			if (expressionType != assigneeType)
			{
				throw new Exception("Expected " + assigneeType + ". Found " + expressionType + ".");
			}

			quadruples.Add(new Quadruple(Utilities.QuadrupleAction.equals, expressionResult, assigneeID));
			counter++;
		}

		/// <summary>
		/// Called every time we read a constant number. We add it to memory (if not already present)
		/// and push it to the operand stack. Called by <see cref="Parser"/>.
		/// </summary>
		/// <param name="num"></param>
		public static void ReadConstantNumber(int num)
		{
			// we read a constant number, so we add it to global constant memory
			int constMem = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.constant, SemanticCubeUtilities.DataTypes.number);
			
			try {
				if (operatorStack.Count > 0 && operatorStack.Peek() == SemanticCubeUtilities.Operators.negative)
				{
					constMem = MemoryManager.SetMemory(constMem, num * -1);
				}
				else
				{
					constMem = MemoryManager.SetMemory(constMem, num);
				}
			}
			catch (Exception e) { throw new Exception(e.Message); }

			// push it to the operand stack
			PushConstant(constMem, SemanticCubeUtilities.DataTypes.number);
		}

		/// <summary>
		/// Called every time we read a constant string. We add it to memory (if not already present)
		/// and push it to the operand stack. Called by <see cref="Parser"/>.
		/// </summary>
		/// <param name="text"></param>
		public static void ReadConstantText(string text)
		{
			// we read a constant string, so we add it to global constant memory
			int constMem = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.constant, SemanticCubeUtilities.DataTypes.text);

			// gets the new memory of the constant or retrieves if it was already set
			try
			{
				if (operatorStack.Count > 0 && operatorStack.Peek() == SemanticCubeUtilities.Operators.negative)
				{
					throw new Exception("Invalid operand: cannot apply negative to text." );
				}
				else { constMem = MemoryManager.SetMemory(constMem, text); }
			}
			catch (Exception e) { throw new Exception(e.Message); }

			// push it to the operand stack
			PushConstant(constMem, SemanticCubeUtilities.DataTypes.text);
		}

		/// <summary>
		/// Called every time we read a constant boolean. We add it to memory (if not already present)
		/// and push it to the operand stack. Called by <see cref="Parser"/>.
		/// </summary>
		/// <param name="condition"></param>
		public static void ReadConstantBool(bool condition)
		{
			// we read a constant bool, so we add it to global constant memory
			int constMem = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.constant, SemanticCubeUtilities.DataTypes.boolean);

			try
			{
				if (operatorStack.Count > 0 && operatorStack.Peek() == SemanticCubeUtilities.Operators.negative)
				{
					throw new Exception("Invalid operand: cannot apply negative to boolean.");
				}
				constMem = MemoryManager.SetMemory(constMem, condition);
			}
			catch (Exception e) { throw new Exception(e.Message); }

			// push it to the operand stack
			PushConstant(constMem, SemanticCubeUtilities.DataTypes.boolean);
		}

		/// <summary>
        /// Adds a quadruple with a <see cref="Utilities.QuadrupleAction.stop"/> action.
		/// Called by <see cref="Parser"/> when a Stop instruction is read.
		/// </summary>
		public static void ReadStop()
		{
			quadruples.Add(new Quadruple(Utilities.QuadrupleAction.stop, -1, -1, -1));
			counter++;
		}

		/// <summary>
		/// Called by <see cref="Parser"/> when an asset's id is read.
        /// Verifies that the asset exists and stores its value in <see cref="LastAssetCalled"/> if it does.
		/// </summary>
		/// <param name="id"></param>
		public static void ReadAssetId(string id)
		{
			// verify an asset was given
			if (id == "")
			{
				throw new Exception("Expected an asset ID.");
			}
			// verify asset exists
			if (!FunctionDirectory.GlobalFunction().GetAssets().ContainsKey(id))
			{
				throw new Exception("Asset ID not found.");
			}
			else
			{
				LastAssetCalled = FunctionDirectory.GlobalFunction().GetAssets()[id];
			}
		}

        /// <summary>
        /// Based on the action read, retrieves all the parameters to pass to the action (if any) and
        /// adds a new quadruple with the corresponding action and the parameters retrieved.
        /// Called by <see cref="Parser"/> when an Asset action is read.
        /// </summary>
        /// <param name="action"></param>
		public static void DoBlock_ReadAssetAction(Utilities.AssetAction action)
		{
			// get the memory address of the asset
			int memoryAdressOfAsset = LastAssetCalled.GetMemoryAddress();

			// possible parameters
			int parameter1;
			int parameter2;

			switch (action)
			{
				case Utilities.AssetAction.spin:
					quadruples.Add(new Quadruple(Utilities.QuadrupleAction.spin, memoryAdressOfAsset, -1, -1));
					counter++;
					break;
				case Utilities.AssetAction.move_x:
					if (typeStack.Peek() == SemanticCubeUtilities.DataTypes.number)
					{
						parameter1 = operandStack.Pop();
                        typeStack.Pop();
					}
					else
					{
						throw new Exception("Expected a numeric value for move_x");
					}
					quadruples.Add(new Quadruple(Utilities.QuadrupleAction.move_x, memoryAdressOfAsset, parameter1, -1));
					counter++;
					break;
				case Utilities.AssetAction.move_y:
					if (typeStack.Peek() == SemanticCubeUtilities.DataTypes.number)
					{
						parameter1 = operandStack.Pop();
                        typeStack.Pop();
					}
					else
					{
						throw new Exception("Expected a numeric value for move_y");
					}
					quadruples.Add(new Quadruple(Utilities.QuadrupleAction.move_y, memoryAdressOfAsset, parameter1, -1));
					counter++;
					break;
				case Utilities.AssetAction.rotate:
					if (typeStack.Peek() == SemanticCubeUtilities.DataTypes.number)
					{
						parameter1 = operandStack.Pop();
                        typeStack.Pop();
					}
					else
					{
						throw new Exception("Expected a numeric value for rotate");
					}
					quadruples.Add(new Quadruple(Utilities.QuadrupleAction.rotate, memoryAdressOfAsset, parameter1, -1));
					counter++;
					break;
				case Utilities.AssetAction.set_position:
					if (typeStack.Peek() == SemanticCubeUtilities.DataTypes.number)
					{
						parameter2 = operandStack.Pop();
                        typeStack.Pop();
					}
					else
					{
						throw new Exception("Expected two numeric values for set_position");
					}

					if (typeStack.Peek() == SemanticCubeUtilities.DataTypes.number)
					{
						parameter1 = operandStack.Pop();
                        typeStack.Pop();
					}
					else
					{
						throw new Exception("Expected two numeric values for set_position");
					}
					quadruples.Add(new Quadruple(Utilities.QuadrupleAction.set_position, memoryAdressOfAsset, parameter1, parameter2));
					counter++;
					break;
			}
		}

        /// <summary>
        /// Adds the asset attribute's memory address to the operand stack.
        /// Called by <see cref="Parser"/> when an Asset attribute is read.
        /// </summary>
        /// <param name="attribute"></param>
		public static void ReadAssetAttribute(MemoryManager.AssetAttributes attribute)
		{
			// get the memory address of the asset
			int memoryAdressOfAsset = LastAssetCalled.GetMemoryAddress();

			if (operatorStack.Count > 0 && operatorStack.Peek() == SemanticCubeUtilities.Operators.negative)
			{
				if (attribute == MemoryManager.AssetAttributes.label || attribute == MemoryManager.AssetAttributes.id)
				{
					throw new Exception("Invalid operand: cannot apply negative to this asset attribute.");
				}
			}

			// add address to operands
			PushOperand(memoryAdressOfAsset + (int)attribute, MemoryManager.AttributeToType(attribute));
		}

        /// <summary>
        /// Verifies that the type of the returned expression matches the function's return type.
        /// Adds the result to the operand stack and adds a retorno quadruple.
        /// Called by <see cref="Parser"/> when the end of a return statement is read.
        /// </summary>
		public static void ReturnEnd()
		{
            hasReturn = true;
			if (functionId == "")
			{
				throw new Exception("Cannot add a return statement here.");
			}
			SemanticCubeUtilities.DataTypes type = typeStack.Peek();
            SemanticCubeUtilities.DataTypes expectedType = FunctionDirectory.GetFunction(functionId).GetReturnType();
            if (type != expectedType)
            {
                throw new Exception("Expected return type " + expectedType + ". Found " + type + ".");
            }

			int result = operandStack.Pop();
            typeStack.Pop();

			// generate quadruple
			quadruples.Add(new Quadruple(Utilities.QuadrupleAction.retorno, result, FunctionDirectory.GetFunction(functionId).GetLocation()));
			counter++;
		}

        /// <summary>
        /// Returns <see cref="counter"/>.
        /// Called by <see cref="Parser"/> to set the start quadruple of a function.
        /// </summary>
        /// <returns>The number of quadruples generated so far.</returns>
		public static int GetCounter()
        {
            return counter;
        }

        /// <summary>
        /// Adds a new quadruple to <see cref="quadruples"/>.
        /// Called by <see cref="Parser"/>.
        /// </summary>
        /// <param name="q"></param>
        public static void AddQuadruple(Quadruple q)
        {
            quadruples.Add(q);
            counter++;
        }

        /// <summary>
        /// Updates the jump of the first quadruple in the list.
        /// Called by <see cref="Parser"/>.
        /// </summary>
        public static void UpdateBeginQuadruple()
        {
            quadruples[0].SetAssignee(counter);
        }

        /// <summary>
        /// Prints <see cref="quadruples"/> to console.
        /// Called by <see cref="CodeView"/> after the quadruples have been generated.
        /// </summary>
        public static void PrintQuadruples()
        {
            Debug.WriteLine("---- QUADRUPLES START ----");
            int counterP = 0;
            foreach (Quadruple quad in quadruples)
            {
                Debug.Write(counterP++ + ". ");
                quad.Print();
            }
            Debug.WriteLine("---- QUADRUPLES END ----");
        }

        /// <summary>
        /// Resets all the properties of this class.
        /// Called by <see cref="CodeView"/> before compilation.
        /// </summary>
        public static void Reset()
        {
            quadruples.Clear();
            typeStack.Clear();
            operandStack.Clear();
            operatorStack.Clear();
            jumpStack.Clear();
            counter = 0;
            inFunction = false;
            functionId = "";
            LastFunctionCalled = null;
            parameterCount = 0;
        }
    }
}
