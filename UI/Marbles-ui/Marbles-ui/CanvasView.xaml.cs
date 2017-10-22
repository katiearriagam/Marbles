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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

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
            this.InitializeComponent();
        }

        private void FontIcon_Drop(object sender, DragEventArgs e)
        {
            // Delete the asset dropped
            Asset assetDragged = e.DataView.Properties["assetDragged"] as Asset;
            cv.Children.Remove(assetDragged);
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

                Canvas.SetLeft(assetDragged, e.GetPosition(relativeTo: cv).X - xClicked);
                Canvas.SetTop(assetDragged, e.GetPosition(relativeTo: cv).Y - yClicked);
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
            if (IDTextBox.Text.Length == 0)
            {
                args.Cancel = true;
                return;
            }

            Asset assetToAdd = new Asset(IDTextBox.Text, Utilities.shapeToImagePath[assetToAddType], LabelTextBox.Text, (int)lastDropPosition.X, (int)lastDropPosition.Y, Convert.ToInt32(NumberTextBox.Text.Length == 0 ? "0" : NumberTextBox.Text));
            Canvas.SetLeft(assetToAdd, (int)lastDropPosition.X);
            Canvas.SetTop(assetToAdd, (int)lastDropPosition.Y);
            cv.Children.Add(assetToAdd);
            
            IDTextBox.Text = "";
            LabelTextBox.Text = "";
            NumberTextBox.Text = "";
        }

        private void MainCanvas_DragOver(object sender, DragEventArgs e)
        {
            // Sender: Canvas
            Canvas canv = sender as Canvas;
            int xDrag = (int)e.GetPosition(relativeTo: canv).X;
            int yDrag = (int)e.GetPosition(relativeTo: canv).Y;
            int assetWidth = e.DataView.Properties.ContainsKey("assetDragged") ? (e.DataView.Properties["assetDragged"] as Asset).GetWidth() : Utilities.assetInitialWidth;
            int assetHeight = e.DataView.Properties.ContainsKey("assetDragged") ? (e.DataView.Properties["assetDragged"] as Asset).GetHeight() : Utilities.assetInitialHeight;
            int xClicked = (int)(e.DataView.Properties["xClicked"]);
            int yClicked = (int)(e.DataView.Properties["yClicked"]);
            if (xDrag + assetWidth - xClicked > canv.ActualWidth || yDrag + assetHeight - yClicked > canv.ActualHeight
                || xDrag - xClicked < 0 || yDrag - yClicked < 0)
            {
                e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.None;
                e.DragUIOverride.IsGlyphVisible = true;
            }
            else
            {
                e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
                e.DragUIOverride.IsGlyphVisible = false;
            }

            e.DragUIOverride.IsCaptionVisible = false;
        }

        private void FontIcon_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
            e.DragUIOverride.IsGlyphVisible = false;
            e.DragUIOverride.IsCaptionVisible = false;
        }
    }
}
