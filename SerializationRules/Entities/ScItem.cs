using System;
using System.Collections.Generic;
using Sitecore.Collections;
using Sitecore.Data.Items;
using System.Linq;

namespace SerializationRules.Entities
{
    public class ScItem : CustomItem, IItem
    {

        public ScItem(Item innerItem)
            : base(innerItem)
        {
            Name = innerItem.Name;
            Database = new ScDatabase(innerItem.Database);
        }

        public new string Name { get; set; }
        public new IDatabase Database { get; set; }
        public IItem GetItem(Guid id)
        {
            return Database.GetItem(id);
        }

        public IItem GetItem(string path)
        {
            return Database.GetItem(path);
        }

        public IEnumerable<IItem> Children
        {
            get
            {
                return InnerItem.GetChildren(ChildListOptions.SkipSorting).Select(i => new ScItem(i)).ToArray();
            }
        }
    }
}