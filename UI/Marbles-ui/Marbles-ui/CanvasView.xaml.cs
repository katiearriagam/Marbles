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

        }

        private async void MainCanvas_Drop(object sender, DragEventArgs e)
        {
            cv = sender as Canvas;
            lastDropPosition = e.GetPosition(relativeTo: sender as Canvas);
            //if (e == null) return;
            //if (e.Data == null) return;
            //if (e.Data.Properties == null) return;

            if (e.DataView.Properties.ContainsKey("CircleInstantiator"))
            {
                assetToAddType = Utilities.ShapeTypes.Circle;
                ModalImage.Source = Utilities.BitmapFromPath("/Assets/circle.png");
            } else if (e.DataView.Properties.ContainsKey("TriangleInstantiator"))
            {
                assetToAddType = Utilities.ShapeTypes.Triangle;
                ModalImage.Source = Utilities.BitmapFromPath("/Assets/triangle.png");
            } else if (e.DataView.Properties.ContainsKey("SquareInstantiator"))
            {
                assetToAddType = Utilities.ShapeTypes.Square;
                ModalImage.Source = Utilities.BitmapFromPath("/Assets/square.png");
            }
            await Modal.ShowAsync();
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
            if (assetToAddType == Utilities.ShapeTypes.Circle)
            {
                Asset assetToAdd = new Asset(IDTextBox.Text, "/Assets/circle.png" , LabelTextBox.Text, (int)lastDropPosition.X, (int)lastDropPosition.Y, Convert.ToInt32(NumberTextBox.Text));
                
                Canvas.SetLeft(assetToAdd, (int)lastDropPosition.X);
                Canvas.SetTop(assetToAdd, (int)lastDropPosition.Y);
                cv.Children.Add(assetToAdd);
            }
            else if (assetToAddType == Utilities.ShapeTypes.Triangle)
            {
                Asset assetToAdd = new Asset(IDTextBox.Text, "/Assets/triangle.png", LabelTextBox.Text, (int)lastDropPosition.X, (int)lastDropPosition.Y, Convert.ToInt32(NumberTextBox.Text));
                Canvas.SetLeft(assetToAdd, (int)lastDropPosition.X);
                Canvas.SetTop(assetToAdd, (int)lastDropPosition.Y);
                cv.Children.Add(assetToAdd);
            }
            else if (assetToAddType == Utilities.ShapeTypes.Square)
            {
                Asset assetToAdd = new Asset(IDTextBox.Text, "/Assets/square.png", LabelTextBox.Text, (int)lastDropPosition.X, (int)lastDropPosition.Y, Convert.ToInt32(NumberTextBox.Text));
                Canvas.SetLeft(assetToAdd, (int)lastDropPosition.X);
                Canvas.SetTop(assetToAdd, (int)lastDropPosition.Y);
                cv.Children.Add(assetToAdd);
            }

            IDTextBox.Text = "";
            LabelTextBox.Text = "";
            NumberTextBox.Text = "";
        }

        private void MainCanvas_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
            e.DragUIOverride.IsCaptionVisible = false;
        }
    }
}
