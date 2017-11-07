using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbles
{
    public static class SemanticCube
    {
        private static Dictionary<TypeTypeOperator, SemanticCubeUtilities.DataTypes> Cube
            = new Dictionary<TypeTypeOperator, SemanticCubeUtilities.DataTypes>();

        static SemanticCube()
        {
            // negative
            Cube.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.invalidDataType, SemanticCubeUtilities.Operators.negative), SemanticCubeUtilities.DataTypes.number);
            Cube.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.invalidDataType, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.negative), SemanticCubeUtilities.DataTypes.number);

            Cube.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.invalidDataType, SemanticCubeUtilities.Operators.negative), SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.invalidDataType, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.negative), SemanticCubeUtilities.DataTypes.invalidDataType);

            Cube.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.invalidDataType, SemanticCubeUtilities.Operators.negative), SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(SemanticCubeUtilities.DataTypes.invalidDataType, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.negative), SemanticCubeUtilities.DataTypes.invalidDataType);

            // number - number
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.negative),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.plus),
                SemanticCubeUtilities.DataTypes.number);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.minus),
                SemanticCubeUtilities.DataTypes.number);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.multiply),
                SemanticCubeUtilities.DataTypes.number);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.divide),
                SemanticCubeUtilities.DataTypes.number);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.greaterThan),
                SemanticCubeUtilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.lessThan),
                SemanticCubeUtilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.greaterThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.lessThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.equalEqual),
                SemanticCubeUtilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.notEqual),
                SemanticCubeUtilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.equals),
                SemanticCubeUtilities.DataTypes.number);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.and),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.or),
                SemanticCubeUtilities.DataTypes.invalidDataType);

            // number - String
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.negative),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.negative),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.plus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.plus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.minus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.minus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.multiply),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.multiply),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.divide),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.divide),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.greaterThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.greaterThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.lessThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.lessThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.greaterThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.greaterThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.lessThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.lessThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.equalEqual),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.equalEqual),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.notEqual),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.notEqual),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.equals),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.equals),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.and),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.and),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.or),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.or),
                SemanticCubeUtilities.DataTypes.invalidDataType);

            // number - Boolean
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.negative),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.negative),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.plus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.plus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.minus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.minus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.multiply),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.multiply),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.divide),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.divide),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.greaterThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.greaterThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.lessThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.lessThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.greaterThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.greaterThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.lessThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.lessThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.equalEqual),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.equalEqual),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.notEqual),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.notEqual),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.equals),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.equals),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.and),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.and),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.or),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.number, SemanticCubeUtilities.Operators.or),
                SemanticCubeUtilities.DataTypes.invalidDataType);

            // String - String
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.negative),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.plus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.minus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.multiply),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.divide),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.greaterThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.lessThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.greaterThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.lessThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.equalEqual),
                SemanticCubeUtilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.notEqual),
                SemanticCubeUtilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.equals),
                SemanticCubeUtilities.DataTypes.text);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.and),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.or),
                SemanticCubeUtilities.DataTypes.invalidDataType);

            // String - Boolean
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.negative),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.negative),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.plus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.plus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.minus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.minus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.multiply),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.multiply),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.divide),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.divide),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.greaterThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.greaterThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.lessThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.lessThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.greaterThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.greaterThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.lessThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.lessThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.equalEqual),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.equalEqual),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.notEqual),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.notEqual),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.equals),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.equals),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.and),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.and),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.or),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.text, SemanticCubeUtilities.Operators.or),
                SemanticCubeUtilities.DataTypes.invalidDataType);

            // Boolean - Boolean
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.negative),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.plus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.minus),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.multiply),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.divide),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.greaterThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.lessThan),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.greaterThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.lessThanOrEqualTo),
                SemanticCubeUtilities.DataTypes.invalidDataType);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.equalEqual),
                SemanticCubeUtilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.notEqual),
                SemanticCubeUtilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.equals),
                SemanticCubeUtilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.and),
                SemanticCubeUtilities.DataTypes.boolean);
            Cube.Add(new TypeTypeOperator(
                SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.DataTypes.boolean, SemanticCubeUtilities.Operators.or),
                SemanticCubeUtilities.DataTypes.boolean);
        }

        public static SemanticCubeUtilities.DataTypes AnalyzeSemantics(TypeTypeOperator tto)
        {
            if (tto.GetOperator() == SemanticCubeUtilities.Operators.negative)
            {
                if (Cube.TryGetValue(tto, out SemanticCubeUtilities.DataTypes resultType))
                {
                    return resultType;
                }
                else
                {
                    return SemanticCubeUtilities.DataTypes.invalidDataType;
                }
            }

            if (tto.GetDataTypeOne().Equals(SemanticCubeUtilities.DataTypes.invalidDataType) ||
                tto.GetDataTypeTwo().Equals(SemanticCubeUtilities.DataTypes.invalidDataType) ||
                tto.GetOperator().Equals(SemanticCubeUtilities.Operators.invalidOperator))
            {
                return SemanticCubeUtilities.DataTypes.invalidDataType;
            }

            SemanticCubeUtilities.DataTypes type;
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

        public static SemanticCubeUtilities.DataTypes AnalyzeSemantics(SemanticCubeUtilities.DataTypes typeOne, SemanticCubeUtilities.DataTypes typeTwo, SemanticCubeUtilities.Operators op)
        {
            if (op == SemanticCubeUtilities.Operators.negative)
            {
                if (Cube.TryGetValue(new TypeTypeOperator(typeOne, typeTwo, op), out SemanticCubeUtilities.DataTypes resultType))
                {
                    return resultType;
                }
                else
                {
                    return SemanticCubeUtilities.DataTypes.invalidDataType;
                }
            }

            if (typeOne.Equals(SemanticCubeUtilities.DataTypes.invalidDataType) ||
                typeTwo.Equals(SemanticCubeUtilities.DataTypes.invalidDataType) ||
                op.Equals(SemanticCubeUtilities.Operators.invalidOperator))
            {
                return SemanticCubeUtilities.DataTypes.invalidDataType;
            }

            SemanticCubeUtilities.DataTypes type;
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

        public static SemanticCubeUtilities.DataTypes AnalyzeSemantics(Type typeOne, Type typeTwo, String op)
        {
            SemanticCubeUtilities.DataTypes DataTypeOne = SemanticCubeUtilities.GetDataTypeFromType(typeOne);
            SemanticCubeUtilities.DataTypes DataTypeTwo = SemanticCubeUtilities.GetDataTypeFromType(typeTwo);
            SemanticCubeUtilities.Operators Operator = SemanticCubeUtilities.GetOperatorFromString(op);

            if (Operator == SemanticCubeUtilities.Operators.negative)
            {
                if (Cube.TryGetValue(new TypeTypeOperator(DataTypeOne, DataTypeTwo, Operator), out SemanticCubeUtilities.DataTypes resultType))
                {
                    return resultType;
                }
                else
                {
                    return SemanticCubeUtilities.DataTypes.invalidDataType;
                }
            }

            if (DataTypeOne.Equals(SemanticCubeUtilities.DataTypes.invalidDataType) ||
                DataTypeTwo.Equals(SemanticCubeUtilities.DataTypes.invalidDataType) ||
                Operator.Equals(SemanticCubeUtilities.Operators.invalidOperator))
            {
                return SemanticCubeUtilities.DataTypes.invalidDataType;
            }

            SemanticCubeUtilities.DataTypes type;
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