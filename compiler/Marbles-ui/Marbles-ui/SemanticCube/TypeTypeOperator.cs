using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
    /// <summary>
    /// Class that holds two <see cref="SemanticCubeUtilities.DataTypes"/> and a <see cref="SemanticCubeUtilities.Operators"/>.
    /// </summary>
	public class TypeTypeOperator
	{
		private SemanticCubeUtilities.DataTypes DataTypeOne;
		private SemanticCubeUtilities.DataTypes DataTypeTwo;
		private SemanticCubeUtilities.Operators Operator;

		public TypeTypeOperator(SemanticCubeUtilities.DataTypes DataTypeOne, SemanticCubeUtilities.DataTypes DataTypeTwo, SemanticCubeUtilities.Operators Operator)
		{
			this.DataTypeOne = DataTypeOne;
			this.DataTypeTwo = DataTypeTwo;
			this.Operator = Operator;
		}

        /// <summary>
        /// Returns <see cref="DataTypeOne"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="SemanticCubeUtilities.DataTypes"/> value.
        /// </returns>
		public SemanticCubeUtilities.DataTypes GetDataTypeOne()
		{
			return DataTypeOne;
		}

        /// <summary>
        /// Sets <see cref="DataTypeOne"/>'s value.
        /// </summary>
        /// <param name="DataType"></param>
		public void SetDataTypeOne(SemanticCubeUtilities.DataTypes DataType)
		{
			DataTypeOne = DataType;
		}

        /// <summary>
        /// Returns <see cref="DataTypeTwo"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="SemanticCubeUtilities.DataTypes"/> value.
        /// </returns>
		public SemanticCubeUtilities.DataTypes GetDataTypeTwo()
		{
			return DataTypeTwo;
		}

        /// <summary>
        /// Sets <see cref="DataTypeTwo"/>'s value.
        /// </summary>
        /// <param name="DataType"></param>
		public void SetDataTypeTwo(SemanticCubeUtilities.DataTypes DataType)
		{
			DataTypeTwo = DataType;
		}

        /// <summary>
        /// Returns <see cref="Operator"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="SemanticCubeUtilities.Operators"/> value.
        /// </returns>
		public SemanticCubeUtilities.Operators GetOperator()
		{
			return Operator;
		}

        /// <summary>
        /// Sets <see cref="Operator"/>'s value.
        /// </summary>
        /// <param name="Operator"></param>
		public void SetOperator(SemanticCubeUtilities.Operators Operator)
		{
			this.Operator = Operator;
		}

        /// <summary>
        /// Overrides <see cref="object.Equals(object)"/>'s definition. Two <see cref="TypeTypeOperator"/> objects are equal
        /// if the two hold the same <see cref="SemanticCubeUtilities.DataTypes"/> and the same <see cref="SemanticCubeUtilities.Operators"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// A boolean stating whether two <see cref="TypeTypeOperator"/> objects are equal or not.
        /// </returns>
		public override bool Equals(object obj)
		{
			TypeTypeOperator tto = obj as TypeTypeOperator;

			return ((tto.GetDataTypeOne() == DataTypeOne) &&
					(tto.GetDataTypeTwo() == DataTypeTwo) &&
					(tto.GetOperator() == Operator));
		}

        /// <summary>
        /// Generates a hash code given the values the current <see cref="TypeTypeOperator"/> object's properties.
        /// </summary>
        /// <returns>
        /// An integer value representing a hash value.
        /// </returns>
		public override int GetHashCode()
		{
			unchecked
			{
				int hash = (int)DataTypeOne * 397 + (int)DataTypeTwo * 607 + (int)Operator * 827;
				return hash;
			}
		}
	}
}
