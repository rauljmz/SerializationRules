using SerializationRules.Entities;

namespace SerializationRules.Providers
{
    public interface ISerializationProvider
    {
        void Serialize(ISerializableItem item);
        void Remove(ISerializableItem item, string oldParentId);
        bool IsSerialized(ISerializableItem item, SerializationDefinition definition);
       
    }
}