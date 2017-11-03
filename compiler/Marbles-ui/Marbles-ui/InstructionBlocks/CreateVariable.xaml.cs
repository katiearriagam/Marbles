using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	public sealed partial class CreateVariable : UserControl
	{
		public CreateVariable()
		{
			this.InitializeComponent();
		}

		public void PrintCode()
		{
            string dataTypeSelected = ((ComboBoxItem)(VariableType.SelectedItem)).Content.ToString();
            if (dataTypeSelected == "boolean")
            {
                dataTypeSelected = "bool";
            }
            Utilities.linesOfCode.Add(new CodeLine("var " + dataTypeSelected  + " " + VariableName.Text.ToString() + ";", this));
            Utilities.linesOfCodeCount++;
        }
	}
}
