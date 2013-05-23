using SerializationRules.Entities;

namespace SerializationRules.Providers
{
    public interface ISerializationProvider
    {
        void Serialize(ISerializableItem item);
        void Remove(ISerializableItem item, ISerializableItem oldParentId);
        bool IsSerialized(ISerializableItem item, ISerializationDefinition definition);
       
    }
}