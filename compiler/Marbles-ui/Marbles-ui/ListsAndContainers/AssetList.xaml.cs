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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Marbles
{
	public sealed partial class AssetList : UserControl
	{
		public AssetList()
		{
			this.InitializeComponent();
			PrintCode();
		}

		public void UpdateAssets()
		{
			AssetListView.Items.Clear();
			foreach (Asset itemAsset in Utilities.assetsInCanvas)
			{
				AssetListView.Items.Add(new CreateAsset(itemAsset.GetID()));
			}
		}
		public void PrintCode()
		{
            foreach (CreateAsset item in AssetListView.Items)
			{
				item.PrintCode();
			}
		}
	}
}
