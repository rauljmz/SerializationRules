using System.IO;
using Sitecore.Data.Items;
using Sitecore.Data.Serialization;

namespace SerializationRules.Extensions
{
    public static class ItemExtensions
    {
        public const string FileExtension = ".item";

        public static string GetSerializationPath(this Item item, bool addExtension = true)
        {
            return GetSerializationPath(item, PathUtils.Root, addExtension);
        }

        public static string GetSerializationPath(this Item item, string root, bool addExtension = true)
        {
            var itemReference = new ItemReference(item);
            return PathUtils.GetDirectoryPath(itemReference.ToString() + (addExtension ? FileExtension : ""), root);
        }

        public static string GetChildSerializationPath(this Item item, string name, string root, bool addExtension = true)
        {
            var parentPath = item.GetSerializationPath(root,false);
            return string.Concat(parentPath, "\\", name, FileExtension);
        }

        public static void Dump(this Item item, string root)
        {
            Manager.DumpItem(item.GetSerializationPath(root), item);
        }

        public static void DumpTree(this Item item, string root)
        {
            item.Dump(root);
            foreach (Item child in item.Children)
            {
                child.Dump(root);
            }
        }

        public static bool IsSerialized(this Item item, string root)
        {
            var fileInfo = new FileInfo(item.GetSerializationPath(root));
            return fileInfo.Exists;
        }

        public static bool IsSerializedCurrent(this Item item, string root)
        {
            var fileInfo = new FileInfo(item.GetSerializationPath(root));
            if (!fileInfo.Exists) return false;

            var path = Sitecore.IO.TempFolder.GetFilename("serializationtest");
            Manager.DumpItem(path,item);

            var fileInfo2 = new FileInfo(path);

            var result = fileInfo.Compare(fileInfo2);
            fileInfo2.Delete();

            return result;            
        }

        public static void DeleteSerialization(this Item item, string root)
        {
            var fileInfo = new FileInfo(item.GetSerializationPath(root));

            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }
        
    }
}