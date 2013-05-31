namespace SerializationRules.Entities
{
    public interface ISerializationDefinition
    {
        string Path { get; }
        string Filter { get; }
        bool Enabled { get;  }
        bool Evaluate(ISerializableItem item);
    }
}