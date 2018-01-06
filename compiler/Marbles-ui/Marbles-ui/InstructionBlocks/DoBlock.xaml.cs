﻿using System;
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

namespace Marbles
{
    public sealed partial class DoBlock : UserControl
    {
        public DoBlock()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Adds to <see cref="Utilities.linesOfCode"/> the code generated by this block.
        /// Called by blocks that contain this UserControl.
        /// </summary>
        public void PrintCode()
        {
			CleanPossibleError();
			Utilities.linesOfCode.Add(new CodeLine("do ", this, Utilities.linesOfCodeCount + 1));
            Utilities.linesOfCodeCount++;
			ValuesInputThis.PrintCode();
            ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += ";";
        }

        /// <summary>
        /// Returns <see cref="ValuesInputThis"/>.
        /// </summary>
        /// <returns>A <see cref="DragAValueHereContainer"/>.</returns>
		public DragAValueHereContainer GetValueContainer()
		{
			return ValuesInputThis;
		}

        /// <summary>
        /// Shows the error signal of this block.
        /// Called when an error is caused by the lines of code this block generated.
        /// </summary>
        /// <param name="errorColor"></param>
		public void SetError(Brush errorColor)
		{
			ErrorEllipseGrid.Padding = new Thickness(5.0);
			ErrorEllipse.Height = 10;
			ErrorEllipse.Width = 10;
			ErrorEllipse.Fill = errorColor;
		}

        /// <summary>
        /// Removes the error signal of this block.
        /// Called before compiling.
        /// </summary>
		public void CleanPossibleError()
		{
			ErrorEllipseGrid.Padding = new Thickness(0.0);
			ErrorEllipse.Height = 0;
			ErrorEllipse.Width = 0;
		}
	}
}
