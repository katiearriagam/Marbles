using System;

namespace Marbles
{
    /// <summary>
    /// This class defines what a variable is in Marbles.
    /// A variable has a data type associated with it and an id (name).
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// The variable's associated data type.
        /// </summary>
        private SemanticCubeUtilities.DataTypes dataType;

        /// <summary>
        /// The variable's identifier.
        /// </summary>
        private String name;

        /// <summary>
        /// Variable constructor. A variable must have a name and a data type.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dataType"></param>
        public Variable(String name, SemanticCubeUtilities.DataTypes dataType)
        {
            this.name = name;
            this.dataType = dataType;
        }

        /// <summary>
        /// Variable's data type getter method.
        /// </summary>
        public SemanticCubeUtilities.DataTypes GetDataType()
        {
            return dataType;
        }

        /// <summary>
        /// Variable's identifier (name) getter method.
        /// </summary>
        /// <returns></returns>
        public String GetName()
        {
            return name;
        }
    }
}
