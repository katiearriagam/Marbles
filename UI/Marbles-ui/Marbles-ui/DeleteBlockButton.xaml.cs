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
    public sealed partial class DeleteBlockButton : UserControl
    {
        public DeleteBlockButton()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var parent = VisualTreeHelper.GetParent(sender as Button);
            while (parent.GetType() != typeof(ListViewItem))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            ListView listView = ItemsControl.ItemsControlFromItemContainer(parent) as ListView;
            ListViewItem lvi = (parent as ListViewItem);
            
            listView.Items.Remove(lvi.Content);
        }
    }
}
