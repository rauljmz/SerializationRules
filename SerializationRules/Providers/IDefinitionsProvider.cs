using System.Collections.Generic;
using SerializationRules.Entities;

namespace SerializationRules.Providers
{
    public interface IDefinitionsProvider
    {
        IEnumerable<ISerializationDefinition> GetSerializationDefinitions(IDatabase database);
    }
}