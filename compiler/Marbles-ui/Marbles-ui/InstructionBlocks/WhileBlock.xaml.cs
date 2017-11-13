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
    public sealed partial class WhileBlock : UserControl
    {
        private Marbles.InstructionListView instructions;

        public WhileBlock()
        {
            this.InitializeComponent();
			instructions = new Marbles.InstructionListView();
			var grid = Container;
			grid.Children.Add(instructions);
			Grid.SetRow(instructions, 2);
		}

        public void PrintCode()
        {
			CleanPossibleError();
			Utilities.linesOfCode.Add(new CodeLine("while (", this, Utilities.linesOfCodeCount + 1));
            Utilities.linesOfCodeCount++;
            ValuesInput.PrintCode();
            ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += ") {";
            instructions.PrintCode();
            Utilities.linesOfCode.Add(new CodeLine("}", this, Utilities.linesOfCodeCount + 1));
            Utilities.linesOfCodeCount++;
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
