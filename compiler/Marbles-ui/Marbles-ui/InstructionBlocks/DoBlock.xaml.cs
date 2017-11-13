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
    public sealed partial class DoBlock : UserControl
    {
        public DoBlock()
        {
            this.InitializeComponent();
        }

        public void PrintCode()
        {
			CleanPossibleError();
			Utilities.linesOfCode.Add(new CodeLine("do ", this, Utilities.linesOfCodeCount + 1));
            Utilities.linesOfCodeCount++;
			ValuesInputThis.PrintCode();
            ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += ";";
        }

		public DragAValueHereContainer GetValueContainer()
		{
			return this.ValuesInputThis;
		}

		public void SetError(Brush errorColor)
		{
			ErrorEllipseGrid.Padding = new Thickness(5.0);
			ErrorEllipse.Height = 10;
			ErrorEllipse.Width = 10;
			ErrorEllipse.Fill = errorColor;
		}

		public void CleanPossibleError()
		{
			ErrorEllipseGrid.Padding = new Thickness(0.0);
			ErrorEllipse.Height = 0;
			ErrorEllipse.Width = 0;
		}
	}
}
