using System;
using System.Collections.Generic;
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
    public sealed partial class Values : UserControl
    {
		public Values()
        {
            this.InitializeComponent();
        }

		private void Values_OnDrop(object sender, DragEventArgs e)
		{
			e.AcceptedOperation = DataPackageOperation.Copy;

			if (e.DataView.Properties.ContainsKey("ConstantNumber"))
			{
				var constantNumber = new Marbles.ConstantNumber();
				Values valuePlaceHolder = sender as Values;
				var grid = valuePlaceHolder.Parent as Grid;
				if (grid != null)
				{
					var row = Grid.GetRow(valuePlaceHolder);
					var column = Grid.GetColumn(valuePlaceHolder);
					grid.Children.Add(constantNumber);
					Grid.SetRow(constantNumber, row);
					Grid.SetColumn(constantNumber, column);
					grid.Children.Remove(valuePlaceHolder);
				}
			}
		}

		private void Values_OnDragEnter(object sender, DragEventArgs e)
		{
			if (e.DataView.Properties.ContainsKey("ConstantNumber"))
			{
				// Change the background of the target
				e.DragUIOverride.Caption = "Drop value here.";
				e.DragUIOverride.IsCaptionVisible = true;
			}
		}

		private void Values_OnDragLeave(object sender, DragEventArgs e)
		{
			e.DragUIOverride.IsCaptionVisible = false;
		}
	}
}
