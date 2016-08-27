using System.Collections;
using System.Collections.Generic;
using DML_MobgameClient.DataProvider;
using DML_MobgameClient.DomainViewModels.DragonsDomain;
using DML_MobgameClient.UI.MVVM.Utils;

namespace DML_MobgameClient.UI.ViewModels
{
    public class DragonsViewModel : ObservableObject
    {
        private MobgameWebsiteProvider dataProvider;

        public DragonsViewModel()
        {
            dataProvider = new MobgameWebsiteProvider();
            dataProvider.Init();
        }

        public IList<Dragon> Dragons => dataProvider.Dragons;
    }
}