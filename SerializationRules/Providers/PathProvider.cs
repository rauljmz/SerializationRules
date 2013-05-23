using SerializationRules.Entities;

namespace SerializationRules.Providers
{
    public class PathProvider : IPathProvider
    {
        private const string Extension = "item";

        protected virtual string GetRelativePath(ISerializableItem item)
        {
            return
                string.Concat(item.DatabaseName,
                              item.FullPath.Replace('/', '\\')
                              );
        }

        private string SanitiseRootPath(string root)
        {
            if (root == null) return root;
            return root.TrimEnd('\\');
        }

        public virtual string GetOldPath(ISerializableItem item, ISerializableItem oldParent, string root)
        {

            return string.Format("{0}\\{1}\\{2}.{3}", SanitiseRootPath(root), GetRelativePath(oldParent), item.Name, Extension);
        }

        public virtual string GetOldFolderPath(ISerializableItem item, ISerializableItem oldParent, string root)
        {
            return string.Format("{0}\\{1}\\{2}", SanitiseRootPath(root), GetRelativePath(oldParent), item.Name);
        }

        public virtual string GetFolderPath(ISerializableItem item, string root)
        {
            return string.Format("{0}\\{1}", SanitiseRootPath(root), GetRelativePath(item));
        }

        public virtual string GetPath(ISerializableItem item, string root)
        {
            return string.Format("{0}\\{1}.{2}", SanitiseRootPath(root), GetRelativePath(item), Extension);
        }

        


    }
}