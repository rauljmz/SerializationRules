using System.IO;
using SerializationRules.MovedItemRules;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Data.Serialization;
using Sitecore.Diagnostics;

namespace SerializationRules.Actions
{
    using Sitecore.Rules;
    using Sitecore.Rules.Actions;
    using SerializationRules.Extensions;

    

    public class SerializeDeepAction<T> : SerializeAction<T> where T : RuleContext
    {
        public override void Apply([NotNull] T ruleContext)
        {
            Sitecore.Diagnostics.Assert.ArgumentNotNull(ruleContext,"ruleContext");
            var item = ruleContext.Item;
            if (item == null)
            {
                Log.Info("Trying to serialize a null item", this);
                return;
            }
            item.DumpTree(Root);
            
            var movedContext = ruleContext as MovedItemRuleContext;
            if (movedContext != null)
            {
                RemoveOldFile(movedContext.GetParentItem(), item.Name);
            }
        }

        

        protected override void RemoveOldFile(Item parentItem, string name)
        {
            var directoryInfo = new DirectoryInfo(parentItem.GetChildSerializationPath(name, Root,false));
            if (directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }
            base.RemoveOldFile(parentItem, name);
        }
    }
}