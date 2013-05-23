using System;

namespace SerializationRules.Conditions
{
    using Sitecore.Rules;
    using Sitecore.Rules.Conditions;

    
    public class IsSerializedAndUpToDate<T> : WhenCondition<T> where T : RuleContext
    {
        public string Root { get; set; }
    
        protected override bool Execute(T ruleContext)
        {
            throw new NotImplementedException();
        }
    }
}