using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
    public class QuadrupleManager
    {
        /// <summary>
        /// Stores the list of quadruples generated.
        /// </summary>
        private List<Quadruple> quadruples;

        /// <summary>
        /// Stores values referencing <code>Utilities.DataTypes</code>.
        /// </summary>
        private Stack<Utilities.DataTypes> typeStack;

        /// <summary>
        /// Stores values referencing <code>Utilities.Operators</code>.
        /// </summary>
        private Stack<int> operandStack;

        /// <summary>
        /// Stores values referencing memory addresses in which the operators are stored.
        /// </summary>
        private Stack<Utilities.Operators> operatorStack;

        /// <summary>
        /// Stores line numbers (within the quadruples list) to which control will jump.
        /// </summary>
        private Stack<int> jumpStack;

        /// <summary>
        /// Maintains the current quadruple line which we are processing.
        /// </summary>
        private int counter = 0;

        /// <summary>
        /// Add the memory address pointing to an operand to the operand stack
        /// and its data type to the type stack.
        /// </summary>
        /// <param name="operand"></param>
        /// <param name="type"></param>
        public void AddOperand(int operand, Utilities.DataTypes type)
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
        public void AddConstant(int constant, Utilities.DataTypes type)
        {
            operandStack.Push(constant);
            typeStack.Push(type);
        }


    }
}
