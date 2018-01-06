using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
    /// <summary>
    /// A Quadruple consists of a <see cref="Utilities.QuadrupleAction"/> and three memory addresses, usually 
    /// belonging to two operands and one address where the result of the two operands with the given action
    /// will be stored.
    /// </summary>
    public class Quadruple
    {
        private Utilities.QuadrupleAction action;
        private int operandOne, operandTwo, assignee;

        public Quadruple(Utilities.QuadrupleAction action, int operandOne, int operandTwo, int assignee)
        {
            this.action = action;
            this.operandOne = operandOne;
            this.operandTwo = operandTwo;
            this.assignee = assignee;
        }

        public Quadruple(Utilities.QuadrupleAction action, int operandOne, int assignee)
        {
            this.action = action;
            this.operandOne = operandOne;
            this.operandTwo = -1;
            this.assignee = assignee;
        }

        /// <summary>
        /// Returns the <see cref="Utilities.QuadrupleAction"/> of the quadruple object.
        /// Called by <see cref="VirtualMachine"/> to get the action to execute.
        /// </summary>
        /// <returns>A <see cref="Utilities.QuadrupleAction"./></returns>
        public Utilities.QuadrupleAction GetAction()
        {
            return action;
        }

        /// <summary>
        /// Returns <see cref="operandOne"/>.
        /// </summary>
        /// <returns>A memory address representing an operand.</returns>
        public int GetOperandOne()
        {
            return operandOne;
        }

        /// <summary>
        /// Returns <see cref="operandTwo"/>.
        /// </summary>
        /// <returns>A memory address representing an operand.</returns>
        public int GetOperandTwo()
        {
            return operandTwo;
        }

        /// <summary>
        /// Returns <see cref="assignee"/>.
        /// </summary>
        /// <returns>A memory address where a result will be stored.</returns>
        public int GetAssignee()
        {
            return assignee;
        }

        /// <summary>
        /// Sets <see cref="action"/> to a <see cref="Utilities.QuadrupleAction"/>.
        /// </summary>
        /// <param name="action"></param>
        public void SetAction(Utilities.QuadrupleAction action)
        {
            this.action = action;
        }

        /// <summary>
        /// Sets <see cref="operandOne"/> to a memory address.
        /// </summary>
        /// <param name="operandOne"></param>
        public void SetOperandOne(int operandOne)
        {
            this.operandOne = operandOne;
        }

        /// <summary>
        /// Sets <see cref="operandTwo"/> to a memory address.
        /// </summary>
        /// <param name="operandTwo"></param>
        public void SetOperandTwo(int operandTwo)
        {
            this.operandTwo = operandTwo;
        }

        /// <summary>
        /// Sets <see cref="assignee"/> to a memory address.
        /// </summary>
        /// <param name="assignee"></param>
        public void SetAssignee(int assignee)
        {
            this.assignee = assignee;
        }

        /// <summary>
        /// Prints a single quadruple.
        /// Called by <see cref="QuadrupleManager.PrintQuadruples"/>.
        /// </summary>
        public void Print()
        {
            Debug.WriteLine(action.ToString() + ", " + operandOne + ", " + operandTwo + ", " + assignee);
        }
    }
}
