using System;
using System.Collections.Generic;

namespace Cubo_Semantico_Marbles
{
    static class SemanticCube
    {
        private static Dictionary<TypeTypeOperator, Utilities.VarDataTypes> Cube 
            = new Dictionary<TypeTypeOperator, Utilities.VarDataTypes>();

        static SemanticCube()
        {
            // Integer - Integer
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.integer, Utilities.Operators.plus),
                Utilities.VarDataTypes.integer);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.integer, Utilities.Operators.minus),
                Utilities.VarDataTypes.integer);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.integer, Utilities.Operators.multiply),
                Utilities.VarDataTypes.integer);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.integer, Utilities.Operators.divide),
                Utilities.VarDataTypes.integer);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.integer, Utilities.Operators.greaterThan),
                Utilities.VarDataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.integer, Utilities.Operators.lessThan),
                Utilities.VarDataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.integer, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.VarDataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.integer, Utilities.Operators.lessThanOrEqualTo),
                Utilities.VarDataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.integer, Utilities.Operators.equalEqual),
                Utilities.VarDataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.integer, Utilities.Operators.notEqual),
                Utilities.VarDataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.integer, Utilities.Operators.equals),
                Utilities.VarDataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.integer, Utilities.Operators.and),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.integer, Utilities.Operators.or),
                Utilities.VarDataTypes.invalidDataType);

            // Integer - String
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.text, Utilities.Operators.plus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.integer, Utilities.Operators.plus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.text, Utilities.Operators.minus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.integer, Utilities.Operators.minus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.text, Utilities.Operators.multiply),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.integer, Utilities.Operators.multiply),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.text, Utilities.Operators.divide),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.integer, Utilities.Operators.divide),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.text, Utilities.Operators.greaterThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.integer, Utilities.Operators.greaterThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.text, Utilities.Operators.lessThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.integer, Utilities.Operators.lessThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.text, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.integer, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.text, Utilities.Operators.lessThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.integer, Utilities.Operators.lessThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.text, Utilities.Operators.equalEqual),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.integer, Utilities.Operators.equalEqual),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.text, Utilities.Operators.notEqual),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.integer, Utilities.Operators.notEqual),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.text, Utilities.Operators.equals),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.integer, Utilities.Operators.equals),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.text, Utilities.Operators.and),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.integer, Utilities.Operators.and),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.text, Utilities.Operators.or),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.integer, Utilities.Operators.or),
                Utilities.VarDataTypes.invalidDataType);

            // Integer - Boolean
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.boolean, Utilities.Operators.plus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.integer, Utilities.Operators.plus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.boolean, Utilities.Operators.minus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.integer, Utilities.Operators.minus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.boolean, Utilities.Operators.multiply),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.integer, Utilities.Operators.multiply),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.boolean, Utilities.Operators.divide),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.integer, Utilities.Operators.divide),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.boolean, Utilities.Operators.greaterThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.integer, Utilities.Operators.greaterThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.boolean, Utilities.Operators.lessThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.integer, Utilities.Operators.lessThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.boolean, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.integer, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.boolean, Utilities.Operators.lessThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.integer, Utilities.Operators.lessThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.boolean, Utilities.Operators.equalEqual),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.integer, Utilities.Operators.equalEqual),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.boolean, Utilities.Operators.notEqual),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.integer, Utilities.Operators.notEqual),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.boolean, Utilities.Operators.equals),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.integer, Utilities.Operators.equals),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.boolean, Utilities.Operators.and),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.integer, Utilities.Operators.and),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.integer, Utilities.VarDataTypes.boolean, Utilities.Operators.or),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.integer, Utilities.Operators.or),
                Utilities.VarDataTypes.invalidDataType);

            // String - String
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.text, Utilities.Operators.plus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.text, Utilities.Operators.minus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.text, Utilities.Operators.multiply),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.text, Utilities.Operators.divide),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.text, Utilities.Operators.greaterThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.text, Utilities.Operators.lessThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.text, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.text, Utilities.Operators.lessThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.text, Utilities.Operators.equalEqual),
                Utilities.VarDataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.text, Utilities.Operators.notEqual),
                Utilities.VarDataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.text, Utilities.Operators.equals),
                Utilities.VarDataTypes.text);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.text, Utilities.Operators.and),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.text, Utilities.Operators.or),
                Utilities.VarDataTypes.invalidDataType);

            // String - Boolean
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.boolean, Utilities.Operators.plus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.text, Utilities.Operators.plus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.boolean, Utilities.Operators.minus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.text, Utilities.Operators.minus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.boolean, Utilities.Operators.multiply),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.text, Utilities.Operators.multiply),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.boolean, Utilities.Operators.divide),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.text, Utilities.Operators.divide),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.boolean, Utilities.Operators.greaterThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.text, Utilities.Operators.greaterThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.boolean, Utilities.Operators.lessThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.text, Utilities.Operators.lessThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.boolean, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.text, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.boolean, Utilities.Operators.lessThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.text, Utilities.Operators.lessThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.boolean, Utilities.Operators.equalEqual),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.text, Utilities.Operators.equalEqual),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.boolean, Utilities.Operators.notEqual),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.text, Utilities.Operators.notEqual),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.boolean, Utilities.Operators.equals),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.text, Utilities.Operators.equals),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.boolean, Utilities.Operators.and),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.text, Utilities.Operators.and),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.text, Utilities.VarDataTypes.boolean, Utilities.Operators.or),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.text, Utilities.Operators.or),
                Utilities.VarDataTypes.invalidDataType);

            // Boolean - Boolean
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.boolean, Utilities.Operators.plus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.boolean, Utilities.Operators.minus),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.boolean, Utilities.Operators.multiply),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.boolean, Utilities.Operators.divide),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.boolean, Utilities.Operators.greaterThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.boolean, Utilities.Operators.lessThan),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.boolean, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.boolean, Utilities.Operators.lessThanOrEqualTo),
                Utilities.VarDataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.boolean, Utilities.Operators.equalEqual),
                Utilities.VarDataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.boolean, Utilities.Operators.notEqual),
                Utilities.VarDataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.boolean, Utilities.Operators.equals),
                Utilities.VarDataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.boolean, Utilities.Operators.and),
                Utilities.VarDataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.VarDataTypes.boolean, Utilities.VarDataTypes.boolean, Utilities.Operators.or),
                Utilities.VarDataTypes.boolean);
        }

        public static Utilities.VarDataTypes PuedeBailar(TypeTypeOperator tto)
        {
            if (tto.GetDataTypeOne().Equals(Utilities.VarDataTypes.invalidDataType) ||
                tto.GetDataTypeTwo().Equals(Utilities.VarDataTypes.invalidDataType) ||
                tto.GetOperator().Equals(Utilities.Operators.invalidOperator))
            {
                return Utilities.VarDataTypes.invalidDataType;
            }

            Utilities.VarDataTypes type;
            if (Cube.TryGetValue(tto, out type))
            {
                return type;
            }
            else
            {
                throw new Exception(String.Format("The specified key <{0},{1},{2}> was not found in the Semantic Cube",
                    tto.GetDataTypeOne(), tto.GetDataTypeTwo(), tto.GetOperator()));
            }
        }

        public static Utilities.VarDataTypes PuedeBailar(Utilities.VarDataTypes typeOne, Utilities.VarDataTypes typeTwo, Utilities.Operators op)
        {
            if (typeOne.Equals(Utilities.VarDataTypes.invalidDataType) ||
                typeTwo.Equals(Utilities.VarDataTypes.invalidDataType) ||
                op.Equals(Utilities.Operators.invalidOperator))
            {
                return Utilities.VarDataTypes.invalidDataType;
            }

            Utilities.VarDataTypes type;
            if (Cube.TryGetValue(new TypeTypeOperator(typeOne, typeTwo, op), out type))
            {
                return type;
            }
            else
            {
                throw new Exception(String.Format("The specified key <{0},{1},{2}> was not found in the Semantic Cube",
                    typeOne, typeTwo, op));
            }
        }

        public static Utilities.VarDataTypes PuedeBailar(Type typeOne, Type typeTwo, String op)
        {
            Utilities.VarDataTypes DataTypeOne = Utilities.GetDataTypeFromType(typeOne);
            Utilities.VarDataTypes DataTypeTwo = Utilities.GetDataTypeFromType(typeTwo);
            Utilities.Operators Operator = Utilities.GetOperatorFromChar(op);

            if (DataTypeOne.Equals(Utilities.VarDataTypes.invalidDataType) ||
                DataTypeTwo.Equals(Utilities.VarDataTypes.invalidDataType) ||
                Operator.Equals(Utilities.Operators.invalidOperator))
            {
                return Utilities.VarDataTypes.invalidDataType;
            }

            Utilities.VarDataTypes type;
            if (Cube.TryGetValue(new TypeTypeOperator(DataTypeOne, DataTypeTwo, Operator), out type))
            {
                return type;
            }
            else
            {
                throw new Exception(String.Format("The specified key <{0},{1},{2}> was not found in the Semantic Cube",
                    typeOne, typeTwo, op));
            }
        }
    }
}
