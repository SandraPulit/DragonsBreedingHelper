using System.Collections.ObjectModel;
using System.Net;
using DML_MobgameClient.DomainViewModels.DragonsDomain;
using HtmlAgilityPack;

namespace DML_MobgameClient.DataProvider
{
    public class MobgameWebsiteProvider
    {
        public ObservableCollection<Dragon> Dragons => GetDragons();

        private ObservableCollection<Dragon> GetDragons()
        {
            var dragonsProvider = new MobgameDragonsProvider();
            dragonsProvider.Init();
            return dragonsProvider.GetDragonsList();
        }

        public ObservableCollection<DragonRecipe> DragonFormula(Dragon selectedDragon)
        {
            var recipeProvider = new MobgameDragonsRecipeProvider();
            return recipeProvider.GetFormula(selectedDragon);
        }
    }
}
