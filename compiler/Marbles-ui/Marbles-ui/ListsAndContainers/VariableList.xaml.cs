﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	public sealed partial class VariableList : UserControl
	{
		ScrollViewer scrollViewer;
		public static bool dropped;
        public static event EventHandler SomethingChanged;

        public VariableList()
        {
            this.InitializeComponent();
            dropped = false;
            SizeChanged += MainPage_SizeChanged;
        }

		void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			scrollViewer = VisualTreeHelper.GetChild(
			  VisualTreeHelper.GetChild(VariableListView, 0), 0) as ScrollViewer;

			// We can unsubscribe after we are done - not gonna be called any more
			SizeChanged -= MainPage_SizeChanged;
		}

		public void ListView_SuspendDragAndDrop()
		{
			this.CanDrag = false;
			this.VariableListView.CanDragItems = true;
			this.VariableListView.CanReorderItems = false;
			this.AllowDrop = false;
		}

		public void ListView_ResumeDragAndDrop()
		{
			this.CanDrag = false;
			this.VariableListView.CanDragItems = true;
			this.VariableListView.CanReorderItems = true;
			this.AllowDrop = true;
		}

		private void TargetListView_Drop(object sender, DragEventArgs e)
		{
			ListView lv = sender as ListView;
			int index = 0;
			if (!dropped)
			{
				// In the foreach loop we will check in which index (height intervals) from 0 to Count the new item was placed.
				// If it was not placed in any of these intervals, then it must have been placed below the last index.
				index = lv.Items.Count;

				Point pos = e.GetPosition(lv);
				pos.Y += scrollViewer.VerticalOffset;

				// Debug.WriteLine("X: " + pos.X + ", Y: " + pos.Y + ", offset: " + scrollViewer.VerticalOffset);
				int count = 0;
				int totalHeight = 0;

				foreach (UserControl item in lv.Items)
				{
					// Debug.WriteLine("Min: " + totalHeight + ", Max: " + (int)(totalHeight + item.ActualHeight));

					if (pos.Y > totalHeight && pos.Y < totalHeight + item.ActualHeight)
					{
						index = count;
						break;
					}

					totalHeight += (int)(item.ActualHeight);
					count++;
				}

				if (e.DataView.Properties.ContainsKey("CreateVariable"))
				{
					lv.Items.Insert(index, new Marbles.CreateVariable() );
                    SomethingChanged?.Invoke(this, EventArgs.Empty);
                }
				ListView_SuspendDragAndDrop();
				dropped = true;
			}
			this.VariableListView.CanReorderItems = true;
		}

		private void TargetListView_DragOver(object sender, DragEventArgs e)
		{
			dropped = false;
			ListView_ResumeDragAndDrop();
			e.AcceptedOperation = DataPackageOperation.Copy;
			if (e.DataView.Properties.ContainsKey("VariableTemplate"))
			{
				e.DragUIOverride.Caption = "";
				e.DragUIOverride.IsCaptionVisible = false;
				e.DragUIOverride.IsGlyphVisible = false;
				e.DragUIOverride.IsContentVisible = true;
			}
			else
			{
				e.DragUIOverride.Caption = "";
				e.DragUIOverride.IsCaptionVisible = false;
				e.DragUIOverride.IsGlyphVisible = false;
				e.DragUIOverride.IsContentVisible = true;
				this.ListView_SuspendDragAndDrop();
			}
		}

        public void PrintCode()
        {
            foreach (CreateVariable item in VariableListView.Items)
            {
                item.PrintCode();
            }
        }

        private void VariableListView_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            SomethingChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
