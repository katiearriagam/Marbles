﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticCube
{
	public static class Utilities
	{
		private static Dictionary<Operators, String> OperatorToString = new Dictionary<Operators, String>();
		private static Dictionary<String, Operators> StringToOperator = new Dictionary<String, Operators>();
		private static Dictionary<Type, DataTypes> TypeToDataType = new Dictionary<Type, DataTypes>();

		static Utilities()
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
			text = 3,
			color = 4,
			character = 5,
			shape = 6,
			empty = 7
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
			invalidOperator = 13
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

		public static Operators GetOperatorFromChar(String c)
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