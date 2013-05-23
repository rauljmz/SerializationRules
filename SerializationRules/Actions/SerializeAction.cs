using System.IO;
using SerializationRules.MovedItemRules;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Data.Serialization;
using Sitecore.Diagnostics;
using SerializationRules.Extensions;

namespace SerializationRules.Actions
{
    using Sitecore.Rules;
    using Sitecore.Rules.Actions;    

    public class SerializeAction<T> : RuleAction<T> where T : RuleContext
    {
        private const string FileExtension =".item";
        public string Root { get; set; }

        public override void Apply([NotNull] T ruleContext)
        {
            Sitecore.Diagnostics.Assert.ArgumentNotNull(ruleContext,"ruleContext");
            var item = ruleContext.Item;
            if (item == null)
            {
                Log.Info("Trying to serialize a null item", this);
                return;
            }

            item.Dump(Root);

            var movedContext = ruleContext as MovedItemRuleContext;
            if (movedContext != null)
            {
                RemoveOldFile(movedContext.GetParentItem(), item.Name);
            }
        }

      protected  virtual void RemoveOldFile(Item parentItem, string name)
        {            
            var fileInfo = new FileInfo(parentItem.GetChildSerializationPath(name,Root));
            if (fileInfo.Exists)
            {
                var directory = fileInfo.Directory;
                fileInfo.Delete();
                
                if (directory != null
                    && directory.Exists
                    && directory.GetFiles().Length == 0 
                    && directory.GetDirectories().Length == 0)
                {
                    directory.Delete();
                }
            }
        }
    }
}