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
	public sealed partial class ForBlock : UserControl
	{
        private Marbles.InstructionListView instructions;

		public ForBlock()
		{
			this.InitializeComponent();
			instructions = new Marbles.InstructionListView();
			var grid = Container;
			grid.Children.Add(instructions);
			Grid.SetRow(instructions, 2);
		}

        public void PrintCode()
        {
            Utilities.linesOfCode.Add(new CodeLine("for (", this, Utilities.linesOfCodeCount + 1));
            Utilities.linesOfCodeCount++;
            ValuesInput.PrintCode();
            ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += ") loops {";
            instructions.PrintCode();
            Utilities.linesOfCode.Add(new CodeLine("}", this, Utilities.linesOfCodeCount + 1));
            Utilities.linesOfCodeCount++;
        }
	}
}
