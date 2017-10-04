using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubo_Semantico_Marbles
{
    class TypeTypeOperator
    {
        private Utilities.VarDataTypes DataTypeOne;
        private Utilities.VarDataTypes DataTypeTwo;
        private Utilities.Operators Operator;

        public TypeTypeOperator(Utilities.VarDataTypes DataTypeOne, Utilities.VarDataTypes DataTypeTwo, Utilities.Operators Operator)
        {
            this.DataTypeOne = DataTypeOne;
            this.DataTypeTwo = DataTypeTwo;
            this.Operator    = Operator;
        }

        public Utilities.VarDataTypes GetDataTypeOne() {
            return DataTypeOne;
        }

        public void SetDataTypeOne(Utilities.VarDataTypes DataType) {
            DataTypeOne = DataType;
        }

        public Utilities.VarDataTypes GetDataTypeTwo()
        {
            return DataTypeTwo;
        }

        public void SetDataTypeTwo(Utilities.VarDataTypes DataType)
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
