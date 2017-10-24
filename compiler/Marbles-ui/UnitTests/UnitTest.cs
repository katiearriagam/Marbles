
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Marbles;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
	[TestClass]
	public class SemanticCubeUT
	{
		Dictionary<TypeTypeOperator, SemanticCubeUtilities.DataTypes> validTypesAndExpected = new Dictionary<TypeTypeOperator, SemanticCubeUtilities.DataTypes>();

		[TestInitialize]
		public void TestInitialize()
		{
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.divide), SemanticCubeUtilities.DataTypes.number);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.equalEqual), SemanticCubeUtilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.equals), SemanticCubeUtilities.DataTypes.number);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.greaterThan), SemanticCubeUtilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.greaterThanOrEqualTo), SemanticCubeUtilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.lessThan), SemanticCubeUtilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.lessThanOrEqualTo), SemanticCubeUtilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.minus), SemanticCubeUtilities.DataTypes.number);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.multiply), SemanticCubeUtilities.DataTypes.number);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.notEqual), SemanticCubeUtilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.plus), SemanticCubeUtilities.DataTypes.number);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.equalEqual), SemanticCubeUtilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.notEqual), SemanticCubeUtilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.equals), SemanticCubeUtilities.DataTypes.text);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.and), SemanticCubeUtilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.or), SemanticCubeUtilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.equals), SemanticCubeUtilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.equalEqual), SemanticCubeUtilities.DataTypes.boolean);
			validTypesAndExpected.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.notEqual), SemanticCubeUtilities.DataTypes.boolean);
		}

		[TestMethod]
		public void ValidTypes()
		{
			foreach (KeyValuePair<TypeTypeOperator, SemanticCubeUtilities.DataTypes> validEntry in validTypesAndExpected)
			{
				Assert.AreEqual(Marbles.SemanticCube.AnalyzeSemantics(validEntry.Key), validEntry.Value);
			}
		}

		[TestMethod]
		public void InvalidTypes()
		{
			List<SemanticCubeUtilities.DataTypes> DataTypesList = Enum.GetValues(typeof(SemanticCubeUtilities.DataTypes)).Cast<SemanticCubeUtilities.DataTypes>().ToList();
			List<SemanticCubeUtilities.Operators> OperatorsList = Enum.GetValues(typeof(SemanticCubeUtilities.Operators)).Cast<SemanticCubeUtilities.Operators>().ToList();
			TypeTypeOperator tto;

			foreach (SemanticCubeUtilities.DataTypes typeOne in DataTypesList)
			{
				foreach (SemanticCubeUtilities.DataTypes typeTwo in DataTypesList)
				{
					foreach (SemanticCubeUtilities.Operators op in OperatorsList)
					{
						tto = new TypeTypeOperator(typeOne, typeTwo, op);
						if (!validTypesAndExpected.ContainsKey(tto))
						{
							Assert.AreEqual(Marbles.SemanticCube.AnalyzeSemantics(tto), SemanticCubeUtilities.DataTypes.invalidDataType);
						}
					}
				}
			}
		}
	}
}
