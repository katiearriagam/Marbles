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
    public sealed partial class Asset : UserControl
    {
        private CanvasView cv;
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

        public Asset(string id, string imageSource, string label, int x, int y, int number)
        {
            this.InitializeComponent();
            cv = new CanvasView();

            this.id = id;

            this.imageSource = imageSource;
            AssetImage.Source = Utilities.BitmapFromPath(imageSource);

            this.label = label;
            AssetLabel.Text = label;

            this.x = x;
            this.y = y;

            this.number = number;
            AssetNumber.Text = number.ToString();

            rotation = 0;

            width = Utilities.assetInitialWidth;
            height = Utilities.assetInitialHeight;
        }

        public string ImageSource
        {
            get { return this.imageSource; }
        }

        public string Label
        {
            get { return this.label; }
        }

        public string Number
        {
            get { return this.number.ToString(); }
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
			return (int)rotation;
		}

		public int GetNumber()
		{
			return (int)number;
		}

		public string GetLabel()
		{
			return label;
		}

		public void SetPosition(int x, int y)
        {
            if (x < 0 || y < 0 || x > cv.GetCanvasWidth() || y > cv.GetCanvasHeight())
            {
                return;
            }

            this.x = x;
            this.y = y;
        }

        public void MoveX(int displacement)
        {
            int newX = x + displacement;

            if (newX < 0 || newX > cv.GetCanvasWidth())
            {
                return;
            }

            x += displacement;
        }

        public void MoveY(int displacement)
        {
            int newY = y + displacement;

            if (newY < 0 || newY > cv.GetCanvasHeight())
            {
                return;
            }

            y += displacement;
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
