﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
    public static class VirtualMachine
    {
        private static List<Quadruple> quadruples = new List<Quadruple>();
        private static int currentInstruction = 0;
        private static bool endExecution = false;
        private static Stack<int> savedInstructionPointer = new Stack<int>();
        private static Stack<int> localMemoryAllocations = new Stack<int>();
		private static Stack<Tuple<string, int>> CallStack = new Stack<Tuple<string, int>>(); 
        public static Function LastFunctionCalled; 

		/// <summary>
		/// Starts executing all quadruples until it reaches the end.
		/// </summary>
        public static async Task Execute()
        {
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
                int num1 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                int num2 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                bool result = num1 == num2;

                MemoryManager.SetMemory(MapAddressToLocalMemory(quadruple.GetAssignee()), result);
            }
            else if (action == Utilities.QuadrupleAction.notEqual)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandOne()));
                int num2 = (int)MemoryManager.GetValueFromAddress(MapAddressToLocalMemory(quadruple.GetOperandTwo()));

                bool result = num1 != num2;

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

                LastFunctionCalled = FunctionDirectory.GetFunctionWithAddress(functionMemAddress);

                // Indicates the address in local memory where the function is loaded.
                int functionAddressInMemory = MemoryManager.AllocateLocalMemory(LastFunctionCalled); // this can throw - but VM caller will catch it

                CallStack.Push(new Tuple<string, int>(LastFunctionCalled.GetName(), functionAddressInMemory));
            }
            else if (action == Utilities.QuadrupleAction.param)
            {
                var temp = CallStack.Pop();
                int paramValueAddress = MapAddressToLocalMemory(quadruple.GetOperandOne());
                CallStack.Push(temp);

                int paramIndex = quadruple.GetAssignee();
                Variable p = FunctionDirectory.GetFunction(CallStack.Peek().Item1).GetParameters()[paramIndex];
                int pAddr = p.GetMemoryAddress();
                int destinationAddress = MapAddressToLocalMemory(pAddr);

                MemoryManager.SetMemory(destinationAddress, MemoryManager.GetValueFromAddress(paramValueAddress));
            }
            else if (action == Utilities.QuadrupleAction.gosub)
            {
                savedInstructionPointer.Push(currentInstruction + 1);
                currentInstruction = quadruple.GetOperandOne();
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
            if (CallStack.Count == 0) throw new Exception("CallStack is empty");
            string s = CallStack.Peek().Item1;
            int sum = MemoryManager.FunctionMemoryToMemoryManager(CallStack.Peek().Item1, address);

            return CallStack.Peek().Item2 + sum; 
		}
    }
}
