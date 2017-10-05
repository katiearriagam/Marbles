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
            List<Utilities.DataTypes> DataTypesList = Enum.GetValues(typeof(Utilities.DataTypes)).Cast<Utilities.DataTypes>().ToList();
            List<Utilities.Operators> OperatorsList = Enum.GetValues(typeof(Utilities.Operators)).Cast<Utilities.Operators>().ToList();
            TypeTypeOperator tto;
            try
            {
                foreach (Utilities.DataTypes typeOne in DataTypesList)
                {
                    foreach (Utilities.DataTypes typeTwo in DataTypesList)
                    {
                        foreach (Utilities.Operators op in OperatorsList)
                        {
                            if (typeOne.Equals(Utilities.DataTypes.invalidDataType) ||
                                typeTwo.Equals(Utilities.DataTypes.invalidDataType) ||
                                op.Equals(Utilities.Operators.invalidOperator))
                            {
                                continue;
                            }

                            tto = new TypeTypeOperator(typeOne, typeTwo, op);

							try
							{
								Console.WriteLine(typeOne + ", " +
											  typeTwo + ", " +
											  Utilities.GetOperatorVisualRepresentation(op) + "   \t-->\t" +
											  SemanticCube.AnalyzeSemantics(tto));
							}
							catch(System.ArgumentException)
							{
								Console.WriteLine(typeOne + ", " +
											  typeTwo + ", " +
											  Utilities.GetOperatorVisualRepresentation(op) + "   \t-->\t" +
											  Utilities.DataTypes.empty);
							}
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
