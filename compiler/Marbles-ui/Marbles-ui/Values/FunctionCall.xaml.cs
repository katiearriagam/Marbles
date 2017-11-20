using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
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
	public sealed partial class FunctionCall : UserControl
	{
		public FunctionCall()
		{
			this.InitializeComponent();
		}

        public static event EventHandler SomethingChanged;


        private void AddParameter(object sender, RoutedEventArgs e)
		{
            SomethingChanged?.Invoke(this, EventArgs.Empty);

            Grid containerGrid = new Grid();
			Values newValue = new Values();
			containerGrid.Children.Add(newValue);

            Parameters.Items.Add(containerGrid);
		}

        public void PrintCode()
        {
            ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += " call " + FunctionNameTextBox.Text + "(";
            bool firstParam = true;
            foreach (Grid holder in Parameters.Items)
            {
				foreach (Object val in holder.Children)
				{
					if (firstParam)
					{
						firstParam = false;
						PrintParameterCode(val);
					}
					else
					{
						((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount - 1]).content += ", ";
						PrintParameterCode(val);
					}
				}
            }
            ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += ")";
        }

		private void PrintParameterCode(Object obj)
		{
			var varType = obj.GetType();
			if (varType == typeof(AssetFunction))
			{
				((AssetFunction)obj).PrintCode();
			}
			else if (varType == typeof(AssetAttribute))
			{
				((AssetAttribute)obj).PrintCode();
			}
			else if (varType == typeof(ConstantBoolean))
			{
				((ConstantBoolean)obj).PrintCode();
			}
			else if (varType == typeof(BooleanExpression))
			{
				((BooleanExpression)obj).PrintCode();
			}
			else if (varType == typeof(Parenthesis))
			{
				((Parenthesis)obj).PrintCode();
			}
			else if (varType == typeof(Negative))
			{
				((Negative)obj).PrintCode();
			}
			else if (varType == typeof(FunctionCall))
			{
				((FunctionCall)obj).PrintCode();
			}
			else if (varType == typeof(MathExpression))
			{
				((MathExpression)obj).PrintCode();
			}
			else if (varType == typeof(ConstantNumber))
			{
				((ConstantNumber)obj).PrintCode();
			}
			else if (varType == typeof(ConstantText))
			{
				((ConstantText)obj).PrintCode();
			}
			else if (varType == typeof(VariableCall))
			{
				((VariableCall)obj).PrintCode();
			}
		}

		private void FunctionNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
            SomethingChanged?.Invoke(this, EventArgs.Empty);

            var textBox = sender as TextBox;
			if (textBox.Text != "")
			{
				if (textBox.SelectionStart == 1)
				{
					if (!(Char.IsLetter(textBox.Text[textBox.SelectionStart - 1]) ||
					textBox.Text[textBox.SelectionStart - 1] == '_'))
					{
						int pos = textBox.SelectionStart - 1;
						textBox.Text = textBox.Text.Remove(pos, 1);
						textBox.SelectionStart = pos;
					}
				}
				else
				{
					if (!(Char.IsLetterOrDigit(textBox.Text[textBox.SelectionStart - 1]) ||
					textBox.Text[textBox.SelectionStart - 1] == '_'))
					{
						int pos = textBox.SelectionStart - 1;
						textBox.Text = textBox.Text.Remove(pos, 1);
						textBox.SelectionStart = pos;
					}
				}
			}
		}
	}
}
