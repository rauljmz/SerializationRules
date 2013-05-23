
using SerializationRules.Conditions;
using Sitecore.Data.Items;
using Sitecore.Data.Serialization;

namespace SerializationRules.Entities
{
    public class SerializableItem : CustomItem, ISerializableItem
    {
        public SerializableItem(Item innerItem) : base(innerItem)
        {
        }

        public string FullPath()
        {
            return InnerItem.Paths.FullPath;
        }

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