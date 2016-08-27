using System.Collections.Generic;
using System.Diagnostics;

namespace DML_MobgameClient.DomainViewModels.DragonsDomain
{
    [DebuggerDisplay("Name = {Name}, Elements = {Elements.Count}")]
    public class Dragon
    {
        public Dragon(string name, IList<Element> elements)
        {
            Name = name;
            Elements = elements;
        }

        public string Name { get; }
        public IList<Element> Elements { get; }

    }
}