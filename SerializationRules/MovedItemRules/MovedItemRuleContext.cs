using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;
using Sitecore.Rules;
using Sitecore.Data.Items;

namespace SerializationRules.MovedItemRules
{
    public class MovedItemRuleContext : RuleContext
    {
        public ID OriginalParent { get; set; }

        public Item GetParentItem()
        {
            return Item.Database.GetItem(OriginalParent);
        }
    }
}