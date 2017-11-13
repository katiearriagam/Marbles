using Marbles.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Marbles
{
    /// <summary>
    /// View that contains a canvas where assets are manipulated, and buttons to navigate between
    /// different views.
    /// </summary>
    public sealed partial class CanvasView : Page
    {
        private Point lastDropPosition;
        private Utilities.ShapeTypes assetToAddType;
        private Canvas cv;
		
        public CanvasView()
        {
            InitializeComponent();
			NavigationCacheMode = NavigationCacheMode.Enabled;
            MainCanvas.Loaded += (s, e) => Clip(MainCanvas);
            MainCanvas.SizeChanged += (s, e) => Clip(MainCanvas);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Run_Button.Background = Utilities.RunButtonColor;
            Run_Button.IsEnabled = Utilities.RunButtonEnabled;
        }

        private async void MainCanvas_Drop(object sender, DragEventArgs e)
        {
            cv = sender as Canvas;

            // If we are dropping an asset that was already in the canvas, just reposition it
            if (e.DataView.Properties.ContainsKey("assetDragged"))
            {
                Asset assetDragged = e.DataView.Properties["assetDragged"] as Asset;
                int xClicked = (int)e.DataView.Properties["xClicked"];
                int yClicked = (int)e.DataView.Properties["yClicked"];

                int newXPosition = (int)e.GetPosition(relativeTo: cv).X - xClicked;
                int newYPosition = (int)e.GetPosition(relativeTo: cv).Y - yClicked;

                assetDragged.SetPositionNoAwait(newXPosition, newYPosition);
                Utilities.DisableRunButton();
                Run_Button.Background = Utilities.RunButtonColor;
                Run_Button.IsEnabled = Utilities.RunButtonEnabled;
                Utilities.BlueCompile();
            }
            else
            {
                // The asset was dragged from the menu, create a new one
                int lastDropX = (int)e.GetPosition(relativeTo: cv).X - (int)e.DataView.Properties["xClicked"];
                int lastDropY = (int)e.GetPosition(relativeTo: cv).Y - (int)e.DataView.Properties["yClicked"];
                lastDropPosition = new Point(lastDropX, lastDropY);

                assetToAddType = Utilities.actionToShapeType[e.DataView.Properties["action"] as string];
                ModalImage.Source = Utilities.BitmapFromPath(Utilities.shapeToImagePath[assetToAddType]);

                await Modal.ShowAsync();
            }
        }

        public int GetCanvasWidth()
        {
            return (int)MainCanvas.ActualWidth;
        }

        public int GetCanvasHeight()
        {
            return (int)MainCanvas.ActualHeight;
        }

        private void Modal_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
			// prevents the user from adding an empty ID
			if (IDTextBox.Text.Length == 0)
			{
				IDTextBox.Text = "";
				args.Cancel = true;
				IDTextBoxBorder.BorderThickness = new Thickness(2.0);
				return;
			}
			else
			{
				try
				{
					Convert.ToString(IDTextBox.Text);
					foreach (Asset a in Utilities.assetsInCanvas)
					{
						if (a.GetID() == IDTextBox.Text)
						{
							IDTextBox.Text = "";
							args.Cancel = true;
							IDTextBoxBorder.BorderThickness = new Thickness(2.0);
							return;
						}
					}
				}
				catch (Exception e)
				{
					IDTextBox.Text = "";
					args.Cancel = true;
					IDTextBoxBorder.BorderThickness = new Thickness(2.0);
					return;
				}
			}

			// prevents the user from adding an invalid string
			try
			{
				Convert.ToString(LabelTextBox.Text);
			}
			catch
			{
				args.Cancel = true;
				LabelTextBox.Text = "";
				LabelTextBoxBorder.BorderThickness = new Thickness(2.0);
				return;
			}
            
			// prevents the user from adding an invalid number
            try
            {
                Convert.ToInt32(NumberTextBox.Text);
            }
            catch (Exception e)
            {
				if (NumberTextBox.Text.Length != 0)
				{
					// The input number was not valid
					args.Cancel = true;
					NumberTextBox.Text = "";
					NumberTextBoxBorder.BorderThickness = new Thickness(2.0);
					return;
				}
            }

            Asset assetToAdd = new Asset(IDTextBox.Text, Utilities.shapeToImagePath[assetToAddType], LabelTextBox.Text, (int)lastDropPosition.X, (int)lastDropPosition.Y, Convert.ToInt32(NumberTextBox.Text.Length == 0 ? "0" : NumberTextBox.Text), cv);

            assetToAdd.SetPositionNoAwait((int)lastDropPosition.X, (int)lastDropPosition.Y);

            cv.Children.Add(assetToAdd);
			Utilities.assetsInCanvas.Add(assetToAdd);

            IDTextBox.Text = "";
            LabelTextBox.Text = "";
            NumberTextBox.Text = "";
			IDTextBoxBorder.BorderThickness = new Thickness(0.0);
			LabelTextBoxBorder.BorderThickness = new Thickness(0.0);
			NumberTextBoxBorder.BorderThickness = new Thickness(0.0);

            Utilities.DisableRunButton();
            Utilities.BlueCompile();
            Run_Button.Background = Utilities.RunButtonColor;
            Run_Button.IsEnabled = Utilities.RunButtonEnabled;
        }

        private void MainCanvas_DragOver(object sender, DragEventArgs e)
        {
            // Sender: Canvas
            Canvas canv = sender as Canvas;

            // current drag position
            int xDrag = (int)e.GetPosition(relativeTo: canv).X;
            int yDrag = (int)e.GetPosition(relativeTo: canv).Y;

            // dimensions of asset we're dragging
            int assetWidth = e.DataView.Properties.ContainsKey("assetDragged") ? (e.DataView.Properties["assetDragged"] as Asset).GetWidth() : Utilities.assetInitialWidth;
            int assetHeight = e.DataView.Properties.ContainsKey("assetDragged") ? (e.DataView.Properties["assetDragged"] as Asset).GetHeight() : Utilities.assetInitialHeight;

            // x and y displacement in which we clicked on (picked up) the asset
            int xClicked = (int)(e.DataView.Properties["xClicked"]);
            int yClicked = (int)(e.DataView.Properties["yClicked"]);
            
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
            e.DragUIOverride.IsGlyphVisible = false;

            e.DragUIOverride.IsCaptionVisible = false;
        }

        private void DeleteIcon_Drop(object sender, DragEventArgs e)
        {
            Utilities.BlueCompile();
            Utilities.DisableRunButton();
            Run_Button.Background = Utilities.RunButtonColor;
            Run_Button.IsEnabled = Utilities.RunButtonEnabled;

            // Delete the asset dropped
            Asset assetDragged = e.DataView.Properties["assetDragged"] as Asset;
            cv.Children.Remove(assetDragged);
			      Utilities.assetsInCanvas.Remove(assetDragged);
        }

        private void DeleteIcon_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
            e.DragUIOverride.IsGlyphVisible = false;
            e.DragUIOverride.IsCaptionVisible = false;
        }

		private void LabelTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			var textBox = sender as TextBox;
			if (textBox.Text != "")
			{
				if (textBox.Text[textBox.SelectionStart - 1] == '\"')
				{
					int pos = textBox.SelectionStart - 1;
					textBox.Text = textBox.Text.Remove(pos, 1);
					textBox.SelectionStart = pos;
				}
			}
		}

		private void IDTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			var textBox = sender as TextBox;
			if (textBox.Text != "")
			{
				if (textBox.SelectionStart == 1)
				{
					if (!(Char.IsLetter(textBox.Text[textBox.SelectionStart - 1]) ||
					textBox.Text[textBox.SelectionStart - 1] == '_'))
					{
						int pos = textBox.SelectionStart - 1;
						textBox.Text = textBox.Text.Remove(pos, 1);
						textBox.SelectionStart = pos;
					}
				}
				else
				{
					if (!(Char.IsLetterOrDigit(textBox.Text[textBox.SelectionStart - 1]) ||
					textBox.Text[textBox.SelectionStart - 1] == '_'))
					{
						int pos = textBox.SelectionStart - 1;
						textBox.Text = textBox.Text.Remove(pos, 1);
						textBox.SelectionStart = pos;
					}
				}
			}
		}
		private new void Clip(FrameworkElement element)
        {
            var clip = new RectangleGeometry { Rect = new Rect(0, 0, element.ActualWidth, element.ActualHeight) };
            element.Clip = clip;
        }

		private void Modal_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
		{
			IDTextBox.Text = "";
			LabelTextBox.Text = "";
			NumberTextBox.Text = "";
			IDTextBoxBorder.BorderThickness = new Thickness(0.0);
			LabelTextBoxBorder.BorderThickness = new Thickness(0.0);
			NumberTextBoxBorder.BorderThickness = new Thickness(0.0);
		}
    
		private async void Run_Button_Click(object sender, RoutedEventArgs e)
		{
            List<Tuple<int, int, int, int, int, int, string>> assetValues = new List<Tuple<int, int, int, int, int, int, string>>();

            foreach (UIElement child in cv.Children)
            {
                Asset c = child as Asset;
                assetValues.Add(new Tuple<int, int, int, int, int, int, string>(
                    c.GetX(), c.GetY(), c.GetWidth(), c.GetHeight(), c.GetRotation(), c.GetNumber(), c.GetLabel()));
            }

            try { await VirtualMachine.Execute(); }
			catch (Exception ex) { ErrorPrinter.AddError(ex.Message); }

            MemoryManager.PrintMemory(); // Print memory with all of its values set
            MemoryManager.RunReset(); // Reset memory to its original state before execution

            int attrCount = Enum.GetNames(typeof(MemoryManager.AssetAttributes)).Length;

            // Return assets in canvas to their original state before execution
            for (int i = 0; i < cv.Children.ToList().Count; i++)
            {
                Asset c = cv.Children[i] as Asset;

                MemoryManager.SetMemory((int)MemoryManager.AssetAttributes.x + (i * attrCount), assetValues[i].Item1);
                MemoryManager.SetMemory((int)MemoryManager.AssetAttributes.y + (i * attrCount), assetValues[i].Item2);
                c.SetPositionNoAwait(assetValues[i].Item1, assetValues[i].Item2);

                MemoryManager.SetMemory((int)MemoryManager.AssetAttributes.width + (i * attrCount), assetValues[i].Item3);
                MemoryManager.SetMemory((int)MemoryManager.AssetAttributes.height + (i * attrCount), assetValues[i].Item4);
                c.SetWidthNoAnimation(assetValues[i].Item3);
                c.SetHeightNoAnimation(assetValues[i].Item4);

                MemoryManager.SetMemory((int)MemoryManager.AssetAttributes.rotation + (i * attrCount), 0);
                c.SetRotationNoAnimation(0);

                MemoryManager.SetMemory((int)MemoryManager.AssetAttributes.number + (i * attrCount), assetValues[i].Item6);
                c.SetNumberNoWait(assetValues[i].Item6);

                MemoryManager.SetMemory((int)MemoryManager.AssetAttributes.label + (i * attrCount), assetValues[i].Item7);
                c.SetLabelNoWait(assetValues[i].Item7);
            }

            System.Threading.Thread.Sleep(1000);
        }
	}
}
