using System.Linq;
using SerializationRules.Conditions;
using Sitecore.Data.Items;
using Sitecore.Rules;

namespace SerializationRules.Entities
{
    public class SerializationDefinition
    {

        public string Path { get; set; }
 
        public string Filter { get; set; }

        public SerializationDefinition(string path, string filter)
        {
            Path = path;
            Filter = filter;
        }

        public SerializationDefinition(Item item)
        {
            Path = item["path"];
            Filter = item["filter"];
        }

        public bool Evaluate(ISerializableItem item)
        {
            var rules = RuleFactory.ParseRules<SerializationRuleContext>(item.Database, Filter);
            var context = new SerializationRuleContext{SerializationDefinition = this};
            item.InitializeSerializationRuleContext(context);
            return rules.Rules.All(rule => rule.Evaluate(context));
        }

    }
}