﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
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
    public sealed partial class ConstantNumber : UserControl
    {
        public ConstantNumber()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Adds to <see cref="Utilities.linesOfCode"/> the code generated by this block.
        /// Called by blocks that contain this UserControl.
        /// </summary>
        public void PrintCode()
        {
            ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += " " + ConstantNumberTextBox.Text;
        }

        public static event EventHandler SomethingChanged;

        /// <summary>
        /// Event invoked when the value of a numeric constant changes.
        /// Verifies that the number is valid and invokes <see cref="SomethingChanged"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConstantNumber_TextChanged(object sender, TextChangedEventArgs e)
		{

            SomethingChanged?.Invoke(this, EventArgs.Empty);
            var textBox = sender as TextBox;
			if (textBox.Text != "")
			{
				if (textBox.SelectionStart > 0 && (!(Char.IsDigit(textBox.Text[textBox.SelectionStart - 1]))))
				{
					int pos = textBox.SelectionStart - 1;
					textBox.Text = textBox.Text.Remove(pos, 1);
					textBox.SelectionStart = pos;
				}
			}
		}
	}
}
