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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Marbles
{
    public sealed partial class ConstantNumber : UserControl
    {
        public ConstantNumber()
        {
            this.InitializeComponent();
        }

        public void PrintCode()
        {
            ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += " " + ConstantNumberTextBox.Text;
        }

		private void ConstantNumber_TextChanged(object sender, TextChangedEventArgs e)
		{
			int helper;
			var textBox = sender as TextBox;
			if (!int.TryParse(textBox.Text, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture, out helper)
				&& textBox.Text != "")
			{
				int pos = textBox.SelectionStart - 1;
				textBox.Text = textBox.Text.Remove(pos, 1);
				textBox.SelectionStart = pos;
			}
		}
	}
}
