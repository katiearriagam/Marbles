using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
    public class Quadruple
    {
        int op, operandOne, operandTwo, assignee;

        public Quadruple(int op, int operandOne, int operandTwo, int assignee)
        {
            this.op = op;
            this.operandOne = operandOne;
            this.operandTwo = operandTwo;
            this.assignee = assignee;
        }

        public Quadruple(int op, int operandOne, int assignee)
        {
            this.op = op;
            this.operandOne = operandOne;
            this.operandTwo = -1;
            this.assignee = assignee;
        }

        public Quadruple(int op, int operandOne)
        {
            this.op = op;
            this.operandOne = operandOne;
            this.operandTwo = -1;
            this.assignee = -1;
        }
    }
}
