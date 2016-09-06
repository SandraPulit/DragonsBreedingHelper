using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using DML_MobgameClient.DomainViewModels.DragonsDomain;
using DML_MobgameClient.UI.MVVM.Utils;
using DML_MobgameClient.UI.ViewModels;

namespace DML_MobgameClient.UI.Panels.BreedingCalculator
{
    internal class BreedingCalculatorViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Breeding Calculator";
        // ReSharper disable once InconsistentNaming
        public DragonsViewModel DragonVM { get; }
        public ObservableCollection<BreedingResult> BreedingResults { get; private set; }
        public Dragon SelectedDragon1 { get; set; }
        public Dragon SelectedDragon2 { get; set; }

        public BreedingCalculatorViewModel()
        {
            DragonVM = new DragonsViewModel();
        }

        public ICommand BreedButtonClicked => new RelayCommand(async p =>
        {
            BreedingResults = await DragonVM.BreedDragons(SelectedDragon1, SelectedDragon2);
            OnPropertyChanged(nameof(BreedingResults));
            var lb = p as ListBox;
            lb?.ScrollIntoView(lb.Items[0]);
        });
    }
}
