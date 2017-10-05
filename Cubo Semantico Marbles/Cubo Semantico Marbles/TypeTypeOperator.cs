using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubo_Semantico_Marbles
{
    class TypeTypeOperator
    {
        private Utilities.DataTypes DataTypeOne;
        private Utilities.DataTypes DataTypeTwo;
        private Utilities.Operators Operator;

        public TypeTypeOperator(Utilities.DataTypes DataTypeOne, Utilities.DataTypes DataTypeTwo, Utilities.Operators Operator)
        {
            this.DataTypeOne = DataTypeOne;
            this.DataTypeTwo = DataTypeTwo;
            this.Operator    = Operator;
        }

        public Utilities.DataTypes GetDataTypeOne() {
            return DataTypeOne;
        }

        public void SetDataTypeOne(Utilities.DataTypes DataType) {
            DataTypeOne = DataType;
        }

        public Utilities.DataTypes GetDataTypeTwo()
        {
            return DataTypeTwo;
        }

        public void SetDataTypeTwo(Utilities.DataTypes DataType)
        {
            DataTypeTwo = DataType;
        }

        public Utilities.Operators GetOperator()
        {
            return Operator;
        }

        public void SetOperator(Utilities.Operators Operator)
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
