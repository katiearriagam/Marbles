using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
	public sealed partial class ErrorTemplate : UserControl
	{
		public ErrorTemplate()
		{
			this.InitializeComponent();
		}

		public void FillTemplate(UserControl block, List<string> errors, SolidColorBrush brush)
		{
			ErrorInBlockGrid.Background = brush;
			Label.Text = Utilities.MapBlockTypeToLabel(block);
			foreach (string error in errors)
			{
				var firstUpperCaseError = error;
				if (String.IsNullOrEmpty(error))
				{
					firstUpperCaseError = error.First().ToString().ToUpper() + error.Substring(1);
				}
				var errorText = new TextBlock
				{
					FontFamily = new FontFamily("Segoe UI Light"),
					FontWeight = FontWeights.Light,
					Foreground = new SolidColorBrush(Colors.White),
					HorizontalAlignment = HorizontalAlignment.Stretch,
					VerticalAlignment = VerticalAlignment.Center,
					TextAlignment = TextAlignment.Center,
					Text = "\u2022 " + firstUpperCaseError +  "\n"
				};
				ErrorListInBlock.Items.Add(errorText);
			}
		}
	}
}
