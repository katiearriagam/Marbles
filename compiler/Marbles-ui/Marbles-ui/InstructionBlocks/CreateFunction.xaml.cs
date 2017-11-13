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
	public sealed partial class CreateFunction : UserControl
	{
		public CreateFunction()
		{
			this.InitializeComponent();
		}

        private void AddParameter(object sender, RoutedEventArgs e)
		{
            SomethingChanged?.Invoke(this, EventArgs.Empty);
            var DataType = new ComboBox
            {
                FontFamily = new FontFamily("Segoe UI Light"),
                FontWeight = FontWeights.Light,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, -10, 0),
                Width = double.NaN,
            };

            DataType.SelectionChanged += DataType_SelectionChanged;

            ComboBoxItem number = new ComboBoxItem
            {
                Content = "number",
                TabIndex = 0
            };
            ComboBoxItem text = new ComboBoxItem
            {
                Content = "text",
                TabIndex = 1
            };
            ComboBoxItem boolean = new ComboBoxItem
            {
                Content = "boolean",
                TabIndex = 2
            };

            DataType.Items.Add(number);
            DataType.Items.Add(text);
            DataType.Items.Add(boolean);

            DataType.SelectedIndex = 0;

            Parameters.Items.Add(DataType);

            var ParameterName = new TextBox
            {
                FontFamily = new FontFamily("Segoe UI Light"),
                FontWeight = FontWeights.Light,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                PlaceholderText = "Parameter Here",
                Width = double.NaN,
                InputScope = new InputScope
                {
                    Names =
                    {
                        new InputScopeName(InputScopeNameValue.Text)
                    }
                }
            };

            ParameterName.TextChanged += ParameterName_TextChanged;

            Parameters.Items.Add(ParameterName);
		}

        private void ParameterName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SomethingChanged?.Invoke(this, EventArgs.Empty);
        }

        private void DataType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SomethingChanged?.Invoke(this, EventArgs.Empty);
        }

        public void PrintCode()
        {
            string funcType = ((ComboBoxItem)(functionType.SelectedItem)).Content.ToString();
            if (funcType == "boolean") funcType = "bool";

            Utilities.linesOfCode.Add(new CodeLine("function " + funcType  + " " + functionID.Text + "(", this, Utilities.linesOfCodeCount + 1));
            Utilities.linesOfCodeCount++;
            bool firstParam = true;
            foreach (var item in Parameters.Items)
            {
                if (item.GetType() == typeof(ComboBox))
                {
                    if (firstParam)
                    {
                        firstParam = false;
                    }
                    else
                    {
                        ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += ", ";
                    }

                    string dataTypeSelected = ((ComboBoxItem)(((ComboBox)item).SelectedItem)).Content.ToString();

                    if (dataTypeSelected == "boolean")
                    {
                        dataTypeSelected = "bool";
                    }

                    ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += dataTypeSelected + " ";
                }
                else
                {
                    ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += ((TextBox)item).Text;
                }
            }

            ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += ") {";
            VariableListViewContainer.PrintCode();
            InstructionListViewContainer.PrintCode();
            Utilities.linesOfCode.Add(new CodeLine("}", this, Utilities.linesOfCodeCount + 1));
            Utilities.linesOfCodeCount++;
        }

		private void functionID_TextChanged(object sender, TextChangedEventArgs e)
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

        public static event EventHandler SomethingChanged;

        private void functionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SomethingChanged?.Invoke(this, EventArgs.Empty);
        }


    }
}
