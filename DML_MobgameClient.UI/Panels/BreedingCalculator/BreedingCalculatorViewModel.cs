using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Animation;
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
            var bcv = p as BreedingCalculatorView;
            ((Storyboard)bcv?.FindResource("LoadingStoryboard1"))?.Begin();
            BreedingResults = await DragonVM.BreedDragons(SelectedDragon1, SelectedDragon2);
            OnPropertyChanged(nameof(BreedingResults));
            bcv?.BreedingResultsListBox.ScrollIntoView(bcv.BreedingResultsListBox.Items[0]);
            ((Storyboard)bcv?.FindResource("LoadingStoryboard1"))?.Stop();
            ((Storyboard)bcv?.FindResource("LoadingStoryboard2"))?.Begin();
        });
    }
}
