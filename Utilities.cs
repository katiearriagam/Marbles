using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubo_Semantico_Marbles
{
    static class Utilities
    {
        private static Dictionary<Operators, String>  OperatorToString  = new Dictionary<Operators, String>();
        private static Dictionary<String, Operators>  StringToOperator  = new Dictionary<String, Operators>();
        private static Dictionary<Type, VarDataTypes> TypeToDataType = new Dictionary<Type, VarDataTypes>();

        static Utilities()
        {
            OperatorToString.Add(Operators.plus,                 "+" );
            OperatorToString.Add(Operators.minus,                "-" );
            OperatorToString.Add(Operators.multiply,             "*" );
            OperatorToString.Add(Operators.divide,               "/" );
            OperatorToString.Add(Operators.greaterThan,          ">" );
            OperatorToString.Add(Operators.lessThan,             "<" );
            OperatorToString.Add(Operators.greaterThanOrEqualTo, ">=");
            OperatorToString.Add(Operators.lessThanOrEqualTo,    "<=");
            OperatorToString.Add(Operators.equalEqual,           "==");
            OperatorToString.Add(Operators.notEqual,             "!=");
            OperatorToString.Add(Operators.equals,               "=" );
            OperatorToString.Add(Operators.and,                  "&&");
            OperatorToString.Add(Operators.or,                   "||");

            StringToOperator.Add("+" , Operators.plus);
            StringToOperator.Add("-" , Operators.minus);
            StringToOperator.Add("*" , Operators.multiply);
            StringToOperator.Add("/" , Operators.divide);
            StringToOperator.Add(">" , Operators.greaterThan);
            StringToOperator.Add("<" , Operators.lessThan);
            StringToOperator.Add(">=", Operators.greaterThanOrEqualTo);
            StringToOperator.Add("<=", Operators.lessThanOrEqualTo);
            StringToOperator.Add("==", Operators.equalEqual);
            StringToOperator.Add("!=", Operators.notEqual);
            StringToOperator.Add("=" , Operators.equals);
            StringToOperator.Add("&&", Operators.and);
            StringToOperator.Add("||", Operators.or);
            
            TypeToDataType.Add(typeof(int)    , VarDataTypes.integer);
            TypeToDataType.Add(typeof(String) , VarDataTypes.text   );
            TypeToDataType.Add(typeof(Boolean), VarDataTypes.boolean);
            
        }

        public enum VarDataTypes
        {
            invalidDataType = 0,
            integer         = 1,
            boolean         = 2,
            text            = 3
        }

        public enum FuncDataTypes
        {
            invalidDataType = 0,
            integer         = 1,
            boolean         = 2,
            text            = 3,
            empty           = 4
        }

        public enum Operators
        {
            plus                  = 0,
            minus                 = 1,
            multiply              = 2,
            divide                = 3,
            greaterThan           = 4,
            lessThan              = 5,
            greaterThanOrEqualTo  = 6,
            lessThanOrEqualTo     = 7,
            equalEqual            = 8,
            notEqual              = 9,
            equals                = 10,
            and                   = 11,
            or                    = 12,
            invalidOperator       = 13
        }

        public static VarDataTypes GetDataTypeFromType(Type t)
        {
            VarDataTypes type;
            if (TypeToDataType.TryGetValue(t, out type))
            {
                return type;
            }
            else
            {
                return VarDataTypes.invalidDataType;
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
