using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Marbles
{
    public sealed partial class TabControlAssetsButton : UserControl
    {
        private Point lastPositionClicked;

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(string), typeof(TabControlAssetsButton), null);

        public string ImageSource
        {
            get { return GetValue(ImageSourceProperty) as string; }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(TabControlAssetsButton), null);

        public string Label
        {
            get { return GetValue(LabelProperty) as string; }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty DragStartingProperty = DependencyProperty.Register("DragStartingId", typeof(string), typeof(TabControlAssetsButton), new PropertyMetadata(""));

        public string DragStartingId
        {
            get { return GetValue(DragStartingProperty) as string; }
            set { SetValue(DragStartingProperty, value); }
        }

        public static readonly DependencyProperty GroupProperty = DependencyProperty.Register("Group", typeof(string), typeof(TabControlAssetsButton), new PropertyMetadata(""));

        public string Group
        {
            get { return GetValue(GroupProperty) as string; }
            set { SetValue(GroupProperty, value); }
        }        

        public TabControlAssetsButton()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        private void TabControlAssetsButton_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            args.Data.Properties.Add(Group, sender);
            args.Data.Properties.Add("action", DragStartingId);
            args.Data.Properties.Add("xClicked", (int)(lastPositionClicked.X));
            args.Data.Properties.Add("yClicked", (int)(lastPositionClicked.Y));
		}

        private void MenuItem_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            lastPositionClicked = e.GetCurrentPoint(relativeTo: MenuItem).Position;
        }
    }
}
