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
            var DataType = new ComboBox
            {
                FontFamily = new FontFamily("Segoe UI Light"),
                FontWeight = FontWeights.Light,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, -10, 0),
                Width = double.NaN
            };

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

            Parameters.Items.Add(ParameterName);
		}

        public void PrintCode()
        {
            Debug.Write("function " + ((ComboBoxItem)(functionType.SelectedItem)).Content.ToString() + " " + functionID.Text + "(");

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
                        Debug.Write(", ");
                    }

                    Debug.Write(((ComboBoxItem)(((ComboBox)item).SelectedItem)).Content.ToString() + " ");
                }
                else
                {
                    Debug.Write(((TextBox)item).Text);
                }
            }
            Debug.WriteLine(") {");
            VariableListViewContainer.PrintCode();
            InstructionListViewContainer.PrintCode();
            Debug.WriteLine("}");
        }
    }
}
