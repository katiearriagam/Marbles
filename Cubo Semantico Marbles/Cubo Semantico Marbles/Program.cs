using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubo_Semantico_Marbles
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Utilities.VarDataTypes> DataTypesList = Enum.GetValues(typeof(Utilities.VarDataTypes)).Cast<Utilities.VarDataTypes>().ToList();
            List<Utilities.Operators> OperatorsList = Enum.GetValues(typeof(Utilities.Operators)).Cast<Utilities.Operators>().ToList();
            TypeTypeOperator tto;
            try
            {
                foreach (Utilities.VarDataTypes typeOne in DataTypesList)
                {
                    foreach (Utilities.VarDataTypes typeTwo in DataTypesList)
                    {
                        foreach (Utilities.Operators op in OperatorsList)
                        {
                            if (typeOne.Equals(Utilities.VarDataTypes.invalidDataType) ||
                                typeTwo.Equals(Utilities.VarDataTypes.invalidDataType) ||
                                op.Equals(Utilities.Operators.invalidOperator))
                            {
                                continue;
                            }

                            tto = new TypeTypeOperator(typeOne, typeTwo, op);
                            Console.WriteLine(typeOne + ", " + 
                                              typeTwo + ", " + 
                                              Utilities.GetOperatorVisualRepresentation(op) + "   \t-->\t" + 
                                              SemanticCube.PuedeBailar(tto));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
