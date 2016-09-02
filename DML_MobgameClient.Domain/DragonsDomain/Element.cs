using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DML_MobgameClient.DomainViewModels.DragonsDomain
{
    [DebuggerDisplay("Name = {Name}")]
    public class Element
    {
        private Element(string name, ImageSource icon)
        {
            Name = name;
            IconSource = icon;
        }

        public string Name { get; }
        public ImageSource IconSource { get; }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        private static BitmapSource LoadBitmap(Bitmap source)
        {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs = null;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, Int32Rect.Empty,
                   BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }
            return bs;
        }

        //private static BitmapSource GetBitmapSource(string resPath)
        //{
        //    //Stream myStream = myAssembly.GetManifestResourceStream(resPath);
        //    //Bitmap image = new Bitmap(myStream);
        //   // return LoadBitmap(image);
        //}

        public static Element Create(string type)
        {
            var rootPath = Directory.GetCurrentDirectory() + '/';
            switch (type)
            {
                case "earth":
                    return new Element("Earth",
                        new BitmapImage(new Uri(rootPath + "Resources/earth-element.png"))
                    );
                case "energy":
                    return new Element("Energy",
                        new BitmapImage(new Uri(rootPath + "Resources/energy-element.png"))
                    );
                case "fire":
                    return new Element("Fire",
                        new BitmapImage(new Uri(rootPath + "Resources/fire-element.png"))
                    );
                case "legendary":
                    return new Element("Legendary",
                        new BitmapImage(new Uri(rootPath + "Resources/legendary-element.png"))
                    );
                case "light":
                    return new Element("Light",
                        new BitmapImage(new Uri(rootPath + "Resources/light-element.png"))
                    );
                case "metal":
                    return new Element("Metal",
                        new BitmapImage(new Uri(rootPath + "Resources/metal-element.png"))
                    );
                case "plant":
                    return new Element("Plant",
                        new BitmapImage(new Uri(rootPath + "Resources/plant-element.png"))
                    );
                case "shadow":
                    return new Element("Shadow",
                        new BitmapImage(new Uri(rootPath + "Resources/shadow-element.png"))
                    );
                case "void":
                    return new Element("Void",
                        new BitmapImage(new Uri(rootPath + "Resources/void-element.png"))
                    );
                case "water":
                    return new Element("Water",
                        new BitmapImage(new Uri(rootPath + "Resources/water-element.png"))
                    );
                case "wind":
                    return new Element("Wind",
                        new BitmapImage(new Uri(rootPath + "Resources/wind-element.png"))
                    );
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}