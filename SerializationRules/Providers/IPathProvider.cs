using SerializationRules.Entities;

namespace SerializationRules.Providers
{
    public interface IPathProvider
    {
        string GetOldPath(ISerializableItem item, SerializableItem oldParent, string root);
        string GetOldFolderPath(ISerializableItem item, SerializableItem oldParent, string root);
        string GetFolderPath(ISerializableItem item, string root);
        string GetPath(ISerializableItem item, string root);
    }
}