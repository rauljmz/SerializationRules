using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SerializationRules.Entities;
using Sitecore.Data;

namespace SerializationRules.Providers
{
    public class DefinitionsProvider : IDefinitionsProvider
    {
        public IEnumerable<ISerializationDefinition> GetSerializationDefinitions(Database database)
        {
            var serializationFolder = database.GetItem("{6AC30241-2EB5-418E-94B1-13915F6B104C}");
            if (serializationFolder == null) return new SerializationDefinition[] { };
            return serializationFolder.Children.Select(i => new SerializationDefinition(i));
        }
    }
}