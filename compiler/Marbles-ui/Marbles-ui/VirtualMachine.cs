using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
    /// <summary>
    /// This class carries out the execution of the program specified by the instructions on the
    /// intermediate code. Execution errors are detected in this step.
    /// </summary>
    public static class VirtualMachine
    {
        private static List<Quadruple> quadruples = new List<Quadruple>();
        public static int currentInstruction = 0;
		private static bool endExecution = false;
        private static Stack<int> savedInstructionPointer = new Stack<int>();
        private static Stack<int> localMemoryAllocations = new Stack<int>();
		private static Stack<Tuple<string, int>> CallStack = new Stack<Tuple<string, int>>(); 
        public static Stack<Function> LastFunctionCalled = new Stack<Function>();
        private static Stack<Tuple<string, int>> funcToAdd = new Stack<Tuple<string, int>>();

		/// <summary>
		/// Starts executing all quadruples until all of them have been processed.
		/// </summary>
        public static async Task Execute()
        {
			Utilities.vmExecuting = true;
			quadruples = QuadrupleManager.GetQuadruples();
            while (!endExecution)
            {
                int instructionToExecute = currentInstruction;
                await ExecuteInstruction();

                // increment current instruction counter only if we didn't do a jump
                if (instructionToExecute == currentInstruction)
                {
                    currentInstruction++;
                }
            }

			quadruples = new List<Quadruple>();
			currentInstruction = 0;
			endExecution = false;
			savedInstructionPointer = new Stack<int>();
			localMemoryAllocations = new Stack<int>();
			CallStack = new Stack<Tuple<string, int>>();
			LastFunctionCalled = null;
		}

		/// <summary>
		/// Executes the instruction at position currentInstruction.
        /// For instructions that involve animations, the animation is awaited until completed.
		/// </summary>
        private static async Task ExecuteInstruction()
        {
            Quadruple quadruple = quadruples[currentInstruction];
            Utilities.QuadrupleAction action = quadruple.GetAction();

            if (action == Utilities.QuadrupleAction.plus)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                int num2 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                int result = num1 + num2;

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), result);
            }
            else if (action == Utilities.QuadrupleAction.minus)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                int num2 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                int result = num1 - num2;

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), result);
            }
            else if (action == Utilities.QuadrupleAction.multiply)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                int num2 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                int result = num1 * num2;

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), result);
            }
			else if (action == Utilities.QuadrupleAction.negative)
			{
				int numMemAddress = quadruple.GetOperandOne();
				int num1 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(numMemAddress));
				if (numMemAddress < MemoryManager.lowestConstantIntAddress)
				{
					num1 *= -1;
				}

				MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), num1);
			}
			else if (action == Utilities.QuadrupleAction.divide)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                int num2 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                if (num2 == 0)
                {
                    throw new DivideByZeroException();
                }

                int result = num1 / num2;

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), result);
            }
            else if (action == Utilities.QuadrupleAction.greaterThan)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                int num2 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                bool result = num1 > num2;

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), result);
            }
            else if (action == Utilities.QuadrupleAction.lessThan)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                int num2 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                bool result = num1 < num2;

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), result);
            }
            else if (action == Utilities.QuadrupleAction.greaterThanOrEqualTo)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                int num2 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                bool result = num1 >= num2;

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), result);
            }
            else if (action == Utilities.QuadrupleAction.lessThanOrEqualTo)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                int num2 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                bool result = num1 <= num2;

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), result);
            }
            else if (action == Utilities.QuadrupleAction.equalEqual)
            {
                var num1 = MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                var num2 = MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                bool result = ((IComparable)num1).CompareTo((IComparable)num2) == 0;

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), result);
            }
            else if (action == Utilities.QuadrupleAction.notEqual)
            {
                var num1 = MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                var num2 = MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                bool result = ((IComparable)num1).CompareTo((IComparable)num2) != 0;

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), result);
            }
            else if (action == Utilities.QuadrupleAction.and)
            {
                bool cond1 = (bool)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                bool cond2 = (bool)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                bool result = cond1 && cond2;

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), result);
            }
            else if (action == Utilities.QuadrupleAction.or)
            {
                bool cond1 = (bool)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                bool cond2 = (bool)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                bool result = cond1 || cond2;

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), result);
            }
            else if (action == Utilities.QuadrupleAction.equals)
            {
                int cc = currentInstruction;
                int targetAddress = quadruple.GetAssignee();
                object value = MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                if (targetAddress < 1000) // assignment was to an asset attribute
                {
                    int assetAttributesCount = Enum.GetNames(typeof(MemoryManager.AssetAttributes)).Length;
                    string assetID = (string)MemoryManager.GetValueFromAddress(targetAddress - (targetAddress % assetAttributesCount));
                    Asset caller = Utilities.FindAssetFromID(assetID);

                    if (targetAddress % assetAttributesCount == (int)MemoryManager.AssetAttributes.width)
                    {
                        await caller.SetWidth((int)value);
                    }
                    else if (targetAddress % assetAttributesCount == (int)MemoryManager.AssetAttributes.height)
                    {
                        await caller.SetHeight((int)value);
                    }
                    else if (targetAddress % assetAttributesCount == (int)MemoryManager.AssetAttributes.x)
                    {
                        await caller.SetPositionXAttribute((int)value);
                    }
                    else if (targetAddress % assetAttributesCount == (int)MemoryManager.AssetAttributes.y)
                    {
                        await caller.SetPositionYAttribute((int)value);
                    }
                    else if (targetAddress % assetAttributesCount == (int)MemoryManager.AssetAttributes.number)
                    {
                        await caller.SetNumber((int)value);
                    }
                    else if (targetAddress % assetAttributesCount == (int)MemoryManager.AssetAttributes.label)
                    {
                        await caller.SetLabel((string)value);
                    }
                }

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), value);
            }
            else if (action == Utilities.QuadrupleAction.Goto)
            {
                currentInstruction = quadruple.GetAssignee();
            }
            else if (action == Utilities.QuadrupleAction.GotoV)
            {
                if ((bool)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne())))
                {
                    currentInstruction = quadruple.GetAssignee();
                }
            }
            else if (action == Utilities.QuadrupleAction.GotoF)
            {
                if (!((bool)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()))))
                {
                    currentInstruction = quadruple.GetAssignee();
                }
            }
            else if (action == Utilities.QuadrupleAction.era)
            {
                int functionSize = quadruple.GetOperandOne();
                int functionMemAddress = quadruple.GetOperandTwo();

                LastFunctionCalled.Push(FunctionDirectory.GetFunctionWithAddress(functionMemAddress));

                // Indicates the address in local memory where the function is loaded.
                int functionAddressInMemory = MemoryManager.AllocateLocalMemory(LastFunctionCalled.Peek()); // this can throw - but VM caller will catch it
                funcToAdd.Push(new Tuple<string, int>(LastFunctionCalled.Peek().GetName(), functionAddressInMemory));
            }
            else if (action == Utilities.QuadrupleAction.param)
            {
                int paramValueAddress = MapAddressToLocalMemory(quadruple.GetOperandOne());
                
                int paramIndex = quadruple.GetAssignee();
                int pAddr = FunctionDirectory.GetFunction(funcToAdd.Peek().Item1).GetParameters()[paramIndex].GetMemoryAddress();

                CallStack.Push(funcToAdd.Peek());

                int destinationAddress = MapAddressToLocalMemory(pAddr);

                CallStack.Pop();

                MemoryManager.SetMemory(destinationAddress, MemoryManager.GetValueFromAddress(paramValueAddress));
            }
            else if (action == Utilities.QuadrupleAction.gosub)
            {
                savedInstructionPointer.Push(currentInstruction + 1);
                currentInstruction = quadruple.GetOperandOne();

                CallStack.Push(funcToAdd.Peek());
                LastFunctionCalled.Pop();
                funcToAdd.Pop();
            }
            else if (action == Utilities.QuadrupleAction.retorno)
            {
              int localResultValueAddress = MapAddressToLocalMemory(quadruple.GetOperandOne());
              int functionAddressInGlobalMemory = quadruple.GetAssignee();

              object resultValue = MemoryManager.GetValueFromAddress(localResultValueAddress);

              // save result in global memory
              try { MemoryManager.SetMemory(functionAddressInGlobalMemory, resultValue); }
              catch (Exception e) { throw new Exception(e.Message); }

              currentInstruction = savedInstructionPointer.Pop();

              if (CallStack.Count == 0)
              {
                throw new Exception("Call stack is empty.");
              }

              MemoryManager.DeallocateLocalMemory(FunctionDirectory.GetFunction(CallStack.Pop().Item1).GetFunctionSize());
            }
            else if (action == Utilities.QuadrupleAction.endProc)
            {
                currentInstruction = savedInstructionPointer.Pop();

                if (CallStack.Count == 0)
                {
                    throw new Exception("Call stack is empty.");
                }

                MemoryManager.DeallocateLocalMemory(FunctionDirectory.GetFunction(CallStack.Pop().Item1).GetFunctionSize());
				throw new Exception("Function codepath did not contain return statement.");
            }
            else if (action == Utilities.QuadrupleAction.set_position)
            {
                string assetID = (string)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                Asset caller = Utilities.FindAssetFromID(assetID);

                int x = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());
                int y = (int)MemoryManager.GetValueFromAddress(quadruple.GetAssignee());

                await caller.SetPosition(x, y);

                int xAttrAddress = quadruple.GetOperandOne() + (int)MemoryManager.AssetAttributes.x;
                MemoryManager.SetMemory(xAttrAddress, x);

                int yAttrAddress = quadruple.GetOperandOne() + (int)MemoryManager.AssetAttributes.y;
                MemoryManager.SetMemory(yAttrAddress, y);
              }
            else if (action == Utilities.QuadrupleAction.move_x)
            {
                string assetID = (string)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                Asset caller = Utilities.FindAssetFromID(assetID);

                int displacement = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());

                await caller.MoveX(displacement);

                int xAttrAddress = quadruple.GetOperandOne() + (int)MemoryManager.AssetAttributes.x;
                MemoryManager.SetMemory(xAttrAddress, (int)MemoryManager.GetValueFromAddress(xAttrAddress) + displacement);
                    }
                    else if (action == Utilities.QuadrupleAction.move_y)
                    {
                        string assetID = (string)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                        Asset caller = Utilities.FindAssetFromID(assetID);

                        int displacement = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());

                        await caller.MoveY(displacement);

                int yAttrAddress = quadruple.GetOperandOne() + (int)MemoryManager.AssetAttributes.y;
                MemoryManager.SetMemory(yAttrAddress, (int)MemoryManager.GetValueFromAddress(yAttrAddress) + displacement);
              }
            else if (action == Utilities.QuadrupleAction.spin)
            {
                string assetID = (string)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                Asset caller = Utilities.FindAssetFromID(assetID);

                await caller.Spin();
            }
            else if (action == Utilities.QuadrupleAction.rotate)
            {
                string assetID = (string)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                Asset caller = Utilities.FindAssetFromID(assetID);

                int degrees = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());

                await caller.Turn(degrees);

				int rotationAttrAddress = quadruple.GetOperandOne() + (int)MemoryManager.AssetAttributes.rotation;
				MemoryManager.SetMemory(rotationAttrAddress, ((int)MemoryManager.GetValueFromAddress(rotationAttrAddress) + degrees) % 360);
			}
            else if (action == Utilities.QuadrupleAction.stop)
            {
                // Go to the end of execution
                currentInstruction = quadruples.Count - 1;
            }
            else if (action == Utilities.QuadrupleAction.end)
            {
                // Finish execution
                endExecution = true;
            }
            else
            {
                throw new Exception("Invalid quadruple action: " + action.ToString());
            }
        }

		/// <summary>
		/// Given a memory address belonging to a function, returns its corresponding
		/// memory address in the Local section of global scope memory.
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		public static int MapAddressToLocalMemory(int address)
		{
			if (address < 100000) { return address; }
            string s;
            int addr;
            if (CallStack.Count > 0)
            {
                s = CallStack.Peek().Item1;
                addr = CallStack.Peek().Item2;
            }
            else
            {
                s = funcToAdd.Peek().Item1;
                addr = funcToAdd.Peek().Item2;
            }

            int sum = MemoryManager.FunctionMemoryToMemoryManager(s, address);

            return addr + sum;
		}
    }
}
