using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DML_MobgameClient.DomainViewModels.DragonsDomain;

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

        public Task<ObservableCollection<BreedingResult>> CalculateBreeding(Dragon parent1, Dragon parent2)
        {
            var breedingCalculatorProvider = new MobgameBreedingCalculatorProvider();
            var task = Task.Run(() => breedingCalculatorProvider.Breed(parent1, parent2));
            return task;
        }
    }
}
