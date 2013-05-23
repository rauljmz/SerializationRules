using System;
using SerializationRules.Conditions;
using SerializationRules.Entities;
using Sitecore.Data;

namespace Test
{ 
    public class FakeSerializableItem : ISerializableItem
    {
       
        public FakeSerializableItem(string name, string path)
        {
            Name = name;
            FullPath = path;
            DatabaseName = "master";
            Database = null;
        }
       

        public string FullPath { get; private set; }
        public string DatabaseName { get; private set; }
        public Database Database { get; private set; }
        public string Name { get; private set; }
        public void Dump(string root)
        {
            throw new NotImplementedException();
        }

        public void InitializeSerializationRuleContext(SerializationRuleContext serializationRuleContext)
        {
            throw new NotImplementedException();
        }
    }
}
