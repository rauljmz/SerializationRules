using System;
using System.Collections.Generic;
using SerializationRules.Conditions;

namespace SerializationRules.Entities
{
    public interface ISerializableItem : IItem
    {
        string FullPath { get; set; }
        void Dump(string root);
        void InitializeSerializationRuleContext(SerializationRuleContext serializationRuleContext);
    }

    public interface IItem
    {
        string this[string fieldName] { get; }
        string Name { get;  }
        IDatabase Database { get; }
        IItem GetItem(Guid id);
        IItem GetItem(string path);
        IEnumerable<IItem> Children { get; }
    }

    public interface IDatabase
    {
        string Name { get; set; }
        IItem GetItem(Guid id);
        IItem GetItem(string path);
    }
}