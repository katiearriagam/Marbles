using System;
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
        private static int savedInstructionPointer = 0;
        private static Stack<int> localMemoryAllocations = new Stack<int>();
        public static Function LastFunctionCalled; 

        public static void StartExecution()
        {
            quadruples = QuadrupleManager.GetQuadruples();
            while (!endExecution)
            {
                int instructionToExecute = currentInstruction;
                ExecuteInstruction();

                // increment current instruction counter only if we didn't do a jump
                if (instructionToExecute == currentInstruction)
                {
                    currentInstruction++;
                }
            }
        }

        private static async void ExecuteInstruction()
        {
            Quadruple quadruple = quadruples[currentInstruction];
            Utilities.QuadrupleAction action = quadruple.GetAction();

            if (action == Utilities.QuadrupleAction.plus)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                int num2 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());
                int result = num1 + num2;
                MemoryManager.SetMemory(quadruple.GetAssignee(), result);
            }
            else if (action == Utilities.QuadrupleAction.minus)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                int num2 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());
                int result = num1 - num2;
                MemoryManager.SetMemory(quadruple.GetAssignee(), result);
            }
            else if (action == Utilities.QuadrupleAction.multiply)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                int num2 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());
                int result = num1 * num2;
                MemoryManager.SetMemory(quadruple.GetAssignee(), result);
            }
            else if (action == Utilities.QuadrupleAction.divide)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                int num2 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());

                if (num2 == 0)
                {
                    throw new DivideByZeroException();
                }

                int result = num1 / num2;
                MemoryManager.SetMemory(quadruple.GetAssignee(), result);
            }
            else if (action == Utilities.QuadrupleAction.greaterThan)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                int num2 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());
                bool result = num1 > num2;
                MemoryManager.SetMemory(quadruple.GetAssignee(), result);
            }
            else if (action == Utilities.QuadrupleAction.lessThan)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                int num2 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());
                bool result = num1 < num2;
                MemoryManager.SetMemory(quadruple.GetAssignee(), result);
            }
            else if (action == Utilities.QuadrupleAction.greaterThanOrEqualTo)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                int num2 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());
                bool result = num1 >= num2;
                MemoryManager.SetMemory(quadruple.GetAssignee(), result);
            }
            else if (action == Utilities.QuadrupleAction.lessThanOrEqualTo)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                int num2 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());
                bool result = num1 <= num2;
                MemoryManager.SetMemory(quadruple.GetAssignee(), result);
            }
            else if (action == Utilities.QuadrupleAction.equalEqual)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                int num2 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());
                bool result = num1 == num2;
                MemoryManager.SetMemory(quadruple.GetAssignee(), result);
            }
            else if (action == Utilities.QuadrupleAction.notEqual)
            {
                int num1 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                int num2 = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());
                bool result = num1 != num2;
                MemoryManager.SetMemory(quadruple.GetAssignee(), result);
            }
            else if (action == Utilities.QuadrupleAction.and)
            {
                bool cond1 = (bool)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                bool cond2 = (bool)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());
                bool result = cond1 && cond2;
                MemoryManager.SetMemory(quadruple.GetAssignee(), result);
            }
            else if (action == Utilities.QuadrupleAction.or)
            {
                bool cond1 = (bool)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                bool cond2 = (bool)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());
                bool result = cond1 || cond2;
                MemoryManager.SetMemory(quadruple.GetAssignee(), result);
            }
            else if (action == Utilities.QuadrupleAction.equals)
            {
                MemoryManager.SetMemory(quadruple.GetAssignee(), MemoryManager.GetValueFromAddress(quadruple.GetOperandOne()));
            }
            else if (action == Utilities.QuadrupleAction.Goto)
            {
                currentInstruction = quadruple.GetAssignee();
            }
            else if (action == Utilities.QuadrupleAction.GotoV)
            {
                if ((bool)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne()))
                {
                    currentInstruction = quadruple.GetAssignee();
                }
            }
            else if (action == Utilities.QuadrupleAction.GotoF)
            {
                if (!((bool)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne())))
                {
                    currentInstruction = quadruple.GetAssignee();
                }
            }
            else if (action == Utilities.QuadrupleAction.era)
            {
                int functionSize = quadruple.GetOperandOne();
                int functionMemAddress = quadruple.GetOperandTwo();

                LastFunctionCalled = FunctionDirectory.GetFunctionWithAddress(functionMemAddress);

                // TODO: load memory from LastFunctionCalled to Global Local Memory

                MemoryManager.AllocateLocalMemory(functionSize); // this can throw - but VM caller will catch it

                localMemoryAllocations.Push(functionSize);
            }
            else if (action == Utilities.QuadrupleAction.param)
            {
                //TODO
            }
            else if (action == Utilities.QuadrupleAction.gosub)
            {
                savedInstructionPointer = currentInstruction;
                currentInstruction = quadruple.GetOperandOne();
            }
            else if (action == Utilities.QuadrupleAction.retorno)
            {
                //TODO
            }
            else if (action == Utilities.QuadrupleAction.endProc)
            {
                currentInstruction = savedInstructionPointer;

                if (localMemoryAllocations.Count == 0)
                {
                    throw new Exception("Trying to deallocate memory that was never allocated."); // this can throw - but VM caller will catch it
                }

                MemoryManager.DeallocateLocalMemory(localMemoryAllocations.Pop());
            }
            else if (action == Utilities.QuadrupleAction.set_position)
            {
                string assetID = (string)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                Asset caller = Utilities.FindAssetFromID(assetID);

                int x = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());
                int y = (int)MemoryManager.GetValueFromAddress(quadruple.GetAssignee());

                caller.SetPosition(x, y);
            }
            else if (action == Utilities.QuadrupleAction.move_x)
            {
                string assetID = (string)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                Asset caller = Utilities.FindAssetFromID(assetID);

                int displacement = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());

                await caller.MoveX(displacement);
            }
            else if (action == Utilities.QuadrupleAction.move_y)
            {
                string assetID = (string)MemoryManager.GetValueFromAddress(quadruple.GetOperandOne());
                Asset caller = Utilities.FindAssetFromID(assetID);

                int displacement = (int)MemoryManager.GetValueFromAddress(quadruple.GetOperandTwo());

                await caller.MoveX(displacement);
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
    }
}
