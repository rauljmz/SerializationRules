using System;
using SerializationRules.Entities;
using SerializationRules.Providers;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace SerializationRules.Events
{
    public class Serialization
    {
        private readonly ISerializationProvider _serializationProvider;

        public Serialization(ISerializationProvider serializationProvider)
        {
            _serializationProvider = serializationProvider;

        }

        public Serialization() : this(new SerializationProvider())
        {
                
        }

        public void OnItemSaved(object sender, EventArgs args)
        {
            var item = Sitecore.Events.Event.ExtractParameter<Item>(args, 0);
            _serializationProvider.Serialize(new SerializableItem(item));
        }

        public void OnItemDeleted(object sender, EventArgs args)
        {
            var item = Sitecore.Events.Event.ExtractParameter<Item>(args, 0);
            var parentID = Sitecore.Events.Event.ExtractParameter<ID>(args, 1);

            _serializationProvider.Remove(new SerializableItem(item),parentID.ToString());
        }

        public void OnItemMoved(object sender, EventArgs args)
        {
            var item = Sitecore.Events.Event.ExtractParameter<Item>(args, 0);
            var parentID = Sitecore.Events.Event.ExtractParameter<ID>(args, 1);

            _serializationProvider.Serialize(new SerializableItem(item));
            _serializationProvider.Remove(new SerializableItem(item), parentID.ToString());
        }

        public void OnVersionRemoved(object sender, EventArgs args)
        {
            var item = Sitecore.Events.Event.ExtractParameter<Item>(args, 0);
            _serializationProvider.Serialize(new SerializableItem(item));
        }
    }
}