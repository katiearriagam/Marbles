using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Marbles
{
    public static class Utilities
    {
        public enum ShapeTypes
        {
            Circle,
            Triangle,
            Square
        }

        public static BitmapImage BitmapFromPath(string path)
        {
            path = "ms-appx:" + path; // adapt to correct Uri format
            Uri imageUri = new Uri(path, UriKind.Absolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            return imageBitmap;
        }
    }
}
