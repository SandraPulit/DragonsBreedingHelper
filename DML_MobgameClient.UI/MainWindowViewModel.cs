using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DML_MobgameClient.UI.MVVM.Utils;
using DML_MobgameClient.UI.Panels.Home;
using DML_MobgameClient.UI.Panels.HowToBreed;

namespace DML_MobgameClient.UI
{
    public class MainWindowViewModel : ObservableObject
    {
        private ICommand _changePageCommand;
        private IPageViewModel _currentPageViewModel;
        public IList<IPageViewModel> PageViewModels { get; }
        public MainWindowViewModel()
        {
            PageViewModels = new List<IPageViewModel> {new HomeViewModel(), new HowToBreedViewModel()};
            _currentPageViewModel = PageViewModels[0];
        }

        public ICommand ChangePageCommand
        {
            get
            {
                return _changePageCommand ?? (_changePageCommand = new RelayCommand(
                    p => ChangeViewModel(p as IPageViewModel),
                    p => p is IPageViewModel));
            }
        }

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

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if(!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(x => x == viewModel);
        }
    }
}