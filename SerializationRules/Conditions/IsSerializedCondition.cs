using System.IO;
using SerializationRules.MovedItemRules;
using Sitecore.Data.Items;

namespace SerializationRules.Conditions
{
    using Sitecore.Rules;
    using Sitecore.Rules.Conditions;
    using SerializationRules.Extensions;    

    public class IsSerializedCondition<T> : WhenCondition<T> where T : RuleContext
    {
        public string Root { get; set; }
        protected override bool Execute(T ruleContext)
        {
            var movedContext = ruleContext as MovedItemRuleContext;

            var path = movedContext != null ? movedContext.GetParentItem().GetChildSerializationPath(ruleContext.Item.Name, Root) : ruleContext.Item.GetSerializationPath(Root);

            return new FileInfo(path).Exists;
        }
    }
}