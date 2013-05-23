using SerializationRules.Conditions;
using Sitecore.Data.Items;
using Sitecore.Rules;
using SerializationRules.Extensions;

namespace SerializationRules.Entities
{
    public class SerializationDefinition
    {

        public string Path { get; set; }
 
        public string Filter { get; set; }

        public SerializationDefinition(string path, bool deep, string filter)
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
            var rules = RuleFactory.ParseRules<RuleContext>(item.Database, Filter);
            var context = new SerializationRuleContext{SerializationDefinition = this};
            item.InitializeSerializationRuleContext(context);
            return rules.Evaluate(context);
        }

    }
}