﻿using System;
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
        public static int assetMinimumWidth = 0;
        public static int assetMinimumHeight = 0;
        public static int linesOfCodeCount = 0;
		public static bool vmExecuting = false;
        public static List<CodeLine> linesOfCode = new List<CodeLine>();
		public static ArrayList assetsInCanvas = new ArrayList();
		public static ArrayList finalAssetsInCanvas = new ArrayList();

		// Helpers for found errors
		public static Dictionary<Object, List<int>> BlockToFaultyLineNumbers = new Dictionary<Object, List<int>>();
		public static Dictionary<Object, Tuple<List<string>, SolidColorBrush>> BlockToLineErrors = new Dictionary<Object, Tuple<List<string>, SolidColorBrush>>();
		public static Random rand = new Random();
		public static event EventHandler ChangedPageHeader;
		public static string PageHeader;
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

        // Helpers for front-end navigation
        public static bool RunButtonEnabled = false;
        public static bool CompileButtonEnabled = true;
        public static SolidColorBrush RunButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 39, 174, 96));
        public static SolidColorBrush CompileButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 39, 174, 96));
		
		/// Tracker of errors in the program
		public static List<ErrorTemplate> errorsInLines = new List<ErrorTemplate>();

        public enum ShapeTypes
        {
            Circle,
            Triangle,
            Square,
            Star,
            Heart,
            Polygon,
            Rhombus,
            Hexagon,
			Banana,
			Strawberry,
			Pineapple,
			Candy,
			Coffee,
			Cookie,
			Pizza,
			Chick,
			Crab,
			Fox,
			Whale,
			Hedgehog,
			Koala,
			Pig,
			Tiger,
			Zebra,
			Bull
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
			Negative,
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
			shapeToImagePath.Add(ShapeTypes.Banana, "/Assets/banana.png");
			shapeToImagePath.Add(ShapeTypes.Strawberry, "/Assets/strawberry.png");
			shapeToImagePath.Add(ShapeTypes.Pineapple, "/Assets/pineapple.png");
			shapeToImagePath.Add(ShapeTypes.Candy, "/Assets/candy.png");
			shapeToImagePath.Add(ShapeTypes.Coffee, "/Assets/coffee.png");
			shapeToImagePath.Add(ShapeTypes.Cookie, "/Assets/cookie.png");
			shapeToImagePath.Add(ShapeTypes.Pizza, "/Assets/pizza.png");
			shapeToImagePath.Add(ShapeTypes.Chick, "/Assets/chick.png");
			shapeToImagePath.Add(ShapeTypes.Crab, "/Assets/crab.png");
			shapeToImagePath.Add(ShapeTypes.Fox, "/Assets/fox.png");
			shapeToImagePath.Add(ShapeTypes.Whale, "/Assets/whale.png");
			shapeToImagePath.Add(ShapeTypes.Hedgehog, "/Assets/hedgehog.png");
			shapeToImagePath.Add(ShapeTypes.Koala, "/Assets/koala.png");
			shapeToImagePath.Add(ShapeTypes.Pig, "/Assets/pig.png");
			shapeToImagePath.Add(ShapeTypes.Tiger, "/Assets/tiger.png");
			shapeToImagePath.Add(ShapeTypes.Zebra, "/Assets/zebra.png");
			shapeToImagePath.Add(ShapeTypes.Bull, "/Assets/bull.png");
			
			actionToShapeType.Add("CircleInstantiator",   ShapeTypes.Circle);
            actionToShapeType.Add("TriangleInstantiator", ShapeTypes.Triangle);
            actionToShapeType.Add("SquareInstantiator",   ShapeTypes.Square);
            actionToShapeType.Add("StarInstantiator",     ShapeTypes.Star);
            actionToShapeType.Add("HeartInstantiator",    ShapeTypes.Heart);
            actionToShapeType.Add("PolygonInstantiator",  ShapeTypes.Polygon);
            actionToShapeType.Add("RhombusInstantiator",  ShapeTypes.Rhombus);
            actionToShapeType.Add("HexagonInstantiator",  ShapeTypes.Hexagon);
			actionToShapeType.Add("BananaInstantiator", ShapeTypes.Banana);
			actionToShapeType.Add("StrawberryInstantiator", ShapeTypes.Strawberry);
			actionToShapeType.Add("PineappleInstantiator", ShapeTypes.Pineapple);
			actionToShapeType.Add("CandyInstantiator", ShapeTypes.Candy);
			actionToShapeType.Add("CoffeeInstantiator", ShapeTypes.Coffee);
			actionToShapeType.Add("CookieInstantiator", ShapeTypes.Cookie);
			actionToShapeType.Add("PizzaInstantiator", ShapeTypes.Pizza);
			actionToShapeType.Add("ChickInstantiator", ShapeTypes.Chick);
			actionToShapeType.Add("CrabInstantiator", ShapeTypes.Crab);
			actionToShapeType.Add("FoxInstantiator", ShapeTypes.Fox);
			actionToShapeType.Add("WhaleInstantiator", ShapeTypes.Whale);
			actionToShapeType.Add("HedgehogInstantiator", ShapeTypes.Hedgehog);
			actionToShapeType.Add("KoalaInstantiator", ShapeTypes.Koala);
			actionToShapeType.Add("PigInstantiator", ShapeTypes.Pig);
			actionToShapeType.Add("TigerInstantiator", ShapeTypes.Tiger);
			actionToShapeType.Add("ZebraInstantiator", ShapeTypes.Zebra);
			actionToShapeType.Add("BullInstantiator", ShapeTypes.Bull);


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
			operatorToAction.Add(SemanticCubeUtilities.Operators.negative, QuadrupleAction.negative);
        }

		/// <summary>
		/// Retrieves an asset using an ID.
		/// Called by <see cref="VirtualMachine"/> whenever an asset property/behavior is needed.
		/// </summary>
		/// <param name="assetID"></param>
		/// <returns>Asset object with the given ID (or null if it does not exist)</returns>
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

		/// <summary>
		/// Get a default value given a C# native data type.
		/// Called by methods that initialize memory addresses to set a default value.
		/// </summary>
		/// <param name="t"></param>
		/// <returns>Default value for the corresponding type</returns>
		public static object GetDefaultValueFromType(Type t)
        {
            if (t == typeof(int) || t == typeof(Int32)) return 0;
            else if (t == typeof(bool)) return false;
            else if (t == typeof(string)) return "";
            else if (t == typeof(double) || t == typeof(float)) return 0.0;

            return null;
        }

		/// <summary>
		/// Get a default value given a <see cref="SemanticCubeUtilities.DataTypes"/> value.
		/// Called by methods that initialize memory addresses to set a default value.
		/// </summary>
		/// <param name="dt"></param>
		/// <returns>Default value for the corresponding type</returns>
		public static object GetDefaultValueFromType(SemanticCubeUtilities.DataTypes dt)
        {
            if (dt == SemanticCubeUtilities.DataTypes.number) return 0;
            else if (dt == SemanticCubeUtilities.DataTypes.boolean) return false;
            else if (dt == SemanticCubeUtilities.DataTypes.text) return "";

            return null;
        }

		/// <summary>
		/// Gets a random color from a pre-defined list to provide feedback on errors found.
		/// Called whenever an error is found during compile/execution time.
		/// </summary>
		/// <returns>Returns a random color brush</returns>
        public static SolidColorBrush GetRandomBrushForErrors()
		{
			return errorDotColors[rand.Next(0, errorDotColors.Count - 1)];
		}

		/// <summary>
		/// Activates visual feedback to indicate an error was found in a given block.
		/// Called when an error is found during compilation.
		/// </summary>
		/// <param name="block"></param>
		/// <param name="color"></param>
		public static void SetUserControlWithError(UserControl block, SolidColorBrush color)
		{
			if (block.GetType() == typeof(Marbles.AssignBlock))
			{
				var AssignBlock = block as AssignBlock;
				AssignBlock.SetError(color);
			}
			if (block.GetType() == typeof(Marbles.CreateAsset))
			{
				var CreateAsset = block as CreateAsset;
				CreateAsset.SetError(color);
			}
			if (block.GetType() == typeof(Marbles.CreateFunction))
			{
				var CreateFunction = block as CreateFunction;
				CreateFunction.SetError(color);
			}
			if (block.GetType() == typeof(Marbles.CreateVariable))
			{
				var CreateVariable = block as CreateVariable;
				CreateVariable.SetError(color);
			}
			if (block.GetType() == typeof(Marbles.DoBlock))
			{
				var DoBlock = block as DoBlock;
				DoBlock.SetError(color);
			}
			if (block.GetType() == typeof(Marbles.ForBlock))
			{
				var ForBlock = block as ForBlock;
				ForBlock.SetError(color);
			}
			if (block.GetType() == typeof(Marbles.IfBlock))
			{
				var IfBlock = block as IfBlock;
				IfBlock.SetError(color);
			}
			if (block.GetType() == typeof(Marbles.ReturnBlock))
			{
				var ReturnBlock = block as ReturnBlock;
				ReturnBlock.SetError(color);
			}
			if (block.GetType() == typeof(Marbles.DoBlock))
			{
				var DoBlock = block as DoBlock;
				DoBlock.SetError(color);
			}
			if (block.GetType() == typeof(Marbles.DoBlock))
			{
				var DoBlock = block as DoBlock;
				DoBlock.SetError(color);
			}
		}

		/// <summary>
		/// Retrieves a string labeling the given type of block.
		/// Called while generating a new error template for a block with errors.
		/// </summary>
		/// <param name="block"></param>
		/// <returns>String with the label</returns>
		public static string MapBlockTypeToLabel(UserControl block)
		{
			if (block != null)
			{
				if (block.GetType() == typeof(Marbles.AssignBlock))
				{
					return "Assign block".ToUpper(); ;
				}
				else if (block.GetType() == typeof(Marbles.CreateAsset))
				{
					return "Create asset block".ToUpper();
				}
				else if (block.GetType() == typeof(Marbles.CreateFunction))
				{
					return "Create function block".ToUpper();
				}
				else if (block.GetType() == typeof(Marbles.CreateVariable))
				{
					return "Create variable block".ToUpper();
				}
				else if (block.GetType() == typeof(Marbles.DoBlock))
				{
					return "Do block".ToUpper();
				}
				else if (block.GetType() == typeof(Marbles.ForBlock))
				{
					return "For block".ToUpper();
				}
				else if (block.GetType() == typeof(Marbles.IfBlock))
				{
					return "If block".ToUpper();
				}
				else if (block.GetType() == typeof(Marbles.ReturnBlock))
				{
					return "Return block".ToUpper();
				}
				else if (block.GetType() == typeof(Marbles.StopBlock))
				{
					return "Stop block".ToUpper();
				}
				else if (block.GetType() == typeof(Marbles.WhileBlock))
				{
					return "While block".ToUpper();
				}
			}
			else if (Utilities.vmExecuting)
			{
				return "Execution error";
			}

			return "";
		}

		/// <summary>
		/// Changes the page title. 
		/// This method is called on execution errors.
		/// </summary>
		/// <param name="newHeader"></param>
		public static void ChangePageHeader(string newHeader)
		{
			PageHeader = newHeader;
			ChangedPageHeader?.Invoke(null, EventArgs.Empty);
		}

		/// <summary>
		/// Enables the RUN button and provides feedback that the program is ready to run.
        /// Called by <see cref="CodeView.CompileButton_Click(object, Windows.UI.Xaml.RoutedEventArgs)"/>.
		/// </summary>
		public static void EnableRunButton()
		{
			RunButtonEnabled = true;
			RunButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 41, 128, 185));
		}

		/// <summary>
		/// Disables the RUN button and provides feedback that the program is not ready to run.
        /// Called by <see cref="CanvasView"/> and <see cref="CodeView"/>.
		/// </summary>
		public static void DisableRunButton()
		{
			RunButtonEnabled = false;
			RunButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 189, 195, 199));
		}

        /// <summary>
        /// Provides feedback with the COMPILE button to indicate that the program is ready to be compiled.
        /// Called by <see cref="CodeView.CompileButton_Click(object, Windows.UI.Xaml.RoutedEventArgs)"/>.
        /// </summary>
        public static void BlueCompile()
		{
			DisableRunButton();
			CompileButtonEnabled = true;
			CompileButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 41, 128, 185));
		}

        /// <summary>
        /// Provides feedback with the COMPILE button to indicate that the program has compilation errors.
        /// Called by <see cref="CodeView.CompileButton_Click(object, Windows.UI.Xaml.RoutedEventArgs)"/>.
        /// </summary>
        public static void RedCompile()
		{
			CompileButtonEnabled = true;
			CompileButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 231, 76, 60));
		}

		/// <summary>
		/// Provides feedback with the COMPILE button to indicate that the program compiled successfully
		/// and is ready to run.
        /// Called by <see cref="CodeView.CompileButton_Click(object, Windows.UI.Xaml.RoutedEventArgs)"/>.
		/// </summary>
		public static void GreenCompile()
		{
			CompileButtonEnabled = true;
			CompileButtonColor = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 39, 174, 96));
		}

        /// <summary>
        /// Disables buttons while the virtual machine is executing. 
        /// Called by <see cref="CanvasView.Run_Button_Click(object, Windows.UI.Xaml.RoutedEventArgs)"/>.
        /// </summary>
        public static void DisableRunAndCompileButtons()
		{
			CompileButtonEnabled = false;
			RunButtonEnabled = false;
		}

		/// <summary>
		/// Enables buttons after the virtual machine stopped execution.
        /// Called by <see cref="CanvasView.Run_Button_Click(object, Windows.UI.Xaml.RoutedEventArgs)"/>.
		/// </summary>
		public static void EnableRunAndCompileButtons()
		{
			CompileButtonEnabled = true;
			RunButtonEnabled = true;
		}
	}
}
