using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Marbles
{
    public static class Utilities
    {
        public static int assetInitialHeight = 90;
        public static int assetInitialWidth = 90;
        public static int assetMinimumWidth = 10;
        public static int assetMinimumHeight = 10;
        public static int linesOfCodeCount = 0;
        public static List<CodeLine> linesOfCode = new List<CodeLine>();
		public static ArrayList assetsInCanvas = new ArrayList();
		public static ArrayList finalAssetsInCanvas = new ArrayList();
		public static Dictionary<Object, List<int>> BlockToFaultyLineNumbers = new Dictionary<Object, List<int>>();
		public static Dictionary<Object, Tuple<List<string>, SolidColorBrush>> BlockToLineErrors = new Dictionary<Object, Tuple<List<string>, SolidColorBrush>>();
		public static Random rand = new Random();

		public static List<SolidColorBrush> errorDotColors =
			new List<SolidColorBrush>(new SolidColorBrush[]
				{	new SolidColorBrush(Windows.UI.Color.FromArgb(255, 26, 188, 156)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 22, 160, 133)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 46, 204, 113)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 39, 174, 96)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 52, 152, 219)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 41, 128, 185)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 155, 89, 182)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 142, 68, 173)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 241, 196, 15)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 243, 156, 18)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 230, 126, 34)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 211, 84, 0)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 231, 76, 60)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 192, 57, 43)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 52, 73, 94)),
					new SolidColorBrush(Windows.UI.Color.FromArgb(255, 44, 62, 80))
				});

        /// <summary>
        /// Helpers for front-end navigation
        /// </summary>
        public static bool RunButtonEnabled = false;
        public static bool CompileButtonEnabled = true;
        public static SolidColorBrush RunButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 39, 174, 96));
        public static SolidColorBrush CompileButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 39, 174, 96));

		public static List<ErrorTemplate> errorsInLines = new List<ErrorTemplate>();


		public static void EnableRunButton()
        {
            RunButtonEnabled = true;
            RunButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 41, 128, 185));
        }

        public static void DisableRunButton()
        {
            RunButtonEnabled = false;
            RunButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 189, 195, 199));
        }

        public static void BlueCompile()
        {
            DisableRunButton();
            CompileButtonEnabled = true;
            CompileButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 41, 128, 185));
        }

        public static void RedCompile()
        {
            CompileButtonEnabled = true;
            CompileButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 231, 76, 60));
        }

        public static void GreenCompile()
        {
            CompileButtonEnabled = true;
            CompileButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 39, 174, 96));
        }

        public enum ShapeTypes
        {
            Circle,
            Triangle,
            Square,
            Star,
            Heart,
            Polygon,
            Rhombus,
            Hexagon
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
			Parenthesis,
            AssetProperty,
            AssetFunction
        }

        public enum QuadrupleAction
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
            Goto = 13,
            GotoF = 14,
            GotoV = 15,
            fakeBottom = 16,
            param = 17,
            gosub = 18,
            retorno = 19,
            era = 20,
            end = 21,
            endProc = 22,
            stop = 23,
            move_x = 24,
            move_y = 25,
            set_position = 26,
            rotate = 27,
            spin = 28,
			negative = 29
        }

        public enum AssetAction
        {
            move_x = 0,
            move_y = 1,
            set_position = 2,
            rotate = 3,
            spin = 4
        }

        public static Dictionary<ShapeTypes, string> shapeToImagePath = new Dictionary<ShapeTypes, string>();
        public static Dictionary<string, ShapeTypes> actionToShapeType = new Dictionary<string, ShapeTypes>();
        public static Dictionary<SemanticCubeUtilities.Operators, QuadrupleAction> operatorToAction = new Dictionary<SemanticCubeUtilities.Operators, QuadrupleAction>();

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
            shapeToImagePath.Add(ShapeTypes.Star,     "/Assets/star.png");
            shapeToImagePath.Add(ShapeTypes.Heart,    "/Assets/heart.png");
            shapeToImagePath.Add(ShapeTypes.Polygon,  "/Assets/polygon.png");
            shapeToImagePath.Add(ShapeTypes.Rhombus,  "/Assets/rhombus.png");
            shapeToImagePath.Add(ShapeTypes.Hexagon,  "/Assets/hexagon.png");

            actionToShapeType.Add("CircleInstantiator",   ShapeTypes.Circle);
            actionToShapeType.Add("TriangleInstantiator", ShapeTypes.Triangle);
            actionToShapeType.Add("SquareInstantiator",   ShapeTypes.Square);
            actionToShapeType.Add("StarInstantiator",     ShapeTypes.Star);
            actionToShapeType.Add("HeartInstantiator",    ShapeTypes.Heart);
            actionToShapeType.Add("PolygonInstantiator",  ShapeTypes.Polygon);
            actionToShapeType.Add("RhombusInstantiator",  ShapeTypes.Rhombus);
            actionToShapeType.Add("HexagonInstantiator",  ShapeTypes.Hexagon);

            operatorToAction.Add(SemanticCubeUtilities.Operators.plus, QuadrupleAction.plus);
            operatorToAction.Add(SemanticCubeUtilities.Operators.minus, QuadrupleAction.minus);
            operatorToAction.Add(SemanticCubeUtilities.Operators.multiply, QuadrupleAction.multiply);
            operatorToAction.Add(SemanticCubeUtilities.Operators.divide, QuadrupleAction.divide);
            operatorToAction.Add(SemanticCubeUtilities.Operators.greaterThan, QuadrupleAction.greaterThan);
            operatorToAction.Add(SemanticCubeUtilities.Operators.lessThan, QuadrupleAction.lessThan);
            operatorToAction.Add(SemanticCubeUtilities.Operators.greaterThanOrEqualTo, QuadrupleAction.greaterThanOrEqualTo);
            operatorToAction.Add(SemanticCubeUtilities.Operators.lessThanOrEqualTo, QuadrupleAction.lessThanOrEqualTo);
            operatorToAction.Add(SemanticCubeUtilities.Operators.equalEqual, QuadrupleAction.equalEqual);
            operatorToAction.Add(SemanticCubeUtilities.Operators.notEqual, QuadrupleAction.notEqual);
            operatorToAction.Add(SemanticCubeUtilities.Operators.equals, QuadrupleAction.equals);
            operatorToAction.Add(SemanticCubeUtilities.Operators.and, QuadrupleAction.and);
            operatorToAction.Add(SemanticCubeUtilities.Operators.or, QuadrupleAction.or);
        }

        public static Asset FindAssetFromID(string assetID)
        {
            foreach (Asset a in finalAssetsInCanvas)
            {
                if (a.GetID() == assetID)
                {
                    return a;
                }
            }
            return null;
        }

		public static SolidColorBrush GetRandomBrushForErrors()
		{
			return errorDotColors[rand.Next(0, errorDotColors.Count - 1)];
		}

		public static void SetUserControlWithError(UserControl block, SolidColorBrush color)
		{
			if (block.GetType() == typeof(Marbles.DoBlock))
			{
				var DoBlock = block as DoBlock;
				DoBlock.SetError(color);
			}
		}

		public static string MapBlockTypeToLabel(UserControl block)
		{
			if (block.GetType() == typeof(Marbles.AssignBlock))
			{
				return "Assign block";
			}
			else if (block.GetType() == typeof(Marbles.CreateAsset))
			{
				return "Create asset block";
			}
			else if (block.GetType() == typeof(Marbles.CreateFunction))
			{
				return "Create function block";
			}
			else if (block.GetType() == typeof(Marbles.CreateVariable))
			{
				return "Create variable block";
			}
			else if (block.GetType() == typeof(Marbles.DoBlock))
			{
				return "Do block";
			}
			else if (block.GetType() == typeof(Marbles.ForBlock))
			{
				return "For block";
			}
			else if (block.GetType() == typeof(Marbles.IfBlock))
			{
				return "If block";
			}
			else if (block.GetType() == typeof(Marbles.ReturnBlock))
			{
				return "Return block";
			}
			else if (block.GetType() == typeof(Marbles.StopBlock))
			{
				return "Stop block";
			}
			else if (block.GetType() == typeof(Marbles.WhileBlock))
			{
				return "While block";
			}

			return "";
		}
    }
}
