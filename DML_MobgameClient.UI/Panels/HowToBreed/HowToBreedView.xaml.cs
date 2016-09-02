using System.Windows.Controls;
using System.Windows.Media;

namespace DML_MobgameClient.UI.Panels.HowToBreed
{
    /// <summary>
    /// Logika interakcji dla klasy HowToBreedView.xaml
    /// </summary>
    public partial class HowToBreedView : UserControl
    {
        public HowToBreedView()
        {
            InitializeComponent();
        }
        #region BreedButton Transformations
        private void BreedButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button breedingButton = sender as Button;
            if (breedingButton != null) breedingButton.RenderTransform = new RotateTransform(13.0);
        }

        private void BreedButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button breedingButton = sender as Button;
            if (breedingButton != null) breedingButton.RenderTransform = null;
        }
        #endregion
    }
}
