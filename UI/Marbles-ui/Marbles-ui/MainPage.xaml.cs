using System;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Marbles_ui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		public MainPage()
        {
            this.InitializeComponent();
        }

		private void TargetListView_Drop(object sender, DragEventArgs e)
		{
			VisualStateManager.GoToState(this, "Inside", true);
			Grid temporaryGrid = e.OriginalSource as Grid;
			TargetListView.Items.Add(temporaryGrid);
		}

		private void TargetListView_DragEnter(object sender, DragEventArgs e)
		{
			/// Change the background of the target
			VisualStateManager.GoToState(this, "Inside", true);
			e.DragUIOverride.Caption = "Drop here to insert.";
		}

		private void TargetListView_DragLeave(object sender, DragEventArgs e)
		{
			VisualStateManager.GoToState(this, "Outside", true);
		}

	}
}
