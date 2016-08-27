using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DML_MobgameClient.DomainViewModels.DragonsDomain
{
    public class Element
    {
        public Element(string name, System.Drawing.Image icon)
        {
            Name = name;
            Icon = new Image();
           // Icon.Source = new DrawingImage;
        }

        public string Name { get; }
        public Image Icon { get; }
    }
}