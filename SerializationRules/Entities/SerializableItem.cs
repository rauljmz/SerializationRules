
using SerializationRules.Conditions;
using Sitecore.Data.Items;
using Sitecore.Data.Serialization;

namespace SerializationRules.Entities
{
    public class SerializableItem : ScItem, ISerializableItem
    {
        public SerializableItem(Item innerItem)
            : base(innerItem)
        {        
            FullPath = innerItem.Paths.FullPath;
        }



        public string FullPath { get; set; }

        public void Dump(string root)
        {
            Manager.DumpItem(root, InnerItem);
        }

        public void InitializeSerializationRuleContext(SerializationRuleContext serializationRuleContext)
        {
            serializationRuleContext.Item = InnerItem;
            
        }
    }
}