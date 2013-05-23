using SerializationRules.Entities;

namespace SerializationRules.Providers
{
    public interface IPathProvider
    {
        string GetOldPath(ISerializableItem item, ISerializableItem oldParent, string root);
        string GetOldFolderPath(ISerializableItem item, ISerializableItem oldParent, string root);
        string GetFolderPath(ISerializableItem item, string root);
        string GetPath(ISerializableItem item, string root);
    }
}