using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using DML_MobgameClient.DataProvider;
using DML_MobgameClient.DomainViewModels.DragonsDomain;
using DML_MobgameClient.UI.MVVM.Utils;

namespace DML_MobgameClient.UI.ViewModels
{
    public class DragonsViewModel : ObservableObject
    {
        private MobgameWebsiteProvider dataProvider;
        private string _filterText = string.Empty;

        public DragonsViewModel()
        {
            dataProvider = new MobgameWebsiteProvider();
            dataProvider.Init();
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

        private ObservableCollection<Dragon> Dragons => dataProvider.Dragons;
        public CollectionViewSource FilteredDragons1 { get; }

        public string FilterText1
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                FilteredDragons1.View.Refresh();
                OnPropertyChanged(nameof(FilteredDragons1));
            }
        }

        public CollectionViewSource FilteredDragons2 { get; }

        public string FilterText2
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                FilteredDragons2.View.Refresh();
                OnPropertyChanged(nameof(FilteredDragons2));
            }
        }
    }
}