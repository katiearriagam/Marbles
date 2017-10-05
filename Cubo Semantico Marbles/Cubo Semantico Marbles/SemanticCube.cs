using System;
using System.Collections.Generic;

namespace Cubo_Semantico_Marbles
{
    static class SemanticCube
    {
        private static Dictionary<TypeTypeOperator, Utilities.DataTypes> Cube 
            = new Dictionary<TypeTypeOperator, Utilities.DataTypes>();

        static SemanticCube()
        {
            // number - number
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.plus),
                Utilities.DataTypes.number);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.minus),
                Utilities.DataTypes.number);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.multiply),
                Utilities.DataTypes.number);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.divide),
                Utilities.DataTypes.number);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.greaterThan),
                Utilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.lessThan),
                Utilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.lessThanOrEqualTo),
                Utilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.equalEqual),
                Utilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.notEqual),
                Utilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.equals),
                Utilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.and),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.number, Utilities.Operators.or),
                Utilities.DataTypes.invalidDataType);

            // number - String
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.text, Utilities.Operators.plus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.number, Utilities.Operators.plus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.text, Utilities.Operators.minus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.number, Utilities.Operators.minus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.text, Utilities.Operators.multiply),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.number, Utilities.Operators.multiply),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.text, Utilities.Operators.divide),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.number, Utilities.Operators.divide),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.text, Utilities.Operators.greaterThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.number, Utilities.Operators.greaterThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.text, Utilities.Operators.lessThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.number, Utilities.Operators.lessThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.text, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.number, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.text, Utilities.Operators.lessThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.number, Utilities.Operators.lessThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.text, Utilities.Operators.equalEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.number, Utilities.Operators.equalEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.text, Utilities.Operators.notEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.number, Utilities.Operators.notEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.text, Utilities.Operators.equals),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.number, Utilities.Operators.equals),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.text, Utilities.Operators.and),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.number, Utilities.Operators.and),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.text, Utilities.Operators.or),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.number, Utilities.Operators.or),
                Utilities.DataTypes.invalidDataType);

            // number - Boolean
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.boolean, Utilities.Operators.plus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.number, Utilities.Operators.plus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.boolean, Utilities.Operators.minus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.number, Utilities.Operators.minus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.boolean, Utilities.Operators.multiply),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.number, Utilities.Operators.multiply),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.boolean, Utilities.Operators.divide),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.number, Utilities.Operators.divide),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.boolean, Utilities.Operators.greaterThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.number, Utilities.Operators.greaterThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.boolean, Utilities.Operators.lessThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.number, Utilities.Operators.lessThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.boolean, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.number, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.boolean, Utilities.Operators.lessThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.number, Utilities.Operators.lessThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.boolean, Utilities.Operators.equalEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.number, Utilities.Operators.equalEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.boolean, Utilities.Operators.notEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.number, Utilities.Operators.notEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.boolean, Utilities.Operators.equals),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.number, Utilities.Operators.equals),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.boolean, Utilities.Operators.and),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.number, Utilities.Operators.and),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.number, Utilities.DataTypes.boolean, Utilities.Operators.or),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.number, Utilities.Operators.or),
                Utilities.DataTypes.invalidDataType);

			// number - Color
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.color, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.number, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.color, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.number, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.color, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.number, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.color, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.number, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.color, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.number, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.color, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.number, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.color, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.number, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.color, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.number, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.color, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.number, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.color, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.number, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.color, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.number, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.color, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.number, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.color, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.number, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// number - Character
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.character, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.number, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.character, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.number, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.character, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.number, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.character, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.number, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.character, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.number, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.character, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.number, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.character, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.number, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.character, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.number, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.character, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.number, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.character, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.number, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.character, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.number, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.character, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.number, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.character, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.number, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// number - Shape
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.shape, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.number, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.shape, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.number, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.shape, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.number, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.shape, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.number, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.shape, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.number, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.shape, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.number, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.shape, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.number, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.shape, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.number, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.shape, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.number, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.shape, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.number, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.shape, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.number, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.shape, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.number, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.number, Utilities.DataTypes.shape, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.number, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// String - String
			Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.plus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.minus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.multiply),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.divide),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.greaterThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.lessThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.lessThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.equalEqual),
                Utilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.notEqual),
                Utilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.equals),
                Utilities.DataTypes.text);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.and),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.text, Utilities.Operators.or),
                Utilities.DataTypes.invalidDataType);

            // String - Boolean
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.boolean, Utilities.Operators.plus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.text, Utilities.Operators.plus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.boolean, Utilities.Operators.minus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.text, Utilities.Operators.minus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.boolean, Utilities.Operators.multiply),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.text, Utilities.Operators.multiply),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.boolean, Utilities.Operators.divide),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.text, Utilities.Operators.divide),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.boolean, Utilities.Operators.greaterThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.text, Utilities.Operators.greaterThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.boolean, Utilities.Operators.lessThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.text, Utilities.Operators.lessThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.boolean, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.text, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.boolean, Utilities.Operators.lessThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.text, Utilities.Operators.lessThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.boolean, Utilities.Operators.equalEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.text, Utilities.Operators.equalEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.boolean, Utilities.Operators.notEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.text, Utilities.Operators.notEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.boolean, Utilities.Operators.equals),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.text, Utilities.Operators.equals),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.boolean, Utilities.Operators.and),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.text, Utilities.Operators.and),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.text, Utilities.DataTypes.boolean, Utilities.Operators.or),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.text, Utilities.Operators.or),
                Utilities.DataTypes.invalidDataType);

			// String - Color
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.color, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.text, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.color, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.text, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.color, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.text, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.color, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.text, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.color, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.text, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.color, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.text, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.color, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.text, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.color, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.text, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.color, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.text, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.color, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.text, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.color, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.text, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.color, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.text, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.color, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.text, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// String - Character
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.character, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.text, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.character, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.text, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.character, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.text, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.character, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.text, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.character, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.text, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.character, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.text, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.character, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.text, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.character, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.text, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.character, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.text, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.character, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.text, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.character, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.text, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.character, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.text, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.character, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.text, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// String - Shape
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.shape, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.text, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.shape, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.text, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.shape, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.text, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.shape, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.text, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.shape, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.text, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.shape, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.text, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.shape, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.text, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.shape, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.text, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.shape, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.text, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.shape, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.text, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.shape, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.text, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.shape, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.text, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.text, Utilities.DataTypes.shape, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.text, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// Boolean - Boolean
			Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.plus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.minus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.multiply),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.divide),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.greaterThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.lessThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.lessThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.equalEqual),
                Utilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.notEqual),
                Utilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.equals),
                Utilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.and),
                Utilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                Utilities.DataTypes.boolean, Utilities.DataTypes.boolean, Utilities.Operators.or),
                Utilities.DataTypes.boolean);

			// Boolean - Color
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.color, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.boolean, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.color, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.boolean, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.color, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.boolean, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.color, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.boolean, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.color, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.boolean, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.color, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.boolean, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.color, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.boolean, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.color, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.boolean, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.color, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.boolean, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.color, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.boolean, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.color, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.boolean, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.color, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.boolean, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.color, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.boolean, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// Boolean - Character
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.character, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.boolean, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.character, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.boolean, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.character, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.boolean, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.character, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.boolean, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.character, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.boolean, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.character, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.boolean, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.character, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.boolean, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.character, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.boolean, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.character, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.boolean, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.character, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.boolean, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.character, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.boolean, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.character, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.boolean, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.character, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.boolean, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// Boolean - Shape
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.shape, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.boolean, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.shape, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.boolean, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.shape, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.boolean, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.shape, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.boolean, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.shape, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.boolean, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.shape, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.boolean, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.shape, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.boolean, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.shape, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.boolean, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.shape, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.boolean, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.shape, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.boolean, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.shape, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.boolean, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.shape, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.boolean, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.boolean, Utilities.DataTypes.shape, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.boolean, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// Color - Color
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.equalEqual),
				Utilities.DataTypes.boolean);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.notEqual),
				Utilities.DataTypes.boolean);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.equals),
				Utilities.DataTypes.color);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.color, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// Color - Character
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.character, Utilities.Operators.plus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.color, Utilities.Operators.plus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.character, Utilities.Operators.minus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.color, Utilities.Operators.minus),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.character, Utilities.Operators.multiply),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.color, Utilities.Operators.multiply),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.character, Utilities.Operators.divide),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.color, Utilities.Operators.divide),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.character, Utilities.Operators.greaterThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.color, Utilities.Operators.greaterThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.character, Utilities.Operators.lessThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.color, Utilities.Operators.lessThan),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.character, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.color, Utilities.Operators.greaterThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.character, Utilities.Operators.lessThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.color, Utilities.Operators.lessThanOrEqualTo),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.character, Utilities.Operators.equalEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.color, Utilities.Operators.equalEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.character, Utilities.Operators.notEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.color, Utilities.Operators.notEqual),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.character, Utilities.Operators.equals),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.color, Utilities.Operators.equals),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.character, Utilities.Operators.and),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.color, Utilities.Operators.and),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.character, Utilities.Operators.or),
                Utilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.color, Utilities.Operators.or),
                Utilities.DataTypes.invalidDataType);

			// Color - Shape
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.shape, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.color, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.shape, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.color, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.shape, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.color, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.shape, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.color, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.shape, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.color, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.shape, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.color, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.shape, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.color, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.shape, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.color, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.shape, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.color, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.shape, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.color, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.shape, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.color, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.shape, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.color, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.color, Utilities.DataTypes.shape, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.color, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// Character - Shape
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.shape, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.character, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.shape, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.character, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.shape, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.character, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.shape, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.character, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.shape, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.character, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.shape, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.character, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.shape, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.character, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.shape, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.character, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.shape, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.character, Utilities.Operators.equalEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.shape, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.character, Utilities.Operators.notEqual),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.shape, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.character, Utilities.Operators.equals),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.shape, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.character, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.shape, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.character, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// Character - Character
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.equalEqual),
				Utilities.DataTypes.boolean);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.notEqual),
				Utilities.DataTypes.boolean);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.equals),
				Utilities.DataTypes.character);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.character, Utilities.DataTypes.character, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// Shape - Shape
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.plus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.minus),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.multiply),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.divide),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.greaterThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.lessThan),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.greaterThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.lessThanOrEqualTo),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.equalEqual),
				Utilities.DataTypes.boolean);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.notEqual),
				Utilities.DataTypes.boolean);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.equals),
				Utilities.DataTypes.shape);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.and),
				Utilities.DataTypes.invalidDataType);
			Cube.Add(new TypeTypeOperator(
				Utilities.DataTypes.shape, Utilities.DataTypes.shape, Utilities.Operators.or),
				Utilities.DataTypes.invalidDataType);

			// Empty

		}

		public static Utilities.DataTypes AnalyzeSemantics(TypeTypeOperator tto)
        {
            if (tto.GetDataTypeOne().Equals(Utilities.DataTypes.invalidDataType) ||
                tto.GetDataTypeTwo().Equals(Utilities.DataTypes.invalidDataType) ||
                tto.GetOperator().Equals(Utilities.Operators.invalidOperator))
            {
                return Utilities.DataTypes.invalidDataType;
            }

			if (tto.GetDataTypeOne().Equals(Utilities.DataTypes.empty) ||
				tto.GetDataTypeTwo().Equals(Utilities.DataTypes.empty))
			{
				throw new System.ArgumentException("Cannot use operations with empty data/functions.", "");
			}

            Utilities.DataTypes type;
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

        public static Utilities.DataTypes AnalyzeSemantics(Utilities.DataTypes typeOne, Utilities.DataTypes typeTwo, Utilities.Operators op)
        {
            if (typeOne.Equals(Utilities.DataTypes.invalidDataType) ||
                typeTwo.Equals(Utilities.DataTypes.invalidDataType) ||
                op.Equals(Utilities.Operators.invalidOperator))
            {
                return Utilities.DataTypes.invalidDataType;
            }

			if (typeOne.Equals(Utilities.DataTypes.empty) ||
				typeTwo.Equals(Utilities.DataTypes.empty))
			{
				throw new System.ArgumentException("Cannot use operations with empty data/functions.", "");
			}

			Utilities.DataTypes type;
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

        public static Utilities.DataTypes AnalyzeSemantics(Type typeOne, Type typeTwo, String op)
        {
            Utilities.DataTypes DataTypeOne = Utilities.GetDataTypeFromType(typeOne);
            Utilities.DataTypes DataTypeTwo = Utilities.GetDataTypeFromType(typeTwo);
            Utilities.Operators Operator = Utilities.GetOperatorFromChar(op);

            if (DataTypeOne.Equals(Utilities.DataTypes.invalidDataType) ||
                DataTypeTwo.Equals(Utilities.DataTypes.invalidDataType) ||
                Operator.Equals(Utilities.Operators.invalidOperator))
            {
                return Utilities.DataTypes.invalidDataType;
            }

			if (DataTypeOne.Equals(Utilities.DataTypes.empty) ||
				DataTypeTwo.Equals(Utilities.DataTypes.empty))
			{
				throw new System.ArgumentException("Cannot use operations with empty data/functions.", "");
			}

			Utilities.DataTypes type;
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
