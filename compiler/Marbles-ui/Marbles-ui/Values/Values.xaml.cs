using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Marbles
{
    public sealed partial class Values : UserControl
    {
        private Utilities.ValueTypes selectedValueType = Utilities.ValueTypes.Undefined;
        private object selectedValue = null;

		public Values()
        {
            this.InitializeComponent();
        }

        public static event EventHandler SomethingChanged;

        /// <summary>
        /// Event invoked when a Value block is dropped into this UserControl.
        /// Accepts the Drop operation and updates the UI to show the Value dropped.
        /// Invokes <see cref="SomethingChanged"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Values_OnDrop(object sender, DragEventArgs e)
		{
			e.AcceptedOperation = DataPackageOperation.Copy;

			if (e.DataView.Properties.ContainsKey("ValueTemplate"))
			{
                SomethingChanged?.Invoke(this, EventArgs.Empty);
                if (e.DataView.Properties.ContainsKey("Variable"))
				{
                    selectedValueType = Utilities.ValueTypes.VariableCall;
                    var vari = new VariableCall();
					Values valuePlaceHolder = sender as Values;
					var grid = valuePlaceHolder.Parent as Grid;
					if (grid != null)
					{
						var row = Grid.GetRow(valuePlaceHolder);
						var column = Grid.GetColumn(valuePlaceHolder);
                        grid.Children.Remove(valuePlaceHolder);
                        grid.Children.Add(vari);
						Grid.SetRow(vari, row);
						Grid.SetColumn(vari, column);
					}
                    selectedValue = vari as VariableCall;
				}
				if (e.DataView.Properties.ContainsKey("Function"))
				{
                    selectedValueType = Utilities.ValueTypes.FunctionCall;
                    var func = new FunctionCall();
					Values valuePlaceHolder = sender as Values;
					var grid = valuePlaceHolder.Parent as Grid;
					if (grid != null)
					{
						var row = Grid.GetRow(valuePlaceHolder);
						var column = Grid.GetColumn(valuePlaceHolder);
                        grid.Children.Remove(valuePlaceHolder);
                        grid.Children.Add(func);
						Grid.SetRow(func, row);
						Grid.SetColumn(func, column);
					}
                    selectedValue = func as FunctionCall;
                }
				if (e.DataView.Properties.ContainsKey("NumberConstant"))
				{
                    selectedValueType = Utilities.ValueTypes.NumberConstant;
                    var constantNumber = new ConstantNumber();
					Values valuePlaceHolder = sender as Values;
					var parent = valuePlaceHolder.Parent as Grid;
					if (parent != null)
					{
						parent = parent as Grid;
						var row = Grid.GetRow(valuePlaceHolder);
						var column = Grid.GetColumn(valuePlaceHolder);
						parent.Children.Remove(valuePlaceHolder);
						parent.Children.Add(constantNumber);
						Grid.SetRow(constantNumber, row);
						Grid.SetColumn(constantNumber, column);
					}
                    selectedValue = constantNumber as ConstantNumber;
                }
				if (e.DataView.Properties.ContainsKey("TextConstant"))
				{
                    selectedValueType = Utilities.ValueTypes.TextConstant;
                    var constantText = new ConstantText();
					Values valuePlaceHolder = sender as Values;
					var grid = valuePlaceHolder.Parent as Grid;
					if (grid != null)
					{
						var row = Grid.GetRow(valuePlaceHolder);
						var column = Grid.GetColumn(valuePlaceHolder);
                        grid.Children.Remove(valuePlaceHolder);
                        grid.Children.Add(constantText);
						Grid.SetRow(constantText, row);
						Grid.SetColumn(constantText, column);
					}
                    selectedValue = constantText as ConstantText;
                }
				if (e.DataView.Properties.ContainsKey("BooleanConstant"))
				{
                    selectedValueType = Utilities.ValueTypes.BooleanConstant;
                    var constantBoolean = new ConstantBoolean();
					Values valuePlaceHolder = sender as Values;
					var grid = valuePlaceHolder.Parent as Grid;
					if (grid != null)
					{
						var row = Grid.GetRow(valuePlaceHolder);
						var column = Grid.GetColumn(valuePlaceHolder);
                        grid.Children.Remove(valuePlaceHolder);
                        grid.Children.Add(constantBoolean);
						Grid.SetRow(constantBoolean, row);
						Grid.SetColumn(constantBoolean, column);
					}
                    selectedValue = constantBoolean as ConstantBoolean;
                }
				if (e.DataView.Properties.ContainsKey("MathExpression"))
				{
                    selectedValueType = Utilities.ValueTypes.MathExpression;
                    var mathExp = new MathExpression();
					Values valuePlaceHolder = sender as Values;
					var grid = valuePlaceHolder.Parent as Grid;
					if (grid != null)
					{
						var row = Grid.GetRow(valuePlaceHolder);
						var column = Grid.GetColumn(valuePlaceHolder);
                        grid.Children.Remove(valuePlaceHolder);
                        grid.Children.Add(mathExp);
						Grid.SetRow(mathExp, row);
						Grid.SetColumn(mathExp, column);
					}
                    selectedValue = mathExp as MathExpression;
                }
				if (e.DataView.Properties.ContainsKey("BooleanExpression"))
				{
                    selectedValueType = Utilities.ValueTypes.BooleanExpression;
                    var boolExp = new BooleanExpression();
					Values valuePlaceHolder = sender as Values;
					var grid = valuePlaceHolder.Parent as Grid;
					if (grid != null)
					{
						var row = Grid.GetRow(valuePlaceHolder);
						var column = Grid.GetColumn(valuePlaceHolder);
                        grid.Children.Remove(valuePlaceHolder);
                        grid.Children.Add(boolExp);
						Grid.SetRow(boolExp, row);
						Grid.SetColumn(boolExp, column);
					}
                    selectedValue = boolExp as BooleanExpression;
                }
				if (e.DataView.Properties.ContainsKey("ParenthesisExpression"))
				{
					selectedValueType = Utilities.ValueTypes.Parenthesis;
					var parExp = new Parenthesis();
					Values valuePlaceHolder = sender as Values;
					var grid = valuePlaceHolder.Parent as Grid;
					if (grid != null)
					{
						var row = Grid.GetRow(valuePlaceHolder);
						var column = Grid.GetColumn(valuePlaceHolder);
						grid.Children.Remove(valuePlaceHolder);
						grid.Children.Add(parExp);
						Grid.SetRow(parExp, row);
						Grid.SetColumn(parExp, column);
					}
					selectedValue = parExp as Parenthesis;
				}
				if (e.DataView.Properties.ContainsKey("AssetProperty"))
				{
                    selectedValueType = Utilities.ValueTypes.AssetProperty;
                    var assetProp = new AssetAttribute();
					Values valuePlaceHolder = sender as Values;
					var grid = valuePlaceHolder.Parent as Grid;
					if (grid != null)
					{
						var row = Grid.GetRow(valuePlaceHolder);
						var column = Grid.GetColumn(valuePlaceHolder);
                        grid.Children.Remove(valuePlaceHolder);
                        grid.Children.Add(assetProp);
						Grid.SetRow(assetProp, row);
						Grid.SetColumn(assetProp, column);
					}
                    selectedValue = assetProp as AssetAttribute;
                }
				if (e.DataView.Properties.ContainsKey("AssetFunction"))
				{
                    selectedValueType = Utilities.ValueTypes.AssetFunction;
                    var assetFunc = new AssetFunction();
					Values valuePlaceHolder = sender as Values;
					var grid = valuePlaceHolder.Parent as Grid;
					if (grid != null)
					{
						var row = Grid.GetRow(valuePlaceHolder);
						var column = Grid.GetColumn(valuePlaceHolder);
                        grid.Children.Remove(valuePlaceHolder);
                        grid.Children.Add(assetFunc);
						Grid.SetRow(assetFunc, row);
						Grid.SetColumn(assetFunc, column);
					}
                    selectedValue = assetFunc as AssetFunction;
                }
			}
		}

        /// <summary>
        /// Event invoked when a Value block is dropped into this UserControl.
        /// Accepts the Drag operation to allow dropping. Updates the event's
        /// caption and glyph.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void Values_OnDragOver(object sender, DragEventArgs e)
		{
			e.AcceptedOperation = DataPackageOperation.Copy;

			if (e.DataView.Properties.ContainsKey("ValueTemplate"))
			{
				// Change the background of the target
				e.DragUIOverride.Caption = "Drop value here.";
				e.DragUIOverride.IsGlyphVisible = false;
				e.DragUIOverride.IsCaptionVisible = true;
			}
			else
			{
				e.DragUIOverride.Caption = "";
				e.DragUIOverride.IsCaptionVisible = false;
				e.DragUIOverride.IsGlyphVisible = false;
				e.DragUIOverride.IsContentVisible = false;
			}
		}

        /// <summary>
        /// Event invoked when a drag operation leaves the values field.
        /// Removes the drag event's caption.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void Values_OnDragLeave(object sender, DragEventArgs e)
		{
			e.DragUIOverride.IsCaptionVisible = false;
		}

        /// <summary>
        /// Updates the placeholder text on the Value block.
        /// </summary>
        /// <param name="s"></param>
		public void SetChangeText(string s)
		{
			DragAValueHere.Text = s;
		}

        /// <summary>
        /// Adds to <see cref="Utilities.linesOfCode"/> the code generated by this block.
        /// Called by blocks that contain this UserControl.
        /// </summary>
		public void PrintCode()
        {
            if (selectedValueType == Utilities.ValueTypes.AssetFunction)
            {
                ((AssetFunction)selectedValue).PrintCode();
            }
            else if (selectedValueType == Utilities.ValueTypes.AssetProperty)
            {
                ((AssetAttribute)selectedValue).PrintCode();
            }
            else if (selectedValueType == Utilities.ValueTypes.BooleanConstant)
            {
                ((ConstantBoolean)selectedValue).PrintCode();
            }
            else if (selectedValueType == Utilities.ValueTypes.BooleanExpression)
            {
                ((BooleanExpression)selectedValue).PrintCode();
            }
			else if (selectedValueType == Utilities.ValueTypes.Parenthesis)
			{
				((Parenthesis)selectedValue).PrintCode();
			}
			else if (selectedValueType == Utilities.ValueTypes.FunctionCall)
            {
                ((FunctionCall)selectedValue).PrintCode();
            }
            else if (selectedValueType == Utilities.ValueTypes.MathExpression)
            {
                ((MathExpression)selectedValue).PrintCode();
            }
            else if (selectedValueType == Utilities.ValueTypes.NumberConstant)
            {
                ((ConstantNumber)selectedValue).PrintCode();
            }
            else if (selectedValueType == Utilities.ValueTypes.TextConstant)
            {
                ((ConstantText)selectedValue).PrintCode();
            }
            else if (selectedValueType == Utilities.ValueTypes.VariableCall)
            {
                ((VariableCall)selectedValue).PrintCode();
            }
        }
	}
}
