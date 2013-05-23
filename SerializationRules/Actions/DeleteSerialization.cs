using System.IO;
using System.Linq;
using SerializationRules.MovedItemRules;
using Sitecore;

namespace SerializationRules.Actions
{
    using Sitecore.Rules;
    using Sitecore.Rules.Actions;
    using SerializationRules.Extensions;
    

    public class DeleteSerialization<T> : RuleAction<T> where T : RuleContext
    {
        public string Root { get; set; }
        public override void Apply([NotNull] T ruleContext)
        {
            var movedContext = ruleContext as MovedItemRuleContext;

            var path = movedContext != null ? movedContext.GetParentItem().GetChildSerializationPath(ruleContext.Item.Name, Root) : ruleContext.Item.GetSerializationPath(Root);
            var fileinfo = new FileInfo(path);
            
            if (fileinfo.Exists)
            {
                if (fileinfo.Directory != null)
                {
                    var directory = fileinfo.Directory.GetDirectories(fileinfo.Name).FirstOrDefault();
                    if(directory != null && directory.Exists) directory.Delete(true);
                }
                fileinfo.Delete();
            }


        }
    }
}
