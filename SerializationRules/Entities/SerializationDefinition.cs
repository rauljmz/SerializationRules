using System.Linq;
using SerializationRules.Conditions;
using Sitecore.Data.Items;
using Sitecore.Rules;

namespace SerializationRules.Entities
{
    public class SerializationDefinition : ISerializationDefinition
    {

        public string Path { get; private set; }
 
        public string Filter { get; private set; }

        public bool Enabled { get; private set; }        

        public SerializationDefinition(IItem item)
        {
            Path = item["path"];
            Filter = item["filter"];
            Enabled = item["enabled"] == "1";
        }

        public bool Evaluate(ISerializableItem item)
        {
            var scDatabase = item.Database as ScDatabase;
            if (scDatabase == null) return false;
            if (!Enabled) return true;
            var rules = RuleFactory.ParseRules<SerializationRuleContext>(scDatabase.InnerDatabase, Filter);
            var context = new SerializationRuleContext{SerializationDefinition = this};
            item.InitializeSerializationRuleContext(context);
            return rules.Rules.All(rule => rule.Evaluate(context));
        }

    }
}