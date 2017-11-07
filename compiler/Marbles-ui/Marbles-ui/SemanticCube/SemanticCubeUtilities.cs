using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
	public static class SemanticCubeUtilities
	{
		private static Dictionary<Operators, String> OperatorToString = new Dictionary<Operators, String>();
		private static Dictionary<String, Operators> StringToOperator = new Dictionary<String, Operators>();
		private static Dictionary<Type, DataTypes> TypeToDataType = new Dictionary<Type, DataTypes>();

		static SemanticCubeUtilities()
		{
			OperatorToString.Add(Operators.plus, "+");
			OperatorToString.Add(Operators.minus, "-");
			OperatorToString.Add(Operators.multiply, "*");
			OperatorToString.Add(Operators.divide, "/");
			OperatorToString.Add(Operators.greaterThan, ">");
			OperatorToString.Add(Operators.lessThan, "<");
			OperatorToString.Add(Operators.greaterThanOrEqualTo, ">=");
			OperatorToString.Add(Operators.lessThanOrEqualTo, "<=");
			OperatorToString.Add(Operators.equalEqual, "==");
			OperatorToString.Add(Operators.notEqual, "!=");
			OperatorToString.Add(Operators.equals, "=");
			OperatorToString.Add(Operators.and, "and");
			OperatorToString.Add(Operators.or, "or");

			StringToOperator.Add("+", Operators.plus);
			StringToOperator.Add("-", Operators.minus);
			StringToOperator.Add("*", Operators.multiply);
			StringToOperator.Add("/", Operators.divide);
			StringToOperator.Add(">", Operators.greaterThan);
			StringToOperator.Add("<", Operators.lessThan);
			StringToOperator.Add(">=", Operators.greaterThanOrEqualTo);
			StringToOperator.Add("<=", Operators.lessThanOrEqualTo);
			StringToOperator.Add("==", Operators.equalEqual);
			StringToOperator.Add("!=", Operators.notEqual);
			StringToOperator.Add("=", Operators.equals);
			StringToOperator.Add("and", Operators.and);
			StringToOperator.Add("or", Operators.or);

			TypeToDataType.Add(typeof(int), DataTypes.number);
			TypeToDataType.Add(typeof(String), DataTypes.text);
			TypeToDataType.Add(typeof(Boolean), DataTypes.boolean);
		}

		public enum DataTypes
		{
			invalidDataType = 0,
			number = 1,
			boolean = 2,
			text = 3
		}

		public enum Operators
		{
			plus = 0,
			minus = 1,
			multiply = 2,
			divide = 3,
			greaterThan = 4,
			lessThan = 5,
			greaterThanOrEqualTo = 6,
			lessThanOrEqualTo = 7,
			equalEqual = 8,
			notEqual = 9,
			equals = 10,
			and = 11,
			or = 12,
			negative = 13,
            fakeBottom = 14,
			invalidOperator = 15
		}

        public static int OperatorToPriority(Operators op)
        {
            switch (op)
            {
                case Operators.fakeBottom:
                    return 0;
				case Operators.negative:
					return 1;
                case Operators.multiply:
                case Operators.divide:
                    return 2;
                case Operators.plus:
                case Operators.minus:
                    return 3;
                case Operators.greaterThan:
                case Operators.lessThan:
                case Operators.greaterThanOrEqualTo:
                case Operators.lessThanOrEqualTo:
                    return 4;
                case Operators.equalEqual:
                case Operators.notEqual:
                    return 5;
                case Operators.and:
                    return 6;
                case Operators.or:
                    return 7;
                case Operators.equals:
                    return 8;
                default: // should never arrive here
                    return -1;
            }
        }

		public static DataTypes GetDataTypeFromType(Type t)
		{
			DataTypes type;
			if (TypeToDataType.TryGetValue(t, out type))
			{
				return type;
			}
			else
			{
				return DataTypes.invalidDataType;
			}
		}

		public static Operators GetOperatorFromString(String c)
		{
			Operators op;
			if (StringToOperator.TryGetValue(c, out op))
			{
				return op;
			}
			else
			{
				return Operators.invalidOperator;
			}
		}

		public static String GetOperatorVisualRepresentation(Operators op)
		{
			String str;
			if (OperatorToString.TryGetValue(op, out str))
			{
				return str;
			}
			else
			{
				return "";
			}
		}
	}
}
