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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Marbles
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class NavBar : Page
	{
		public NavBar()
		{
			this.InitializeComponent();
		}

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
		}

		private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			switch (args.InvokedItem)
				{
					case "Canvas":
						ContentFrame.Navigate(typeof(CanvasView));
						break;

					case "Code":
						ContentFrame.Navigate(typeof(CodeView));
						break;
				}

		}

		private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
		{
				NavigationViewItem item = args.SelectedItem as NavigationViewItem;

				switch (item.Tag)
				{
					case "Canvas":
						ContentFrame.Navigate(typeof(CanvasView));
						break;

					case "Code":
						ContentFrame.Navigate(typeof(CodeView));
						break;
				}
		}
	}
}
