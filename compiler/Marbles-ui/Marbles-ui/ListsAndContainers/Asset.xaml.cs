using System;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Marbles
{
    public sealed partial class Asset : UserControl
    {
        private Canvas cv;
        private string id;
        private string imageSource;
        private string label;
        private double x, y;
        private int width, height;
        private int number;
        private int rotation;
        CompositeTransform ctImage = new CompositeTransform();
        CompositeTransform ctUserControl = new CompositeTransform();
        Storyboard sb = new Storyboard();
        DoubleAnimation anim = new DoubleAnimation();

        /// <summary>
        /// The variable's memory address
        /// </summary>
        private int memoryAddress;

		private Point lastPositionClicked;

        public Asset(string id, string imageSource, string label, int x, int y, int number, Canvas parentCanvas)
        {
            InitializeComponent();

            cv = parentCanvas;
            
            this.id = id;

            this.imageSource = imageSource;
            AssetImage.Source = Utilities.BitmapFromPath(imageSource);

            this.label = label;
            AssetLabel.Text = label;

            // The point (x,y) points to the top left point of the asset's image
            this.x = x;
            this.y = y;

            this.number = number;
            AssetNumber.Text = number.ToString();

            rotation = 0;

            width = (int)AssetImage.Width;
            height = (int)AssetImage.Height;

            // Set transform, rotate, and scaling's center point
            ctImage.CenterX = ctUserControl.CenterX = width / 2;
            ctImage.CenterY = ctUserControl.CenterY = height / 2;
            AssetImage.RenderTransform = ctImage;
            AssetUserControl.RenderTransform = ctUserControl;

            sb.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            sb.Children.Add(anim);
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

        public async Task SetWidth(int width)
        {
            int originalWidth = this.width;

            double widthRatio = width / 100.0;

            if (this.width * widthRatio < Utilities.assetMinimumWidth)
            {
                return;
            }

            AssetImage.RenderTransform = ctImage;
            Storyboard.SetTarget(anim, AssetImage);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.ScaleX)").Path);

            anim.From = 1;
            anim.To = widthRatio;
            anim.Duration = sb.Duration;

            await sb.BeginAsync();
            sb.Stop();

            this.width = (int)(this.width * widthRatio);
            AssetImage.Width = this.width;

            int pixelsAdded = this.width - originalWidth;

            double hyp = pixelsAdded / 2.0;
            double angleInRadians = rotation * (Math.PI / 180.0);
            double opp = Math.Sin(angleInRadians) * hyp;
            double adj = Math.Cos(angleInRadians) * hyp;

            x = x - adj;
            y = y - opp;

            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
            
            ctImage.CenterX = ctUserControl.CenterX = this.width / 2;
        }

        public int GetHeight()
        {
            return height;
        }

        public async Task SetHeight(int height)
        {
            int originalHeight= this.height;

            double heightRatio = height / 100.0;

            if (this.height * heightRatio < Utilities.assetMinimumHeight)
            {
                return;
            }

            AssetImage.RenderTransform = ctImage;
            Storyboard.SetTarget(anim, AssetImage);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.ScaleY)").Path);

            anim.From = 1;
            anim.To = heightRatio;
            anim.Duration = sb.Duration;

            await sb.BeginAsync();
            sb.Stop();

            this.height = (int)(this.height * heightRatio);
            AssetImage.Height = this.height;

            int pixelsAdded = this.height - originalHeight;

            double hyp = pixelsAdded / 2.0;
            double angleInRadians = 360 - rotation * (Math.PI / 180.0);
            double opp = Math.Sin(angleInRadians) * hyp;
            double adj = Math.Cos(angleInRadians) * hyp;

            x = x + adj;
            y = y - opp;

            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);

            ctImage.CenterY = ctUserControl.CenterY =  this.height / 2;
        }

		public double GetX()
		{
			return x;
		}

		public double GetY()
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

		public void SetPosition(double x, double y)
        {
            this.x = x;
            this.y = y;
            
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }

        public async Task MoveX(double displacement)
        {
            AssetUserControl.RenderTransform = ctUserControl;
            Storyboard.SetTarget(anim, AssetUserControl);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateX)").Path);

            anim.From = 0;
            anim.To = displacement;
            anim.Duration = sb.Duration;

            x = x + displacement;
            
            await sb.BeginAsync();
            Canvas.SetLeft(this, x);
            sb.Stop();
        }

        public async Task MoveY(double displacement)
        {
            AssetUserControl.RenderTransform = ctUserControl;
            Storyboard.SetTarget(anim, AssetUserControl);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.TranslateY)").Path);

            anim.From = 0;
            anim.To = displacement;
            anim.Duration = sb.Duration;

            y = y + displacement;

            await sb.BeginAsync();
            Canvas.SetTop(this, y);
            sb.Stop();
        }

        public async Task Turn(int degrees)
        {
            AssetImage.RenderTransform = ctImage;
            Storyboard.SetTarget(anim, AssetImage);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.Rotation)").Path);

            anim.From = rotation;
            anim.To = rotation + degrees;

            degrees = degrees % 360;

            rotation = (rotation + degrees) % 360;
            ctImage.Rotation = rotation;
            
            anim.Duration = sb.Duration;

            await sb.BeginAsync();
            sb.Stop();
        }

        public async Task Spin()
        {
            AssetImage.RenderTransform = ctImage;
            Storyboard.SetTarget(anim, AssetImage);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(UIElement.RenderTransform).(CompositeTransform.Rotation)").Path);

            anim.From = rotation;
            anim.To = rotation + 360;
            anim.Duration = sb.Duration;

            await sb.BeginAsync();
            sb.Stop();
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

        public void SetPositionXAttribute(double newX)
        {
            SetPosition(newX, y);
        }

        public void SetPositionYAttribute(double newY)
        {
            SetPosition(x, newY);
        }
	}
}
