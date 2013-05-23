namespace SerializationRules.Entities
{
    public interface ISerializationDefinition
    {
        string Path { get; set; }
        string Filter { get; set; }
        bool Evaluate(ISerializableItem item);
    }
}