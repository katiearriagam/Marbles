using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Marbles
{
    public static class Utilities
    {
        public static int assetInitialHeight = 100;
        public static int assetInitialWidth = 100;
        public static int linesOfCodeCount = 0;
        public static ArrayList linesOfCode = new ArrayList();
		public static ArrayList assetsInCanvas = new ArrayList();

        public enum DataTypes
        {
            dtInt = 0,
            dtString = 1,
            dtBoolean = 2
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
        }

		public enum ShapeTypes
        {
            Circle,
            Triangle,
            Square
        }
        
        public enum BlockTypes
        {
            AssignBlock,
            CreateAsset,
            CreateFunction,
            CreateVariable,
            DoBlock,
            ForBlock,
            IfBlock,
            ReturnBlock,
            StopBlock,
            WhileBlock
        }

        public enum ValueTypes
        {
			Undefined,
            VariableCall,
            FunctionCall,
            NumberConstant,
            TextConstant,
            BooleanConstant,
            MathExpression,
            BooleanExpression,
            AssetProperty,
            AssetFunction
        }

        public static Dictionary<ShapeTypes, string> shapeToImagePath = new Dictionary<ShapeTypes, string>();
        public static Dictionary<string, ShapeTypes> actionToShapeType = new Dictionary<string, ShapeTypes>();

        public static BitmapImage BitmapFromPath(string path)
        {
            path = "ms-appx:" + path; // adapt to correct Uri format
            Uri imageUri = new Uri(path, UriKind.Absolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            return imageBitmap;
        }

        static Utilities()
        {
            shapeToImagePath.Add(ShapeTypes.Circle,   "/Assets/circle.png");
            shapeToImagePath.Add(ShapeTypes.Triangle, "/Assets/triangle.png");
            shapeToImagePath.Add(ShapeTypes.Square,   "/Assets/square.png");

            actionToShapeType.Add("CircleInstantiator", ShapeTypes.Circle);
            actionToShapeType.Add("TriangleInstantiator", ShapeTypes.Triangle);
            actionToShapeType.Add("SquareInstantiator", ShapeTypes.Square);
        }
    }
}
