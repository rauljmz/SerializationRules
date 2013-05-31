using System;
using System.Collections.Generic;
using System.Linq;
using SerializationRules.Entities;

namespace SerializationRules.Providers
{
    public class DefinitionsProvider : IDefinitionsProvider
    {
        private const string FolderId = "{6AC30241-2EB5-418E-94B1-13915F6B104C}";

        public IEnumerable<ISerializationDefinition> GetSerializationDefinitions(IDatabase database)
        {
            var serializationFolder = database.GetItem(new Guid(FolderId));
            if (serializationFolder == null) return new SerializationDefinition[] { };
            return serializationFolder.Children.Select(i => new SerializationDefinition(i));
        }
    }
}