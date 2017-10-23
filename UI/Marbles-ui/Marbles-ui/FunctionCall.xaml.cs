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

		private void AddParameter(object sender, RoutedEventArgs e)
		{
            TextBox parameter = new TextBox()
            {
                FontFamily = new FontFamily("Segoe UI Light"),
                FontWeight = FontWeights.Light,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                PlaceholderText = "Parameter Here",
                Width = double.NaN
            };

            Parameters.Items.Add(parameter);
		}

        public void PrintCode()
        {
            Debug.Write(FunctionNameTextBox.Text + "(");
            bool firstParam = true;
            foreach (TextBox tb in Parameters.Items)
            {
                if (firstParam)
                {
                    firstParam = false;
                    Debug.Write(tb.Text);
                }
                else
                {
                    Debug.Write(", " + tb.Text);
                }
            }
            Debug.Write(")");
        }
	}
}
