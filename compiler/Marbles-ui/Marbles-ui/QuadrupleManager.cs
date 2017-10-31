using Marbles.MemoryManagement;
using System;
using System.Collections.Generic;
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
        private static List<Quadruple> quadruples;

        /// <summary>
        /// Stores values referencing <code>Utilities.DataTypes</code>.
        /// </summary>
        private static Stack<SemanticCubeUtilities.DataTypes> typeStack;

        /// <summary>
        /// Stores values referencing <code>Utilities.Operators</code>.
        /// </summary>
        private static Stack<int> operandStack;

        /// <summary>
        /// Stores values referencing memory addresses in which the operators are stored.
        /// </summary>
        private static Stack<SemanticCubeUtilities.Operators> operatorStack;

        /// <summary>
        /// Stores line numbers (within the quadruples list) to which control will jump.
        /// </summary>
        private static Stack<int> jumpStack;

        /// <summary>
        /// Maintains the current quadruple line which we are processing.
        /// </summary>
        private static int counter = 0;

        /// <summary>
        /// Tells us whether we are currently in a function or not.
        /// </summary>
        private static bool inFunction = false;

        /// <summary>
        /// If inFunction is true, this value holds the ID of the function we are currently in.
        /// </summary>
        private static string functionId = "";

        /// <summary>
        /// An object of the last function that was called.
        /// </summary>
        private static Function LastFunctionCalled;

        /// <summary>
        /// Counter used to compare a called function's parameters against the actual called function's parameters
        /// </summary>
        private static int parameterCount = 0;

        /// <summary>
        /// Returns the list of all quadruples generated.
        /// </summary>
        /// <returns></returns>
        public static List<Quadruple> GetQuadruples()
        {
            return quadruples;
        }

        /// <summary>
        /// Add the memory address pointing to an operand to the operand stack
        /// and its data type to the type stack.
        /// </summary>
        /// <param name="operand"></param>
        /// <param name="type"></param>
        public static void AddOperand(int operand, SemanticCubeUtilities.DataTypes type)
        {
            operandStack.Push(operand);
            typeStack.Push(type);
        }

        /// <summary>
        /// Add the memory address pointing to a constant to the operand stack
        /// and its data type to the type stack.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public static void AddConstant(int constant, SemanticCubeUtilities.DataTypes type)
        {
            operandStack.Push(constant);
            typeStack.Push(type);
        }

        /// <summary>
        /// Push an operator to the operator stack.
        /// </summary>
        /// <param name="op"></param>
        public static void AddOperator(SemanticCubeUtilities.Operators op)
        {
            operatorStack.Push(op);
        }

        /// <summary>
        /// Pop an operator from the operator stack and two corresponding operands from the
        /// operand stack. Verify their compatibility with the Semantic Cube and push the resulting
        /// operand to the operand stack. Adds a quadruple with the operation that was verified.
        /// </summary>
        public static void PopOperator()
        {
            if (operatorStack.Count == 0)
            {
                throw new Exception("There are no operators left on the operator stack.");
            }

            SemanticCubeUtilities.Operators op = operatorStack.Pop();

            int operandOne = operandStack.Pop();
            SemanticCubeUtilities.DataTypes typeOne = typeStack.Pop();
            int operandTwo = operandStack.Pop();
            SemanticCubeUtilities.DataTypes typeTwo = typeStack.Pop();

            SemanticCubeUtilities.DataTypes resultingDataType = SemanticCube.AnalyzeSemantics(
                new TypeTypeOperator(typeOne, typeTwo, op));
                        
            if (resultingDataType == SemanticCubeUtilities.DataTypes.invalidDataType)
            {
                throw new Exception("Invalid operation: " + operandOne + " " + op + " " + operandTwo);
            }

            int addressTemp;
            if (resultingDataType == SemanticCubeUtilities.DataTypes.number)
            {
                addressTemp = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.temporary, SemanticCubeUtilities.DataTypes.number);
            }
            else if (resultingDataType == SemanticCubeUtilities.DataTypes.boolean)
            {
                addressTemp = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.temporary, SemanticCubeUtilities.DataTypes.boolean);
            }
            else
            {
                addressTemp = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.temporary, SemanticCubeUtilities.DataTypes.text);
            }
            
            AddOperand(addressTemp, resultingDataType);

            quadruples.Add(new Quadruple(Utilities.operatorToAction[op],
                operandOne, operandTwo, addressTemp));
        }

        /// <summary>
        /// This method is called when we read an open parenthesis. A fake bottom is added to
        /// mark the beginning of a new expression to evaluate.
        /// </summary>
        public static void AddParenthesis()
        {
            operatorStack.Push(SemanticCubeUtilities.Operators.fakeBottom);
        }

        /// <summary>
        /// This method is called when we read a closing parenthesis. Pops the opening
        /// parenthesis at the top of the operator stack. Throws an exception if the latter
        /// is not present.
        /// </summary>
        public static void PopParenthesis()
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
        /// This method is called when we have read the closing parenthesis of an IF expression.
        /// The function verifies that the expression is boolean and, if it is, sets the GOTOF
        /// in case it is false.
        /// </summary>
        public static void IfAfterCondition()
        {
            SemanticCubeUtilities.DataTypes compareType = typeStack.Pop();
            if (compareType != SemanticCubeUtilities.DataTypes.boolean)
            {
                throw new Exception("All conditions must be of boolean data type.");
            }

            int condition = operandStack.Pop(); // memory address where the boolean result is stored

            // we will have to set this jump's position at the next ELSE or ELSE-IF statement or at the end of the whole IF statement if none
            jumpStack.Push(counter);
            quadruples.Add(new Quadruple(Utilities.QuadrupleAction.GotoF, condition));

        }

        /// <summary>
        /// This method is called when the last block in the IF statement has been processed.
        /// Update the jump of the last IF block to jump to this position.
        /// </summary>
        public static void IfEnd()
        {
            int lastJump = jumpStack.Pop();
            quadruples[lastJump].SetAssignee(counter);
        }

        /// <summary>
        /// This method is called after a WHILE expression is detected but before we read
        /// its condition. This point is where we will jump to each time after the end of the loop.
        /// </summary>
        public static void WhileBeforeCondition()
        {
            jumpStack.Push(counter);
        }

        /// <summary>
        /// This method is called when we have read the closing parenthesis of a WHILE expression.
        /// The function verifies that the expression is boolean and, if it is, sets the GOTOF
        /// in case it is false.
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
            quadruples.Add(new Quadruple(Utilities.QuadrupleAction.GotoF, condition));
        }

        /// <summary>
        /// This method is called after the last instruction in the WHILE statement has been processed.
        /// Update the jump of the while condition to jump to this position.
        /// </summary>
        public static void WhileEnd()
        {
            int jumpToOnFalse = jumpStack.Pop();
            int jumpToBeginningOfWhile = jumpStack.Pop();

            // Jump back to where the WHILE condition is evaluated
            quadruples.Add(new Quadruple(Utilities.QuadrupleAction.Goto, -1, -1, jumpToBeginningOfWhile));

            // Update the jump of the while condition to jump here
            quadruples[jumpToOnFalse].SetAssignee(counter);
        }

        /// <summary>
        /// This method is called after a FOR expression is detected but before we read
        /// its condition. This point is where we will jump to each time after the end of the loop.
        /// </summary>
        public static void ForBeforeCondition()
        {
            jumpStack.Push(counter);
        }

        /// <summary>
        /// This method is called when we have read the closing parenthesis of a FOR expression.
        /// The function verifies that the expression is numeric and, if it is, sets the GOTOF
        /// in case it is less than or equal to 0. Finally, substracts one from the numeric expression
        /// in the FOR condition.
        /// </summary>
        public static void ForAfterCondition()
        {
            SemanticCubeUtilities.DataTypes forType = typeStack.Pop();
            if (forType != SemanticCubeUtilities.DataTypes.number)
            {
                throw new Exception("FOR loop conditions must contain a numeric data type.");
            }

            // we will have to set this jump's position at the end of the FOR statement
            jumpStack.Push(counter);

            int numericExpAddress = operandStack.Pop(); // memory address where the numeric expression's result is stored           
            int numericExpValue = (int)(MemoryManager.GetValueFromAddress(numericExpAddress));
            bool condition = numericExpValue > 0;

            int conditionMem = 0;
            if (inFunction)
            {
                conditionMem = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.temporary, SemanticCubeUtilities.DataTypes.boolean);
            }
            else
            {
                conditionMem = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.global, SemanticCubeUtilities.DataTypes.boolean);
            }

            MemoryManager.SetMemory(conditionMem, condition);
            quadruples.Add(new Quadruple(Utilities.QuadrupleAction.GotoF, conditionMem));

            // After checking the condition, we can safely substract one from the numeric expression on the FOR condition
            MemoryManager.SetMemory(numericExpAddress, numericExpValue - 1);
        }
        
        /// <summary>
        /// This method is called after every instruction inside the FOR loop is executed.
        /// Jumps back to before the FOR condition and sets the jump to here when the FOR
        /// condition evaluates to false.
        /// </summary>
        public static void ForEnd()
        {
            int jumpToOnFalse = jumpStack.Pop();
            int jumpToBeginningOfFor = jumpStack.Pop();

            // Jump back to where the FOR condition is evaluated
            quadruples.Add(new Quadruple(Utilities.QuadrupleAction.Goto, -1, -1, jumpToBeginningOfFor));
            
            quadruples[jumpToOnFalse].SetAssignee(counter);
        }

        public static void CreateFunctionBeforeParameters()
        {

        }

        public static void CallFunctionBeforeParameters(string functionId)
        {
            if (!FunctionDirectory.FunctionExists(functionId))
            {
                // make this a semantic error
                throw new Exception("Use of undeclared function " + functionId);
            }
            LastFunctionCalled = FunctionDirectory.GetFunction(functionId);
        }

        public static void CallFunctionOpeningParenthesis()
        {
            quadruples.Add(new Quadruple(Utilities.QuadrupleAction.era, LastFunctionCalled.GetFunctionSize(), -1, -1));
            parameterCount = 0;
        }

        /// <summary>
        /// Compare the current parameter's type against the called functions's parameter type.
        /// If types match, adds a new quadruple with PARAM action.
        /// </summary>
        public static void CallFunctionParameter()
        {
            SemanticCubeUtilities.DataTypes type = typeStack.Pop();
            if (type != LastFunctionCalled.GetParameters().Values.ToList()[parameterCount].GetDataType())
            {
                throw new Exception("Parameter types do not match.");
            }

            int param = operandStack.Pop();

            int paramAddress = MemoryManager.GetNextAvailable(MemoryManager.MemoryScope.local, SemanticCubeUtilities.DataTypes.number);
            MemoryManager.SetMemory(paramAddress, param);

            quadruples.Add(new Quadruple(Utilities.QuadrupleAction.param, param, paramAddress));
        }

        /// <summary>
        /// Increase the current parameter counter, since we are now expecting another parameter.
        /// </summary>
        public static void CallFunctionComma()
        {
            parameterCount++;
        }

        public static void CallFunctionClosingParenthesis()
        {
            if (parameterCount != LastFunctionCalled.GetParameters().Count - 1)
            {
                throw new Exception("Number of arguments on function call do not match.");
            }
        }

        public static void CallFunctionEnd()
        {
            int funcNameAddress = MemoryManager.GetNextAvailable(inFunction ? MemoryManager.MemoryScope.local : MemoryManager.MemoryScope.global, SemanticCubeUtilities.DataTypes.text);
            MemoryManager.SetMemory(funcNameAddress, LastFunctionCalled.GetName());
            quadruples.Add(new Quadruple(Utilities.QuadrupleAction.gosub, funcNameAddress , -1, -1));
        }


        /// <summary>
        /// Returns whether we are currently in a function or not.
        /// </summary>
        public static bool IsInFunction()
        {
            return inFunction;
        }

        /// <summary>
        /// Sets the value of InFunction to true, meaning we are now inside a function.
        /// </summary>
        public static void EnterFunction(string functionID)
        {
            inFunction = true;
            functionId = functionID;
        }

        /// <summary>
        /// Sets the value of InFunction to false, meaning we are now not inside a function.
        /// </summary>
        public static void ExitFunction()
        {
            inFunction = false;
            functionId = "";
        }

        /// <summary>
        /// Actual verification of an variables's ID existence. Returns the memory address of the
        /// variable found, -1 otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="idMemoryAddress"></param>
        /// <returns></returns>
        public static bool VerifyVariableIDExists(string id, out SemanticCubeUtilities.DataTypes type, out int idMemoryAddress)
        {
            type = SemanticCubeUtilities.DataTypes.invalidDataType;
            idMemoryAddress = -1;

            bool existsLocal = false, existsGlobal = false;

            if (inFunction)
            {
                Function current = FunctionDirectory.GetFunction(functionId);
                existsLocal = current.GetLocalVariables().ContainsKey(id);
                if (existsLocal)
                {
                    type = current.GetLocalVariables()[id].GetDataType();
                    idMemoryAddress = current.GetLocalVariables()[id].GetMemoryAddress();
                }
            }
            else
            {
                existsGlobal = FunctionDirectory.GlobalFunction().GetLocalVariables().ContainsKey(id);
                if (existsGlobal)
                {
                    type = FunctionDirectory.GlobalFunction().GetLocalVariables()[id].GetDataType();
                    idMemoryAddress = FunctionDirectory.GlobalFunction().GetLocalVariables()[id].GetMemoryAddress();
                }
            }

            return existsLocal || existsGlobal;
        }

        /// <summary>
        /// Actual verification of an asset's ID existence. Returns the memory address of the
        /// Asset found, -1 otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idMemoryAddress"></param>
        /// <returns></returns>
        public static bool VerifyAssetIDExists(string id, out int idMemoryAddress)
        {
            idMemoryAddress = -1;

            bool exists = FunctionDirectory.GlobalFunction().GetAssets().ContainsKey(id);
            if (exists)
            {
                idMemoryAddress = FunctionDirectory.GlobalFunction().GetAssets()[id].GetMemoryAddress();
            }

            return exists;
        }

        /// <summary>
        /// This method is called when we read an id and it is not followed by a decimal point (meaning it is a variable).
        /// Verifies that the variable exists either locally or globally and, if it exists, returns its memory address.
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

            AddOperand(idMemoryAddress, idType);
        }

        /// <summary>
        /// This method is called when we read an id followed by a decimal point (meaning it is an asset).
        /// Verifies that the Asset exists and, if it exists, returns its memory address.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idMemoryAddress"></param>
        public static void ReadIDAsset(string id, out int idMemoryAddress)
        {
            // When we read an asset ID, we have to verify that it is valid (existent)
            bool exists = VerifyAssetIDExists(id, out idMemoryAddress);
            if (!exists)
            {
                throw new Exception("Use of undeclared identifier.");
            }
        }

        /// <summary>
        /// Called after reading the expression in an assign instruction. Verifies that the assignation
        /// matches both data types and generates a new quadruple with this instruction.
        /// </summary>
        public static void AssignEnd()
        {
            int expressionResult = operandStack.Pop();
            SemanticCubeUtilities.DataTypes expressionType = typeStack.Pop();

            int assigneeID = operandStack.Pop();
            SemanticCubeUtilities.DataTypes assigneeType = typeStack.Pop();

            if (expressionType != assigneeType)
            {
                throw new Exception("Expected " + assigneeType + ". Found " + expressionType + ".");
            }

            quadruples.Add(new Quadruple(Utilities.QuadrupleAction.equals, expressionResult, assigneeID));
        }

        /// <summary>
        /// Called every time we read a constant number. We add it to memory (if not already present)
        /// and push it to the operand stack.
        /// </summary>
        /// <param name="num"></param>
        public static void ReadConstantNumber(int num)
        {
            // we read a constant number, so we add it to global constant memory
            int constMem = MemoryManager.AddConstantInt(num);

            // push it to the operand stack
            AddConstant(constMem, SemanticCubeUtilities.DataTypes.number);
        }

        /// <summary>
        /// Called every time we read a constant string. We add it to memory (if not already present)
        /// and push it to the operand stack.
        /// </summary>
        /// <param name="text"></param>
        public static void ReadConstantText(string text)
        {
            // we read a constant string, so we add it to global constant memory
            int constMem = MemoryManager.AddConstantString(text);

            // push it to the operand stack
            AddConstant(constMem, SemanticCubeUtilities.DataTypes.text);
        }

        /// <summary>
        /// Called every time we read a constant boolean. We add it to memory (if not already present)
        /// and push it to the operand stack.
        /// </summary>
        /// <param name="condition"></param>
        public static void ReadConstantBool(bool condition)
        {
            // we read a constant bool, so we add it to global constant memory
            int constMem = MemoryManager.AddConstantBool(condition);

            // push it to the operand stack
            AddConstant(constMem, SemanticCubeUtilities.DataTypes.boolean);
        }
    }
}
