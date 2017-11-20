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

namespace Marbles
{
	public sealed partial class DragAValueHereContainer : UserControl
	{
		public DragAValueHereContainer()
		{
			this.InitializeComponent();
		}

        /// <summary>
        /// Adds to <see cref="Utilities.linesOfCode"/> the code generated by this block.
        /// Called by blocks that contain this UserControl.
        /// </summary>
		public void PrintCode()
		{
			values.PrintCode();
		}

        /// <summary>
        /// Returns a <see cref="Values"/> control.
        /// </summary>
        /// <returns></returns>
		public Values GetValuesList()
		{
			return values;
		}
	}
}
