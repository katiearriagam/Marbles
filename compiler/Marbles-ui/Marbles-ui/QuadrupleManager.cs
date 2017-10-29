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
        private Stack<SemanticCubeUtilities.DataTypes> typeStack;

        /// <summary>
        /// Stores values referencing <code>Utilities.Operators</code>.
        /// </summary>
        private Stack<int> operandStack;

        /// <summary>
        /// Stores values referencing memory addresses in which the operators are stored.
        /// </summary>
        private Stack<SemanticCubeUtilities.Operators> operatorStack;

        /// <summary>
        /// Stores line numbers (within the quadruples list) to which control will jump.
        /// </summary>
        private Stack<int> jumpStack;

        /// <summary>
        /// Maintains the current quadruple line which we are processing.
        /// </summary>
        private int counter = 0;

        /// <summary>
        /// Tells us whether we are currently in a function or not.
        /// </summary>
        private bool inFunction = false;

        /// <summary>
        /// Add the memory address pointing to an operand to the operand stack
        /// and its data type to the type stack.
        /// </summary>
        /// <param name="operand"></param>
        /// <param name="type"></param>
        public void AddOperand(int operand, SemanticCubeUtilities.DataTypes type)
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
        public void AddConstant(int constant, SemanticCubeUtilities.DataTypes type)
        {
            operandStack.Push(constant);
            typeStack.Push(type);
        }

        /// <summary>
        /// Push an operator to the operator stack.
        /// </summary>
        /// <param name="op"></param>
        public void AddOperator(SemanticCubeUtilities.Operators op)
        {
            operatorStack.Push(op);
        }

        public void PopOperator()
        {
            if (operatorStack.Count == 0)
            {
                throw new Exception("There are no operators left on the operator stack.");
            }

            SemanticCubeUtilities.Operators op = operatorStack.Pop();

            // TO-DO
            // pop 1 operand
            // check if unary or binary operator
            // if binary, pop another operand
            // check in semantic cube if types are valid
            // push new quadruple and result to stacks
        }

        /// <summary>
        /// This method is called when we read an open parenthesis. A fake bottom is added to
        /// mark the beginning of a new expression to evaluate.
        /// </summary>
        public void AddParenthesis()
        {
            operatorStack.Push(SemanticCubeUtilities.Operators.fakeBottom);
        }

        /// <summary>
        /// This method is called when we read a closing parenthesis. Pops the opening
        /// parenthesis at the top of the operator stack. Throws an exception if the latter
        /// is not present.
        /// </summary>
        public void PopParenthesis()
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
        public void IfAfterCondition()
        {
            SemanticCubeUtilities.DataTypes compareType = typeStack.Pop();
            if (compareType != SemanticCubeUtilities.DataTypes.boolean)
            {
                throw new Exception("All conditions must be of boolean data type.");
            }

            int condition = operandStack.Pop(); // memory address where the boolean result is stored

            // we will have to set this jump's position at the next ELSE or ELSE-IF statement or at the end of the whole IF statement if none
            jumpStack.Push(counter);
            quadruples.Add(new Quadruple(SemanticCubeUtilities.Operators.GotoF, condition));

        }

        /// <summary>
        /// This method is called when we detect an ELSE or an ELSE-IF expression.
        /// Set the jump of the latest IF or ELSE-IF condition to jump here, and add a
        /// GOTO quadruple at the end of this block to go to the end of the IF.
        /// </summary>
        public void IfElse()
        {
            int latestJump = jumpStack.Pop();
            quadruples[latestJump].SetAssignee(counter + 1);

            // we will have to set this jump's position at the next ELSE or ELSE-IF statement or at the end of the whole IF statement if none
            jumpStack.Push(counter);
            quadruples.Add(new Quadruple(SemanticCubeUtilities.Operators.Goto));
        }

        /// <summary>
        /// This method is called when the last block in the IF statement has been processed.
        /// Update the jump of the last IF block to jump to this position.
        /// </summary>
        public void IfEnd()
        {
            int lastJump = jumpStack.Pop();
            quadruples[lastJump].SetAssignee(counter);
        }

        /// <summary>
        /// This method is called after a WHILE expression is detected but before we read
        /// its condition. This point is where we will jump to each time after the end of the loop.
        /// </summary>
        public void WhileBeforeCondition()
        {
            jumpStack.Push(counter);
        }

        /// <summary>
        /// This method is called when we have read the closing parenthesis of a WHILE expression.
        /// The function verifies that the expression is boolean and, if it is, sets the GOTOF
        /// in case it is false.
        /// </summary>
        public void WhileAfterCondition()
        {
            SemanticCubeUtilities.DataTypes compareType = typeStack.Pop();
            if (compareType != SemanticCubeUtilities.DataTypes.boolean)
            {
                throw new Exception("All conditions must be of boolean data type.");
            }

            int condition = operandStack.Pop(); // memory address where the boolean result is stored

            // we will have to set this jump's position at the end of the WHILE statement
            jumpStack.Push(counter);
            quadruples.Add(new Quadruple(SemanticCubeUtilities.Operators.GotoF, condition));
        }

        /// <summary>
        /// This method is called after the last instruction in the WHILE statement has been processed.
        /// Update the jump of the while condition to jump to this position.
        /// </summary>
        public void WhileEnd()
        {
            int GotoFToUpdate = jumpStack.Pop();
            int jumpTo = jumpStack.Pop();

            // Jump back to where the WHILE condition is evaluated
            quadruples.Add(new Quadruple(SemanticCubeUtilities.Operators.Goto, -1, -1, jumpTo));

            // Update the jump of the while condition to jump here
            quadruples[GotoFToUpdate].SetAssignee(counter);
        }

        /// <summary>
        /// This method is called after a FOR expression is detected but before we read
        /// its condition. This point is where we will jump to each time after the end of the loop.
        /// </summary>
        public void ForBeforeCondition()
        {
            jumpStack.Push(counter);
        }

        /// <summary>
        /// This method is called when we have read the closing parenthesis of a FOR expression.
        /// The function verifies that the expression is boolean and, if it is, sets the GOTOF
        /// in case it is false.
        /// </summary>
        public void ForAfterCondition()
        {
            SemanticCubeUtilities.DataTypes forType = typeStack.Pop();
            if (forType != SemanticCubeUtilities.DataTypes.number)
            {
                throw new Exception("FOR loop conditions must contain a numeric data type.");
            }

            int numericExp = operandStack.Pop(); // memory address where the numeric expression's result is stored           
            
            // we will have to set this jump's position at the end of the FOR statement
            jumpStack.Push(counter);

            // bool condition = MemoryManager.GetValueAtAddress(numericExp) > 0;

            int conditionMem = 0;
            if (inFunction)
            {
                // conditionMem = MemoryManager.GetNextMemoryAddressTempBoolean(condition, "local")
                // asumo que esto ^^^ la guarda en ese temp boolean address y me regresa su address
            }
            else
            {
                // conditionMem = MemoryManager.GetNextMemoryAddressTempBoolean(condition, "global")
                // asumo que esto ^^^ la guarda en ese temp boolean address y me regresa su address
            }

            quadruples.Add(new Quadruple(SemanticCubeUtilities.Operators.GotoF, conditionMem));
        }

        public void ForBegin()
        {

        }

        /// <summary>
        /// This method is called after every instruction inside the FOR loop is executed.
        /// Substracts one from the numeric expression we are evaluating on the for loop
        /// and
        /// </summary>
        public void ForEnd()
        {
            // agregar un quad que le reste 1 a la numericExp del FOR
            // luego hacer un jump de regreso al inicio del FOR (antes de evaluar la numericExp)


            // set the place where the FOR loop will jump to if its expression is <= 0
            int exitFor = jumpStack.Pop();
            quadruples[exitFor].SetAssignee(counter);
        }

        public bool GetInFunction()
        {
            return inFunction;
        }

        public void SetInFunction(bool inFunction)
        {
            this.inFunction = inFunction;
        }
    }
}
