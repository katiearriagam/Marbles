using System;
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

namespace Marbles
{
    public sealed partial class InstructionListView : UserControl
    {
		ScrollViewer scrollViewer;
		public static bool dropped;

		public InstructionListView()
        {
            this.InitializeComponent();
			dropped = false;
			SizeChanged += MainPage_SizeChanged;
		}

		void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			scrollViewer = VisualTreeHelper.GetChild(
			  VisualTreeHelper.GetChild(TargetListView, 0), 0) as ScrollViewer;

			// We can unsubscribe after we are done - not gonna be called any more
			SizeChanged -= MainPage_SizeChanged;
		}

		public void ListView_SuspendDragAndDrop()
		{
			this.CanDrag = false;
			this.TargetListView.CanDragItems = true;
			this.TargetListView.CanReorderItems = false;
			this.AllowDrop = false;
		}

		public void ListView_ResumeDragAndDrop()
		{
			this.CanDrag = false;
			this.TargetListView.CanDragItems = true;
			this.TargetListView.CanReorderItems = true;
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

				if (e.DataView.Properties.ContainsKey("AssignInstantiator"))
				{
					lv.Items.Insert(index, new Marbles.AssignBlock());
				}
				else if (e.DataView.Properties.ContainsKey("DoInstantiator"))
				{
					lv.Items.Insert(index, new Marbles.DoBlock());
				}
				else if (e.DataView.Properties.ContainsKey("ForInstantiator"))
				{
					lv.Items.Insert(index, new Marbles.ForBlock());
				}
				else if (e.DataView.Properties.ContainsKey("WhileInstantiator"))
				{
					lv.Items.Insert(index, new Marbles.WhileBlock());
				}
				else if (e.DataView.Properties.ContainsKey("IfInstantiator"))
				{
					lv.Items.Insert(index, new Marbles.IfBlock());
				}
				else if (e.DataView.Properties.ContainsKey("StopInstantiator"))
				{
					lv.Items.Insert(index, new Marbles.StopBlock());
				}
				else if (e.DataView.Properties.ContainsKey("ReturnStatement"))
				{
					lv.Items.Insert(index, new Marbles.ReturnBlock());
				}
				ListView_SuspendDragAndDrop();
				dropped = true;
			}
			this.TargetListView.CanReorderItems = true;
		}

		private void TargetListView_DragOver(object sender, DragEventArgs e)
		{
			dropped = false;
			ListView_ResumeDragAndDrop();
			e.AcceptedOperation = DataPackageOperation.Copy;
			if (e.DataView.Properties.ContainsKey("Instruction"))
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

		public ListView GetTargetListView()
		{
			return this.TargetListView;
		}

        public void PrintCode()
        {

        }
	}
}
