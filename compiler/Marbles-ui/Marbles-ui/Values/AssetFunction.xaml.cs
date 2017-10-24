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
	public sealed partial class AssetFunction : UserControl
	{
		public AssetFunction()
		{
			this.InitializeComponent();
		}

		private void MethodChosenChanged(object sender, SelectionChangedEventArgs e)
		{
            var list = sender as ComboBox;
            TextBox parameter = new TextBox()
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
            if (list != null && Parameters != null)
			{
				Parameters.Items.Clear();
				switch (list.SelectedIndex)
				{
					case 0:
						break;
					case 1:
						parameter.PlaceholderText = "Displacement in X";
						Parameters.Items.Add(parameter);
						break;
					case 2:
						parameter.PlaceholderText = "Displacement in Y";
						Parameters.Items.Add(parameter);
						break;
					case 3:
						parameter.PlaceholderText = "Degrees";
						Parameters.Items.Add(parameter);
						break;
					case 4:
                        parameter.PlaceholderText = "X";
                        TextBox parameterY = new TextBox()
                        {
                            FontFamily = new FontFamily("Segoe UI Light"),
                            FontWeight = FontWeights.Light,
                            HorizontalContentAlignment = HorizontalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center,
                            TextAlignment = TextAlignment.Center,
                            PlaceholderText = "Y",
                            Width = double.NaN,
                            InputScope = new InputScope
                            {
                                Names =
                                {
                                    new InputScopeName(InputScopeNameValue.Text)
                                }
                            }
                        };

						Parameters.Items.Add(parameter);
						Parameters.Items.Add(parameterY);
						break;
					default:
						break;
				}
			}
		}

        public void PrintCode()
        {
            ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += AssetID.Text + "." + ((ComboBoxItem)(AssetAction.SelectedItem)).Content.ToString() + "(";

            bool firstParam = true;
            foreach (TextBox parameter in Parameters.Items)
            {
                if (firstParam)
                {
                    ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += parameter.Text;
                    firstParam = false;
                }
                else
                {
                    ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += ", " + parameter.Text;
                }
            }

            ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += ")";
        }
	}
}
