using SerializationRules.Entities;

namespace SerializationRules.Providers
{
    public class PathProvider : IPathProvider
    {
        private const string Extension = "item";

        protected virtual string GetRelativePath(ISerializableItem item)
        {
            return
                string.Concat(item.Database.Name, "\\",
                              item.FullPath().Replace('/', '\\')
                              );
        }

        public virtual string GetOldPath(ISerializableItem item, SerializableItem oldParent, string root)
        {
            return string.Format("{0}\\{1}\\{2}.{3}", root, GetRelativePath(oldParent), item.Name, Extension);
        }

        public virtual string GetOldFolderPath(ISerializableItem item, SerializableItem oldParent, string root)
        {
            return string.Format("{0}\\{1}\\{2}", root, GetRelativePath(oldParent), item.Name);
        }

        public virtual string GetFolderPath(ISerializableItem item, string root)
        {
            return string.Format("{0}\\{1}", root, GetRelativePath(item));
        }

        public virtual string GetPath(ISerializableItem item, string root)
        {
            return string.Format("{0}\\{1}.{2}", root, GetRelativePath(item), Extension);
        }

        


    }
}