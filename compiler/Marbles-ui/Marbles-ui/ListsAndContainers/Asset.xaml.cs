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
    /// <summary>
    /// Class describing an Asset used in the Canvas and that the user
    /// can interact with.
    /// </summary>
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

        /// <summary>
        /// The Asset's image source.
        /// </summary>
        public string ImageSource
        {
            get { return imageSource; }
        }

        /// <summary>
        /// The Asset's current label.
        /// </summary>
        public string Label
        {
            get { return label; }
        }

        /// <summary>
        /// The Asset's current value.
        /// </summary>
        public string Number
        {
            get { return number.ToString(); }
        }

        /// <summary>
        ///  Returns the Asset object's ID attribute.
        /// </summary>
        public string GetID()
        {
            return id;
        }

        /// <summary>
        /// Returns the Asset object's width attribute.
        /// </summary>
        public int GetWidth()
        {
            return width;
        }

        /// <summary>
        /// Set the Asset's width without animating the change.
        /// </summary>
        /// <param name="width"></param>
        public void SetWidthNoAnimation(int width)
        {
            this.width = width;
            AssetImage.Width = this.width;
            ctImage.CenterX = ctUserControl.CenterX = this.width / 2;
        }

        /// <summary>
        /// Set the Asset's width, animating the change.
        /// </summary>
        /// <param name="width"></param>
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

            x = x - (int)adj;
            y = y - (int)opp;

            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
            
            ctImage.CenterX = ctUserControl.CenterX = this.width / 2;
        }

        /// <summary>
        /// Returns the Asset object's height attribute.
        /// </summary>
        public int GetHeight()
        {
            return height;
        }

        /// <summary>
        /// Set the Asset's height without animating the change.
        /// </summary>
        /// <param name="height"></param>
        public void SetHeightNoAnimation(int height)
        {
            this.height= height;
            AssetImage.Height = this.height;
            ctImage.CenterY = ctUserControl.CenterY = this.height / 2;
        }

        /// <summary>
        /// Set the Asset's height, animating the change.
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
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

            x = x + (int)adj;
            y = y - (int)opp;

            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);

            ctImage.CenterY = ctUserControl.CenterY =  this.height / 2;
        }

        /// <summary>
        /// Returns the Asset object's X position.
        /// </summary>
		public int GetX()
		{
			return x;
		}

        /// <summary>
        /// Returns the Asset object's Y position.
        /// </summary>
		public int GetY()
		{
			return y;
		}

        /// <summary>
        /// Returns the Asset object's rotation attribute.
        /// </summary>
		public int GetRotation()
		{
			return rotation;
		}

        /// <summary>
        /// Returns the Asset object's Number attribute.
        /// </summary>
		public int GetNumber()
		{
			return number;
		}

        /// <summary>
        /// Returns the Asset object's Label attribute.
        /// </summary>
		public string GetLabel()
		{
			return label;
		}

        /// <summary>
        /// Sets the (x, y) position of an Asset.
        /// Used when we manually call this function within our code.
        /// Use <see cref="SetPosition"/> when the user sets the position through the UI's code blocks.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPositionNoAwait(int x, int y)
        {
            this.x = x;
            this.y = y;

            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }

        /// <summary>
        /// Sets the (x, y) position of an Asset.
        /// Use this only when the user sets the position throught the UI's code blocks.
        /// Otherwise, use <see cref="SetPositionNoAwait"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
		public async Task SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;

            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);

            sb.Stop(); // stop the storyboard in case it is running
            await Turn(0); // necessary for the SetPosition action to wait sb.Duration milliseconds afte completion
        }

        /// <summary>
        /// Move the horizontal position of the asset, animating the transition
        /// from one point in the Canvas to another.
        /// </summary>
        /// <param name="displacement"></param>
        public async Task MoveX(int displacement)
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

        /// <summary>
        /// Move the vertical position of the asset, animating the transition
        /// from one point in the Canvas to another.
        /// </summary>
        /// <param name="displacement"></param>
        public async Task MoveY(int displacement)
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

        /// <summary>
        /// Set the Asset's current rotation without animating the change.
        /// </summary>
        /// <param name="rotation"></param>
        public void SetRotationNoAnimation(int rotation)
        {
            this.rotation = rotation;
            ctImage.Rotation = rotation;
            AssetImage.RenderTransform = ctImage;
        }

        /// <summary>
        /// Rotate the asset a certain amount of degrees, animating the rotation.
        /// </summary>
        /// <param name="degrees"></param>
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

        public void SetNumberNoWait(int number)
        {
            this.number = number;
            AssetNumber.Text = number.ToString();
        }

        public async Task SetNumber(int number)
        {
            this.number = number;
            AssetNumber.Text = number.ToString();

            sb.Stop(); // stop the storyboard in case it is running
            await Turn(0); // necessary for the SetNumber action to wait sb.Duration milliseconds afte completion
        }

        public void SetLabelNoWait(string label)
        {
            this.label = label;
            AssetLabel.Text = label;
        }

        public async Task SetLabel(string label)
        {
            this.label = label;
            AssetLabel.Text = label;

            sb.Stop(); // stop the storyboard in case it is running
            await Turn(0); // necessary for the SetLabel action to wait sb.Duration milliseconds afte completion
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

        public async Task SetPositionXAttribute(int newX)
        {
            await SetPosition(newX, y);
        }

        public async Task SetPositionYAttribute(int newY)
        {
            await SetPosition(x, newY);
        }
	}
}
