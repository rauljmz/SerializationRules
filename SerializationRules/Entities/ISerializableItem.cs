using SerializationRules.Conditions;
using Sitecore.Data;

namespace SerializationRules.Entities
{
    public interface ISerializableItem
    {
        string FullPath { get; }
        string DatabaseName { get; }
        Database Database { get; }
        string Name { get; }
        void Dump(string root);
        void InitializeSerializationRuleContext(SerializationRuleContext serializationRuleContext);
    }
}