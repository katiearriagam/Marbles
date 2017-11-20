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
	public sealed partial class ConstantBoolean : UserControl
	{
		public ConstantBoolean()
		{
			this.InitializeComponent();
		}

        /// <summary>
        /// Adds to <see cref="Utilities.linesOfCode"/> the code generated by this block.
        /// Called by blocks that contain this UserControl.
        /// </summary>
        public void PrintCode()
        {
            ((CodeLine)Utilities.linesOfCode[Utilities.linesOfCodeCount-1]).content += " " + ((ComboBoxItem)(BooleanConstantComboBox.SelectedItem)).Content.ToString();
        }

        public static event EventHandler SomethingChanged;

        /// <summary>
        /// Event invoked when the selection of a boolean constant changes.
        /// Invokes <see cref="SomethingChanged"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BooleanConstantComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SomethingChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
