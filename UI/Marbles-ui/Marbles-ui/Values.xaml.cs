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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Marbles
{
    public sealed partial class Values : UserControl
    {
        private Utilities.ValueTypes selectedValueType;
        private object selectedValue;

		public Values()
        {
            this.InitializeComponent();
        }

		private void Values_OnDrop(object sender, DragEventArgs e)
		{
			e.AcceptedOperation = DataPackageOperation.Copy;

			if (e.DataView.Properties.ContainsKey("ValueTemplate"))
			{
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
					var grid = valuePlaceHolder.Parent as Grid;
					if (grid != null)
					{
						var row = Grid.GetRow(valuePlaceHolder);
						var column = Grid.GetColumn(valuePlaceHolder);
                        grid.Children.Remove(valuePlaceHolder);
                        grid.Children.Add(constantNumber);
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

		private void Values_OnDragLeave(object sender, DragEventArgs e)
		{
			e.DragUIOverride.IsCaptionVisible = false;
		}

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
