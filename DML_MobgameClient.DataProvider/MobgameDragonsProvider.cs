using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using DML_MobgameClient.DomainViewModels.DragonsDomain;
using HtmlAgilityPack;

namespace DML_MobgameClient.DataProvider
{
    internal class MobgameDragonsProvider
    {
        private static HtmlDocument _source;
        private static bool _isInitialized;
        private bool _createdList;
        private static ObservableCollection<Dragon> _dragonsList;

        public void Init()
        {
            if(_isInitialized)
                return;
            _source = new HtmlDocument { OptionUseIdAttribute = true };
            var request = (HttpWebRequest)WebRequest.Create("http://mobga.me/dragon-mania-legends/how-to-breed/");
            request.Method = "GET";
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                    _source.Load(stream);
            }
            _isInitialized = true;
        }

        public ObservableCollection<Dragon> GetDragonsList()
        {
            if (_createdList) return _dragonsList;
            _dragonsList = new ObservableCollection<Dragon>();
            foreach (var dragonHtmlNode in
                _source.DocumentNode.SelectNodes(@".//*[@id='dragonMenu']/ul/li/a/div[@class='info']"))
            {
                var dragonElements = new ObservableCollection<Element>();

                foreach (var elementHtmlNode in
                    dragonHtmlNode.SelectNodes(".//div/div/img"))
                {
                    var elementStr = elementHtmlNode.Attributes["alt"].Value.Split(' ')[3];
                    dragonElements.Add(Element.Create(elementStr.ToLower()));
                }
                var dragonName = dragonHtmlNode.SelectNodes(".//div/span[@class='listDragonName']")[0].InnerHtml;
                var dragon = new Dragon(dragonName, dragonElements);
                _dragonsList.Add(dragon);
            }
            _createdList = true;
            return _dragonsList;
        }

        public Dragon GetDragonByName(string name)
        {
            Init();
            var ret = GetDragonsList().FirstOrDefault(d=>string.Equals(d.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (ret == null)
                return
                    GetDragonsList()
                        .FirstOrDefault(
                            d =>
                                string.Equals(d.Name, name.Replace(' ', '-'),
                                    StringComparison.CurrentCultureIgnoreCase));
            return ret;
        }
    }
}