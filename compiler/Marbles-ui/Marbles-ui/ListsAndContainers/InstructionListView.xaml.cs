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
        public static event EventHandler SomethingChanged;

        public InstructionListView()
        {
            this.InitializeComponent();
			dropped = false;
			SizeChanged += MainPage_SizeChanged;
		}

        /// <summary>
        /// Event invoked when the size of the page changes.
        /// Gets the instruction list's scroll viewer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			scrollViewer = VisualTreeHelper.GetChild(
			  VisualTreeHelper.GetChild(TargetListView, 0), 0) as ScrollViewer;

			// We can unsubscribe after we are done - not gonna be called any more
			SizeChanged -= MainPage_SizeChanged;
		}

        /// <summary>
        /// Prevents drag and drop operations on this element.
        /// Called after dropping an element on the ListView to prevent parent
        /// elements from calling the same event.
        /// </summary>
		public void ListView_SuspendDragAndDrop()
		{
			this.CanDrag = false;
			this.TargetListView.CanDragItems = true;
			this.TargetListView.CanReorderItems = false;
			this.AllowDrop = false;
		}

        /// <summary>
        /// Enables drag and drop operations on this element.
        /// Called after dragging an element over the ListView.
        /// </summary>
		public void ListView_ResumeDragAndDrop()
		{
			this.CanDrag = false;
			this.TargetListView.CanDragItems = true;
			this.TargetListView.CanReorderItems = true;
			this.AllowDrop = true;
		}

        /// <summary>
        /// Event invoked when a Block is dropped on the ListView.
        /// Gets the index in which the element was dropped and inserts a new
        /// Instruction block in that position.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

				int count = 0;
				int totalHeight = 0;

				foreach (UserControl item in lv.Items)
				{
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
					lv.Items.Insert(index, new AssignBlock());
                    SomethingChanged?.Invoke(this, EventArgs.Empty);
                }
				else if (e.DataView.Properties.ContainsKey("DoInstantiator"))
				{
					lv.Items.Insert(index, new DoBlock());
                    SomethingChanged?.Invoke(this, EventArgs.Empty);
                }
				else if (e.DataView.Properties.ContainsKey("ForInstantiator"))
				{
					lv.Items.Insert(index, new ForBlock());
                    SomethingChanged?.Invoke(this, EventArgs.Empty);
                }
				else if (e.DataView.Properties.ContainsKey("WhileInstantiator"))
				{
					lv.Items.Insert(index, new WhileBlock());
                    SomethingChanged?.Invoke(this, EventArgs.Empty);
                }
				else if (e.DataView.Properties.ContainsKey("IfInstantiator"))
				{
					lv.Items.Insert(index, new IfBlock());
                    SomethingChanged?.Invoke(this, EventArgs.Empty);
                }
				else if (e.DataView.Properties.ContainsKey("StopInstantiator"))
				{
					lv.Items.Insert(index, new StopBlock());
                    SomethingChanged?.Invoke(this, EventArgs.Empty);
                }
				else if (e.DataView.Properties.ContainsKey("ReturnStatement"))
				{
					lv.Items.Insert(index, new ReturnBlock());
                    SomethingChanged?.Invoke(this, EventArgs.Empty);
                }
				ListView_SuspendDragAndDrop();
				dropped = true;
			}

			TargetListView.CanReorderItems = true;
		}

        /// <summary>
        /// Event invoked when a Block is dragged over the ListView.
        /// Accepts the drag operation to allow dropping.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
				ListView_SuspendDragAndDrop();
			}
		}

        /// <summary>
        /// Calls the PrintCode() function for each corresponding block.
        /// Called by blocks that contain this UserControl.
        /// </summary>
        public void PrintCode()
        {
            foreach (UserControl instruction in TargetListView.Items)
            {
                if (instruction.GetType() == typeof(DoBlock))
                {
                    ((DoBlock)instruction).PrintCode();
                }
                else if (instruction.GetType() == typeof(AssignBlock))
                {
                    ((AssignBlock)instruction).PrintCode();
                }
                else if (instruction.GetType() == typeof(CreateAsset))
                {
                    ((CreateAsset)instruction).PrintCode();
                }
                else if (instruction.GetType() == typeof(CreateFunction))
                {
                    ((CreateFunction)instruction).PrintCode();
                }
                else if (instruction.GetType() == typeof(CreateVariable))
                {
                    ((CreateVariable)instruction).PrintCode();
                }
                else if (instruction.GetType() == typeof(ForBlock))
                {
                    ((ForBlock)instruction).PrintCode();
                }
                else if (instruction.GetType() == typeof(IfBlock))
                {
                    ((IfBlock)instruction).PrintCode();
                }
                else if (instruction.GetType() == typeof(ReturnBlock))
                {
                    ((ReturnBlock)instruction).PrintCode();
                }
                else if (instruction.GetType() == typeof(StopBlock))
                {
                    ((StopBlock)instruction).PrintCode();
                }
                else if (instruction.GetType() == typeof(WhileBlock))
                {
                    ((WhileBlock)instruction).PrintCode();
                }
            }
        }

        /// <summary>
        /// Event invoked when a drag operation starts over this list view.
        /// Invokes the <see cref="SomethingChanged"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void TargetListView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            SomethingChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
