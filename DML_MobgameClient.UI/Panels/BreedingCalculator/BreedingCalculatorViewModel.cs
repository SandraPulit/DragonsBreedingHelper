using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DML_MobgameClient.DomainViewModels.DragonsDomain;
using DML_MobgameClient.UI.MVVM.Utils;
using DML_MobgameClient.UI.ViewModels;

namespace DML_MobgameClient.UI.Panels.BreedingCalculator
{
    class BreedingCalculatorViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Breeding Calculator";
        public DragonsViewModel DragonVM => new DragonsViewModel();
        public ObservableCollection<BreedingResult> BreedingResults { get; private set; }
        public Dragon SelectedDragon1 { get; set; }
        public Dragon SelectedDragon2 { get; set; }

        public ICommand BreedButtonClicked => new RelayCommand(p =>
        {
            BreedingResults = DragonVM.BreedDragons(SelectedDragon1, SelectedDragon2);
            OnPropertyChanged(nameof(BreedingResults));
        });
    }
}
