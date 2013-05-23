using System;
using SerializationRules.Entities;
using SerializationRules.Providers;

namespace SerializationRules.Conditions
{
    using Sitecore.Rules;
    using Sitecore.Rules.Conditions;

    public class IsSerializedCondition<T> : WhenCondition<T> where T : SerializationRuleContext
    {
        public string Root { get; set; }
        protected override bool Execute(T ruleContext)
        {
           return new SerializationProvider().IsSerialized(new SerializableItem(ruleContext.Item),
                                                    ruleContext.SerializationDefinition);
        }
    }
}