using System.Collections.ObjectModel;
using System.Windows.Input;
using DML_MobgameClient.DomainViewModels.DragonsDomain;
using DML_MobgameClient.UI.MVVM.Utils;
using DML_MobgameClient.UI.ViewModels;

namespace DML_MobgameClient.UI.Panels.HowToBreed
{
    public class HowToBreedViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "How to breed";
        public DragonsViewModel DragonsViewModel { get; } = new DragonsViewModel();

        public Dragon SelectedDragon { get; set; }

        public ObservableCollection<DragonRecipe> DragonRecipes { get; set; }

        public ICommand BreedButtonClicked => new RelayCommand(p =>
        {
            DragonRecipes = DragonsViewModel.FindDragonsBreedingRecepture(SelectedDragon);
            OnPropertyChanged(nameof(DragonRecipes));
        });
    }
}