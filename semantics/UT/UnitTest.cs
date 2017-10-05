
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SemanticCube;
using System.Collections.Generic;
using System.Linq;

namespace UT
{
    [TestClass]
    public class SemanticCubeUT
    {
		Dictionary<TypeTypeOperator, Utilities.DataTypes> validTypesAndExpected = new Dictionary<TypeTypeOperator, Utilities.DataTypes>();

		[TestInitialize]
		public void TestInitialize()
		{
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.divide), Utilities.DataTypes.number);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.equalEqual), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.equals), Utilities.DataTypes.number);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.greaterThan), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.greaterThanOrEqualTo), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.lessThan), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.lessThanOrEqualTo), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.minus), Utilities.DataTypes.number);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.multiply), Utilities.DataTypes.number);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.notEqual), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.plus), Utilities.DataTypes.number);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.equalEqual), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.notEqual), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.equals), Utilities.DataTypes.text);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.and), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.or), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.equals), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.equalEqual), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.notEqual), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.equalEqual), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.equals), Utilities.DataTypes.shape);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.notEqual), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.equalEqual), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.equals), Utilities.DataTypes.color);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.notEqual), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.equalEqual), Utilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.equals), Utilities.DataTypes.character);
			validTypesAndExpected.Add(new TypeTypeOperator(Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.notEqual), Utilities.DataTypes.boolean);
		}

		[TestMethod]
        public void ValidTypes()
        {
			foreach(KeyValuePair<TypeTypeOperator, Utilities.DataTypes> validEntry in validTypesAndExpected)
			{
				Assert.AreEqual(SemanticCube.SemanticCube.AnalyzeSemantics(validEntry.Key), validEntry.Value);
			}
        }

		[TestMethod]
		public void InvalidTypes()
		{
			List<Utilities.DataTypes> DataTypesList = Enum.GetValues(typeof(Utilities.DataTypes)).Cast<Utilities.DataTypes>().ToList();
			List<Utilities.Operators> OperatorsList = Enum.GetValues(typeof(Utilities.Operators)).Cast<Utilities.Operators>().ToList();
			TypeTypeOperator tto;

			foreach (Utilities.DataTypes typeOne in DataTypesList)
			{
				foreach (Utilities.DataTypes typeTwo in DataTypesList)
				{
					foreach (Utilities.Operators op in OperatorsList)
					{
						tto = new TypeTypeOperator(typeOne, typeTwo, op);
						if (typeOne != Utilities.DataTypes.empty && typeTwo != Utilities.DataTypes.empty && !validTypesAndExpected.ContainsKey(tto))
						{
							Assert.AreEqual(SemanticCube.SemanticCube.AnalyzeSemantics(tto), Utilities.DataTypes.invalidDataType);
						}
					}
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof(System.ArgumentException), "Exception thrown because DT is empty.")]
		public void EmptyTypes()
		{
			List<Utilities.DataTypes> DataTypesList = Enum.GetValues(typeof(Utilities.DataTypes)).Cast<Utilities.DataTypes>().ToList();
			List<Utilities.Operators> OperatorsList = Enum.GetValues(typeof(Utilities.Operators)).Cast<Utilities.Operators>().ToList();
			TypeTypeOperator tto;

			foreach (Utilities.DataTypes typeOne in DataTypesList)
			{
				foreach (Utilities.DataTypes typeTwo in DataTypesList)
				{
					foreach (Utilities.Operators op in OperatorsList)
					{
						tto = new TypeTypeOperator(typeOne, typeTwo, op);
						if (typeOne == Utilities.DataTypes.empty || typeTwo == Utilities.DataTypes.empty)
						{
							SemanticCube.SemanticCube.AnalyzeSemantics(tto);
						}
					}
				}
			}
		}
	}
}
