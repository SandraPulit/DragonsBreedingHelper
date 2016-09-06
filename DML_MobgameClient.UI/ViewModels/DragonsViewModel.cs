using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using DML_MobgameClient.DataProvider;
using DML_MobgameClient.DomainViewModels.DragonsDomain;
using DML_MobgameClient.UI.MVVM.Utils;

namespace DML_MobgameClient.UI.ViewModels
{
    public class DragonsViewModel : ObservableObject
    {
        private readonly MobgameWebsiteProvider _dataProvider;
        private string _filterText1 = string.Empty;
        private string _filterText2 = string.Empty;

        public DragonsViewModel()
        {
            _dataProvider = new MobgameWebsiteProvider(); //TODO: Globalne
            FilteredDragons1 = new CollectionViewSource {Source = Dragons};
            FilteredDragons1.Filter += (x, y) =>
            {
                var dragon = y.Item as Dragon;
                if (dragon != null) y.Accepted = FilterText1.Length == 0 
                    || dragon.Name.ToUpper().Contains(FilterText1.ToUpper())
                    || dragon.Elements.Any(e => e.Name.ToUpper().Contains(FilterText1.ToUpper()));
            };
            FilteredDragons2 = new CollectionViewSource { Source = Dragons };
            FilteredDragons2.Filter += (x, y) =>
            {
                var dragon = y.Item as Dragon;
                if (dragon != null) y.Accepted = FilterText2.Length == 0
                    || dragon.Name.ToUpper().Contains(FilterText2.ToUpper())
                    || dragon.Elements.Any(e => e.Name.ToUpper().Contains(FilterText2.ToUpper()));
            };
        }

        private ObservableCollection<Dragon> Dragons => _dataProvider.Dragons;
        public CollectionViewSource FilteredDragons1 { get; }

        public string FilterText1
        {
            get { return _filterText1; }
            set
            {
                _filterText1 = value;
                FilteredDragons1.View.Refresh();
                OnPropertyChanged(nameof(FilteredDragons1));
            }
        }

        public CollectionViewSource FilteredDragons2 { get; }

        public string FilterText2
        {
            get { return _filterText2; }
            set
            {
                _filterText2 = value;
                FilteredDragons2.View.Refresh();
                OnPropertyChanged(nameof(FilteredDragons2));
            }
        }

        public ObservableCollection<DragonRecipe> FindDragonsBreedingRecepture(Dragon selectedDragon)
        {
            return _dataProvider.DragonFormula(selectedDragon);
        }

        public Task<ObservableCollection<BreedingResult>> BreedDragons(Dragon selectedDragon1, Dragon selectedDragon2)
        {
            return _dataProvider.CalculateBreeding(selectedDragon1, selectedDragon2);
        }
    }
}