using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
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

		public SemanticCubeUtilities.DataTypes GetDataTypeOne()
		{
			return DataTypeOne;
		}

		public void SetDataTypeOne(SemanticCubeUtilities.DataTypes DataType)
		{
			DataTypeOne = DataType;
		}

		public SemanticCubeUtilities.DataTypes GetDataTypeTwo()
		{
			return DataTypeTwo;
		}

		public void SetDataTypeTwo(SemanticCubeUtilities.DataTypes DataType)
		{
			DataTypeTwo = DataType;
		}

		public SemanticCubeUtilities.Operators GetOperator()
		{
			return Operator;
		}

		public void SetOperator(SemanticCubeUtilities.Operators Operator)
		{
			this.Operator = Operator;
		}

		public override bool Equals(object obj)
		{
			TypeTypeOperator tto = obj as TypeTypeOperator;

			return ((tto.GetDataTypeOne() == DataTypeOne) &&
					(tto.GetDataTypeTwo() == DataTypeTwo) &&
					(tto.GetOperator() == Operator));
		}

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
