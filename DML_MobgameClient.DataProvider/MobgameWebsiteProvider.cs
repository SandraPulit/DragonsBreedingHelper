using System.Collections.Generic;
using System.IO;
using System.Net;
using DML_MobgameClient.DomainViewModels.DragonsDomain;
using HtmlAgilityPack;
using System.Drawing;

namespace DML_MobgameClient.DataProvider
{
    public class MobgameWebsiteProvider
    {
        private HtmlDocument _source;

        public IList<Dragon> Dragons => GetDragonsFromSource();

        public void Init()
        {
            _source = new HtmlDocument { OptionUseIdAttribute = true };
            var request = (HttpWebRequest)WebRequest.Create("http://mobga.me/dragon-mania-legends/how-to-breed/");
            request.Method = "GET";
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using(var stream = response.GetResponseStream())
                    _source.Load(stream);
            }
        }

        private IList<Dragon> GetDragonsFromSource()
        {
            var dragonsList = new List<Dragon>();

            foreach (var dragonHtmlNode in
                _source.DocumentNode.SelectNodes(@".//*[@id='dragonMenu']/ul/li/a/div[@class='info']"))
            {
                var dragonElements = new List<Element>();

                foreach (var elementHtmlNode in
                    dragonHtmlNode.SelectNodes(".//div/div/img"))
                {
                    var request = (HttpWebRequest)WebRequest.Create(elementHtmlNode.Attributes["src"].Value);
                    request.Method = "GET";
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            Image img = Image.FromStream(stream);
                            var elementAlt = elementHtmlNode.Attributes["alt"].Value;
                            var elementName = elementAlt.Split(' ')[3];
                            dragonElements.Add(new Element(elementName, img));
                        }
                    }
                }
                var dragonName = dragonHtmlNode.SelectNodes(".//div/span[@class='listDragonName']")[0].InnerHtml;
                var dragon = new Dragon(dragonName, dragonElements);
                dragonsList.Add(dragon);
            }

            return dragonsList;
        }
    }
}
