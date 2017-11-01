using Marbles.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
    public class Quadruple
    {
        private Utilities.QuadrupleAction op;
        private int operandOne, operandTwo, assignee;

        public Quadruple(Utilities.QuadrupleAction op, int operandOne, int operandTwo, int assignee)
        {
            this.op = op;
            this.operandOne = operandOne;
            this.operandTwo = operandTwo;
            this.assignee = assignee;
        }

        public Quadruple(Utilities.QuadrupleAction op, int operandOne, int assignee)
        {
            this.op = op;
            this.operandOne = operandOne;
            this.operandTwo = -1;
            this.assignee = assignee;
        }

        public Quadruple(Utilities.QuadrupleAction op, int operandOne)
        {
            this.op = op;
            this.operandOne = operandOne;
            this.operandTwo = -1;
            this.assignee = -1;
        }

        public Quadruple(Utilities.QuadrupleAction op)
        {
            this.op = op;
            this.operandOne = -1;
            this.operandTwo = -1;
            this.assignee = -1;
        }

        public Utilities.QuadrupleAction GetOperator()
        {
            return op;
        }

        public int GetOperandOne()
        {
            return operandOne;
        }

        public int GetOperandTwo()
        {
            return operandTwo;
        }

        public int GetAssignee()
        {
            return assignee;
        }

        public void SetOperator(Utilities.QuadrupleAction op)
        {
            this.op = op;
        }

        public void SetOperandOne(int operandOne)
        {
            this.operandOne = operandOne;
        }

        public void SetOperandTwo(int operandTwo)
        {
            this.operandTwo = operandTwo;
        }

        public void SetAssignee(int assignee)
        {
            this.assignee = assignee;
        }

        public void Print()
        {
            Debug.WriteLine(op.ToString() + ", " + operandOne + ", " + operandTwo + ", " + assignee);
        }

        public void PrintValues()
        {
            Debug.WriteLine(op.ToString() + ", " + (operandOne == -1 ? "_" : MemoryManager.GetValueFromAddress(operandOne)) + ", " + (operandTwo == -1 ? "_" : MemoryManager.GetValueFromAddress(operandTwo)) + ", " + (assignee == -1 ? "_" : MemoryManager.GetValueFromAddress(assignee)));
        }
    }
}
