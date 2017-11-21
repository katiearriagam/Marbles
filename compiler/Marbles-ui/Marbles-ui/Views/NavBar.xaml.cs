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

namespace Marbles
{
	/// <summary>
	/// Page that controls the left-side navigation bar to change between views.
	/// </summary>
	public sealed partial class NavBar : Page
	{
		public NavBar()
		{
			this.InitializeComponent();
		}

        /// <summary>
        /// Event invoked when the current view loads.
        /// Sets the default view to <see cref="CanvasView"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void NavView_Loaded(object sender, RoutedEventArgs e)
		{
			// set the initial selected item to canvas view
			foreach (NavigationViewItemBase item in NavView.MenuItems)
			{
				if (item is NavigationViewItem && item.Tag.ToString() == "Canvas")
				{
					NavView.SelectedItem = item;
					break;
				}
			}

			Utilities.ChangedPageHeader += new EventHandler(ChangedPageHeader);
		}

        /// <summary>
        /// Event invoked when the page header changes.
        /// Updates the header of the current Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void ChangedPageHeader(object sender, EventArgs e)
		{
			NavView.Header = Utilities.PageHeader;
		}

        /// <summary>
        /// Event invoked when an item from the nav bar has been selected.
        /// Navigates to the selected view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
		private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			switch (args.InvokedItem)
				{
					case "Canvas":
						sender.Header = "Canvas";
						ContentFrame.Navigate(typeof(CanvasView));
						break;

					case "Code":
						sender.Header = "Code";
						ContentFrame.Navigate(typeof(CodeView));
						break;

					case "Errors":
						sender.Header = "Errors";
						ContentFrame.Navigate(typeof(ErrorView));
						break;

					case "Help":
						sender.Header = "Help";
						ContentFrame.Navigate(typeof(QuickReference));
						break;
			}

		}

        /// <summary>
        /// Event invoked when the nav bar selection has changed.
        /// Navigates to the view corresponding to the changed selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
		private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
		{
				NavigationViewItem item = args.SelectedItem as NavigationViewItem;

				switch (item.Tag)
				{
					case "Canvas":
						sender.Header = "Canvas";
						ContentFrame.Navigate(typeof(CanvasView));
						break;

					case "Code":
						sender.Header = "Code";
						ContentFrame.Navigate(typeof(CodeView));
						break;

					case "Errors":
						sender.Header = "Errors";
						ContentFrame.Navigate(typeof(ErrorView));
						break;

					case "Help":
						sender.Header = "Help";
						ContentFrame.Navigate(typeof(QuickReference));
						break;
			}
		}
	}
}
