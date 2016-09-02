using System.Collections.ObjectModel;
using System.Net;
using DML_MobgameClient.DomainViewModels.DragonsDomain;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace DML_MobgameClient.DataProvider
{
    internal class MobgameDragonsRecipeProvider
    {
        private HtmlDocument _source;
        private void Init(Dragon selectedDragon)
        {
            _source = new HtmlDocument { OptionUseIdAttribute = true };
            var request = (HttpWebRequest)WebRequest.Create($"http://mobga.me/dragon-mania-legends/how-to-breed-{selectedDragon.Name.Replace(' ','-').ToLower()}/");
            request.Method = "GET";
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                    _source.Load(stream);
            }
        }

        public ObservableCollection<DragonRecipe> GetFormula(Dragon selectedDragon)
        {
            Init(selectedDragon);

            var dragonsRecipeList = new ObservableCollection<DragonRecipe>();
            var dragonsProvider = new MobgameDragonsProvider();

            foreach (var recipe in _source.DocumentNode.SelectNodes(".//*[@id='page']/div/div/div/section/div[2]/div[div[contains(@class, 'info')]]"))
            {
                var firstParent = recipe.SelectSingleNode(".//div[2]/div[1]/strong/a[1]").InnerText;
                var secondParent = recipe.SelectSingleNode(".//div[2]/div[1]/strong/a[2]").InnerText;
                var probability = recipe.SelectSingleNode(".//div[2]/div[2]/span[2]").InnerText;
                var expectedTime = recipe.SelectSingleNode(".//div[2]/div[3]/span[2]").InnerText;

                var firstDragon = dragonsProvider.GetDragonByName(firstParent);
                var secondDragon = dragonsProvider.GetDragonByName(secondParent);

                dragonsRecipeList.Add(new DragonRecipe(firstDragon, secondDragon, probability, expectedTime));
            }
            return dragonsRecipeList;
        }
    }
}
