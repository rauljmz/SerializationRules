using System;
using SerializationRules.Entities;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace SerializationRules.Events
{
    public class Serialization
    {
        private readonly ISerializationManager _serializationManager;

        public Serialization(ISerializationManager serializationManager)
        {
            _serializationManager = serializationManager;

        }

        public Serialization() : this(new SerializationManager())
        {
                
        }

        public void OnItemSaved(object sender, EventArgs args)
        {
            var item = Sitecore.Events.Event.ExtractParameter<Item>(args, 0);
            _serializationManager.Serialize(new SerializableItem(item));
        }

        public void OnItemDeleted(object sender, EventArgs args)
        {
            var item = Sitecore.Events.Event.ExtractParameter<Item>(args, 0);
            var parentID = Sitecore.Events.Event.ExtractParameter<ID>(args, 1);

            _serializationManager.Remove(new SerializableItem(item),parentID.ToString());
        }

        public void OnItemMoved(object sender, EventArgs args)
        {
            var item = Sitecore.Events.Event.ExtractParameter<Item>(args, 0);
            var parentID = Sitecore.Events.Event.ExtractParameter<ID>(args, 1);

            _serializationManager.Serialize(new SerializableItem(item));
            _serializationManager.Remove(new SerializableItem(item), parentID.ToString());
        }

        public void OnVersionRemoved(object sender, EventArgs args)
        {
            var item = Sitecore.Events.Event.ExtractParameter<Item>(args, 0);
            _serializationManager.Serialize(new SerializableItem(item));
        }
    }
}