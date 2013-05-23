using SerializationRules.Entities;

namespace SerializationRules
{
    public interface ISerializationManager
    {
        void Serialize(ISerializableItem item);
        void Remove(ISerializableItem item, string oldParentId);
        bool IsSerialized(ISerializableItem item, SerializationDefinition definition);
       
    }
}