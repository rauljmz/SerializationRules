using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.Rules;

namespace SerializationRules.MovedItemRules
{
    public class RunRules
    {
        public string ParentFolderId { get; set; }
    

    public void ItemMoved(object sender, EventArgs args)
        {
            var item = Event.ExtractParameter<Item>(args, 0);
            var oldParentID = Event.ExtractParameter<ID>(args, 1);

            var context = new MovedItemRuleContext() {Item = item, OriginalParent = oldParentID};

            var parentItem = item.Database.GetItem(new ID(ParentFolderId));
            var rules = RuleFactory.GetRules<MovedItemRuleContext>(parentItem, "rule");

            rules.Run(context);

        }
    }
}