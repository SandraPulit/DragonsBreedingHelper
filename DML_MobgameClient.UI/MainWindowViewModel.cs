using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DML_MobgameClient.UI.MVVM.Utils;
using DML_MobgameClient.UI.Panels.BreedingCalculator;
using DML_MobgameClient.UI.Panels.Home;
using DML_MobgameClient.UI.Panels.HowToBreed;

namespace DML_MobgameClient.UI
{
    public class MainWindowViewModel : ObservableObject
    {
        private IPageViewModel _currentPageViewModel;
        public IList<IPageViewModel> PageViewModels { get; }
        public MainWindowViewModel()
        {
            PageViewModels = new List<IPageViewModel>
            {
                new HomeViewModel(),
                new HowToBreedViewModel(),
                new BreedingCalculatorViewModel()
            };
            _currentPageViewModel = PageViewModels[0];
        }

        public ICommand ChangePageCommand => new RelayCommand(
            p => ChangeViewModel(p as string));

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel == value) return;
                _currentPageViewModel = value;
                OnPropertyChanged(nameof(CurrentPageViewModel));
            }
        }

        private void ChangeViewModel(string viewModel)
        {
            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(x => x.Name == viewModel);
        }
    }
}