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
    public sealed partial class DeleteBlockButton : UserControl
    {
        public DeleteBlockButton()
        {
            this.InitializeComponent();
        }

        public static event EventHandler SomethingChanged;

        /// <summary>
        /// Event invoked when the delete button of an instruction block is clicked.
        /// Gets the parent element of the instruction block and removes the block from its children list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SomethingChanged?.Invoke(this, EventArgs.Empty);
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
