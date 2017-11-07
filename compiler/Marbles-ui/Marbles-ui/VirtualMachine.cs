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
        private static int savedMemoryPointer = 0;

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
                //TODO
                savedMemoryPointer = currentInstruction;
                // set currentInstruction = Function's quadrupleStart
                // question: how to get the function we are going to??
            }
            else if (action == Utilities.QuadrupleAction.param)
            {
                //TODO
            }
            else if (action == Utilities.QuadrupleAction.gosub)
            {
                //TODO
            }
            else if (action == Utilities.QuadrupleAction.retorno)
            {
                //TODO
                currentInstruction = savedMemoryPointer;
            }
            else if (action == Utilities.QuadrupleAction.endProc)
            {
                //TODO
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
        }
    }
}
