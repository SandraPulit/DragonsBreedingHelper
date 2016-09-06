using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using DML_MobgameClient.DomainViewModels.DragonsDomain;
using HtmlAgilityPack;

namespace DML_MobgameClient.DataProvider
{
    public class MobgameBreedingCalculatorProvider
    {
        private HtmlDocument _source;
        private void Init(Dragon dragon1, Dragon dragon2)
        {
            _source = new HtmlDocument { OptionUseIdAttribute = true };
            var request = (HttpWebRequest)WebRequest.Create(
                $"http://mobga.me/dragon-mania-legends/breeding-calculator/{dragon1.Name.Replace(' ', '-').ToLower()}-and-{dragon2.Name.Replace(' ', '-').ToLower()}/");
            request.Method = "GET";
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                    _source.Load(stream);
            }
        }
        public ObservableCollection<BreedingResult> Breed(Dragon parent1, Dragon parent2)
        {
            Init(parent1, parent2);

            var childs = _source.DocumentNode.SelectNodes(".//*[@id='hatchery']/ul/li");
            if (childs == null)
                return null;
            var breedingResults = new ObservableCollection<BreedingResult>();
            var dragonsProvider = new MobgameDragonsProvider();
            var nfi = new NumberFormatInfo {NumberDecimalSeparator = "."};
            foreach (var child in childs)
            {
                var name = child.SelectSingleNode(".//div/span[1]").InnerText;
                var oddsUnformatted = double.Parse(child.SelectSingleNode(".//div/span[2]").InnerText, nfi);
                var odds = $"{Math.Round(oddsUnformatted, 2)}%";
                var breedingTimeInSeconds = int.Parse(child.SelectSingleNode(".//div/span[3]").InnerText.Split('.')[0]);
                var breedingTime = PrepareTimeAsString(breedingTimeInSeconds);
                var tmp = child.SelectSingleNode(".//div/span[4]").InnerText;
                var expectedTimeInSeconds = int.Parse(child.SelectSingleNode(".//div/span[4]").InnerText.Split('.')[0]);
                var expectedTime = PrepareTimeAsString(expectedTimeInSeconds);
                var childDragon = dragonsProvider.GetDragonByName(name);
                breedingResults.Add(new BreedingResult(childDragon, breedingTime, odds, expectedTime));
            }
            return breedingResults;
        }

        private string PrepareTimeAsString(int breedingTimeInSeconds)
        {
            var days = breedingTimeInSeconds/(60*60*24);
            breedingTimeInSeconds -= days*60*60*24;
            var hours = breedingTimeInSeconds/(60*60);
            breedingTimeInSeconds -= hours*60*60;
            var minutes = breedingTimeInSeconds/60;
            return days > 0 ? $"{days}d {hours}h {minutes}m" : $"{hours}h {minutes}m";
        }
    }
}