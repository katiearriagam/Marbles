﻿using System;
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
    public sealed partial class WhileBlock : UserControl
    {
        public WhileBlock()
        {
            this.InitializeComponent();
			var instructions = new Marbles.InstructionListView();
			var grid = Container;
			grid.Children.Add(instructions);
			Grid.SetRow(instructions, 2);
		}
    }
}