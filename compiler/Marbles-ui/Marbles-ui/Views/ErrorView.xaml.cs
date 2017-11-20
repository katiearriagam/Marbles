using Marbles.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    /// View that displays error messages.
    /// </summary>
    public sealed partial class ErrorView : Page
    {
		public ErrorView()
        {
            this.InitializeComponent();
			this.NavigationCacheMode = NavigationCacheMode.Enabled;
		}

        /// <summary>
        /// This function is an event called whenever the user changes from a different view
        /// to this view (ErrorView). Regenerates the latest list of errors found.
        /// </summary>
        /// <param name="e"></param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			ErrorViewList.Items.Clear();
			foreach (ErrorTemplate errorBlock in Utilities.errorsInLines)
			{
				ErrorViewList.Items.Add(errorBlock);
			}
		}
	}
}
