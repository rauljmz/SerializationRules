using System;
using Sitecore.Data;

namespace SerializationRules.Entities
{
    public class ScDatabase : IDatabase
    {
        public Database InnerDatabase { get; private set; }

        public string Name { get; set; }
        public IItem GetItem(Guid id)
        {
            var item = InnerDatabase.GetItem(new ID(id));
            return item == null ? null :  new ScItem(item);
        }

        public IItem GetItem(string path)
        {
            var item = InnerDatabase.GetItem(new ID(path));
            return item == null ? null : new ScItem(item);
        }

        public ScDatabase(Database innerDatabase)
        {
            InnerDatabase = innerDatabase;
        }
    }
}