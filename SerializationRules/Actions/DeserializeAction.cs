using Sitecore;
using Sitecore.Data.Serialization;
using Sitecore.Diagnostics;

namespace SerializationRules.Actions
{
    using Sitecore.Rules;
    using Sitecore.Rules.Actions;    

    public class DeserializeAction<T> : RuleAction<T> where T : RuleContext
    {
        
        public override void Apply([NotNull] T ruleContext)
        {
            Sitecore.Diagnostics.Assert.ArgumentNotNull(ruleContext, "ruleContext");
            if (ruleContext.Item == null)
            {
                Log.Info("Trying to serialize a null item", this);
                return;
            }
            
            Manager.LoadItem(ruleContext.Item.Paths.FullPath,new LoadOptions{Database = ruleContext.Item.Database,DisableEvents = false,ForceUpdate = false,UseNewID = false});
        }
    }
}