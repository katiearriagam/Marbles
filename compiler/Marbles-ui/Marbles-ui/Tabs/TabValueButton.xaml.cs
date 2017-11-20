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
	public sealed partial class TabValueButton : UserControl
	{
		public TabValueButton()
		{
			this.InitializeComponent();
			this.DataContext = this;
		}

		public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register("Glyph", typeof(string), typeof(TabControlCodeButton), null);

		public string Glyph
		{
			get { return GetValue(GlyphProperty) as string; }
			set { SetValue(GlyphProperty, value); }
		}

		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(TabControlCodeButton), null);

		public string Label
		{
			get { return GetValue(LabelProperty) as string; }
			set { SetValue(LabelProperty, value); }
		}

		public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register("BackgroundColor", typeof(string), typeof(TabControlCodeButton), null);

		public string BackgroundColor
		{
			get { return GetValue(BackgroundColorProperty) as string; }
			set { SetValue(BackgroundColorProperty, value); }
		}

		public static readonly DependencyProperty DragStartingProperty = DependencyProperty.Register("DragStartingId", typeof(string), typeof(TabControlCodeButton), new PropertyMetadata(""));

		public string DragStartingId
		{
			get { return GetValue(DragStartingProperty) as string; }
			set { SetValue(DragStartingProperty, value); }
		}

        /// <summary>
        /// Event invoked when the user drags a value from the rightmost sidebar.
        /// Adds the dragged value to argument data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
		private void TabValueButton_DragStarting(UIElement sender, DragStartingEventArgs args)
		{
			args.Data.Properties.Add(DragStartingId, sender);
		}
	}
}
