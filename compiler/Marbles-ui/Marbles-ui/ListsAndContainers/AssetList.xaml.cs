using System;
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
	public sealed partial class AssetList : UserControl
	{
		public AssetList()
		{
			this.InitializeComponent();
		}

        /// <summary>
        /// Updates the <see cref="CreateAsset"/> blocks on the code view based on the
        /// assets present in the Canvas.
        /// Called by <see cref="CodeView.OnNavigatedTo(NavigationEventArgs)"/>.
        /// </summary>
		public void UpdateAssets()
		{
			Utilities.finalAssetsInCanvas.Clear();
			AssetListView.Items.Clear();
			foreach (Asset itemAsset in Utilities.assetsInCanvas)
			{
				AssetListView.Items.Add(new CreateAsset(itemAsset.GetID()));
				Utilities.finalAssetsInCanvas.Add(itemAsset);
			}
		}

        /// <summary>
        /// Calls the PrintCode() method for all <see cref="CreateAsset"/> items in <see cref="AssetListView"/>.
        /// </summary>
		public void PrintCode()
		{
            foreach (CreateAsset item in AssetListView.Items)
			{
				item.PrintCode();
			}
		}
	}
}
