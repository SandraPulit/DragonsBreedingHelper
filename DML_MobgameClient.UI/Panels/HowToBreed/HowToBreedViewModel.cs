using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DML_MobgameClient.DomainViewModels.DragonsDomain;
using DML_MobgameClient.UI.MVVM.Utils;
using DML_MobgameClient.UI.ViewModels;

namespace DML_MobgameClient.UI.Panels.HowToBreed
{
    public class HowToBreedViewModel : ObservableObject, IPageViewModel
    {
        public HowToBreedViewModel()
        {
            DragonsViewModel = new DragonsViewModel();
        }

        public string Name => "How to breed";
        public DragonsViewModel DragonsViewModel { get; }

        public Dragon SelectedDragon { get; set; }

        public ObservableCollection<DragonRecipe> DragonRecipes { get; set; }

        public ICommand BreedButtonClicked => new RelayCommand(async p =>
        {
            var htbv = p as HowToBreedView;
            ((Storyboard)htbv?.FindResource("LoadingStoryboard1"))?.Begin();
            DragonRecipes = await DragonsViewModel.FindDragonsBreedingRecepture(SelectedDragon);
            OnPropertyChanged(nameof(DragonRecipes));
            ((Storyboard)htbv?.FindResource("LoadingStoryboard1"))?.Stop();
            ((Storyboard)htbv?.FindResource("LoadingStoryboard2"))?.Begin();
            if (DragonRecipes == null)
                MessageBox.Show($"Dragon {SelectedDragon.Name} is not breedable.");
        });
    }
}