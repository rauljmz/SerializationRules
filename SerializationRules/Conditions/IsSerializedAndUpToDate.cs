namespace SerializationRules.Conditions
{
    using Sitecore.Rules;
    using Sitecore.Rules.Conditions;
    using Extensions;
    
    public class IsSerializedAndUpToDate<T> : WhenCondition<T> where T : RuleContext
    {
        public string Root { get; set; }
    
        protected override bool Execute(T ruleContext)
        {
            return ruleContext.Item.IsSerializedCurrent(Root);            
        }
    }
}