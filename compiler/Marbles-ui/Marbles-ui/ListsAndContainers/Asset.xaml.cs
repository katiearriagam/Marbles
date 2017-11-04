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
    public sealed partial class Asset : UserControl
    {
        private Canvas cv;
        private string id;
        private string imageSource;
        private string label;
        private int x, y;
        private int width, height;
        private int number;
        private int rotation;

		/// <summary>
		/// The variable's memory address
		/// </summary>
		private int memoryAddress;

		private Point lastPositionClicked;

        public Asset(string id, string imageSource, string label, int x, int y, int number, Canvas parentCanvas)
        {
            this.InitializeComponent();

            cv = parentCanvas;
            
            this.id = id;

            this.imageSource = imageSource;
            AssetImage.Source = Utilities.BitmapFromPath(imageSource);

            this.label = label;
            AssetLabel.Text = label;

            this.x = (int)AssetImage.Width; // distance to the leftmost part of the canvas
            this.y = (int)AssetImage.Height; // distance to the top of the canvas

            this.number = number;
            AssetNumber.Text = number.ToString();

            rotation = 0;

            width = Utilities.assetInitialWidth;
            height = Utilities.assetInitialHeight;
        }

        public string ImageSource
        {
            get { return imageSource; }
        }

        public string Label
        {
            get { return label; }
        }

        public string Number
        {
            get { return number.ToString(); }
        }

        public string GetID()
        {
            return id;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

		public int GetX()
		{
			return x;
		}

		public int GetY()
		{
			return y;
		}

		public int GetRotation()
		{
			return rotation;
		}

		public int GetNumber()
		{
			return number;
		}

		public string GetLabel()
		{
			return label;
		}

		public void SetPosition(int x, int y)
        {
            bool outOfBounds = false;
            if (x < 0)
            {
                outOfBounds = true;
                x = 1;
            }

            if (x + (int)AssetImage.ActualWidth >= cv.ActualWidth)
            {
                outOfBounds = true;
                x = (int)cv.ActualWidth - (int)AssetImage.ActualWidth - 1;
            }

            if (y < 0)
            {
                outOfBounds = true;
                y = 1;
            }

            if (y + (int)AssetImage.ActualHeight >= cv.ActualHeight)
            {
                outOfBounds = true;
                y = (int)cv.ActualHeight - (int)AssetImage.ActualHeight - 1;
            }

            if (outOfBounds)
            {
                Canvas.SetLeft(this, x);
                Canvas.SetTop(this, y);
                return;
            }
            
            this.x = x;
            this.y = y;
            
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }

        public void MoveX(int displacement)
        {
            int newX = x + displacement;

            if (newX < 0)
            {
                x = 1;
                Canvas.SetLeft(this, x);
                return;
            }

            if (newX + (int)AssetImage.ActualWidth >= cv.ActualWidth)
            {
                x = (int)cv.ActualWidth - (int)AssetImage.ActualWidth - 1;
                Canvas.SetLeft(this, x);
                return;
            }

            x = newX;

            Canvas.SetLeft(this, x);
        }

        public void MoveY(int displacement)
        {
            int newY = y + displacement;

            if (newY < 0)
            {
                y = 1;
                Canvas.SetTop(this, y);
                return;
            }

            if (newY + (int)AssetImage.ActualHeight >= cv.ActualHeight)
            {
                y = (int)cv.ActualHeight - (int)AssetImage.ActualHeight - 1;
                Canvas.SetTop(this, y);
                return;
            }

            y = newY;

            Canvas.SetTop(this, y);
        }

        public void Turn(int degrees)
        {
            degrees = degrees % 360;
            rotation = (rotation + degrees) % 360;
        }

        public void SetNumber(int number)
        {
            this.number = number;
            AssetNumber.Text = number.ToString();
        }

        public void SetLabel(string label)
        {
            this.label = label;
            AssetLabel.Text = label;
        }

        private void UserControl_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            // sender: Marbles.Asset
            var asset = sender as Marbles.Asset;
            args.Data.Properties.Add("assetDragged", asset);
            args.Data.Properties.Add("xClicked", (int)(lastPositionClicked.X));
            args.Data.Properties.Add("yClicked", (int)(lastPositionClicked.Y));
        }


        private void UserControl_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // sender: Marbles.Asset
            lastPositionClicked = e.GetCurrentPoint(relativeTo: AssetUserControl).Position;
        }

		/// <summary>
		/// Sets or modifies the memory address value
		/// </summary>
		/// <param name="mem"></param>
		public void SetMemoryAddress(int mem)
		{
			memoryAddress = mem;
		}

		/// <summary>
		/// Retrieves the memory address
		/// </summary>
		public int GetMemoryAddress()
		{
			return memoryAddress;
		}
	}
}
