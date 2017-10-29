using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
    public class Quadruple
    {
        private SemanticCubeUtilities.Operators op;
        private int operandOne, operandTwo, assignee;

        public Quadruple(SemanticCubeUtilities.Operators op, int operandOne, int operandTwo, int assignee)
        {
            this.op = op;
            this.operandOne = operandOne;
            this.operandTwo = operandTwo;
            this.assignee = assignee;
        }

        public Quadruple(SemanticCubeUtilities.Operators op, int operandOne, int assignee)
        {
            this.op = op;
            this.operandOne = operandOne;
            this.operandTwo = -1;
            this.assignee = assignee;
        }

        public Quadruple(SemanticCubeUtilities.Operators op, int operandOne)
        {
            this.op = op;
            this.operandOne = operandOne;
            this.operandTwo = -1;
            this.assignee = -1;
        }

        public Quadruple(SemanticCubeUtilities.Operators op)
        {
            this.op = op;
            this.operandOne = -1;
            this.operandTwo = -1;
            this.assignee = -1;
        }

        public SemanticCubeUtilities.Operators GetOperator()
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

        public void SetOperator(SemanticCubeUtilities.Operators op)
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
    }
}
