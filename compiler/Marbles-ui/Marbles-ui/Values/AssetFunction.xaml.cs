using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
	public sealed partial class AssetFunction : UserControl
	{
		public AssetFunction()
		{
			this.InitializeComponent();
		}

		private void MethodChosenChanged(object sender, SelectionChangedEventArgs e)
		{
			var list = sender as ComboBox;
			var parameter = new TextBox();
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
						var parameterY = new TextBox();
						parameterY.PlaceholderText = "Y"; ;
						Parameters.Items.Add(parameter);
						Parameters.Items.Add(parameterY);
						break;
					default:
						break;
				}
			}
		}
	}
}
