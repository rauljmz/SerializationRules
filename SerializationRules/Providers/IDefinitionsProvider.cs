using System.Collections.Generic;
using SerializationRules.Entities;
using Sitecore.Data;

namespace SerializationRules.Providers
{
    public interface IDefinitionsProvider
    {
        IEnumerable<SerializationDefinition> GetSerializationDefinitions(Database database);
    }
}