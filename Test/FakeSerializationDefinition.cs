using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerializationRules.Entities;

namespace Test
{
    public class FakeSerializationDefinition : ISerializationDefinition
    {
        private Func<ISerializableItem, bool> _evaluationFunction;
        public FakeSerializationDefinition(Func<ISerializableItem, bool> evaluationFunction, string path, string filter)
        {
            _evaluationFunction = evaluationFunction;
            Path = path;
            Filter = filter;
        }
        public string Path { get; set; }
        public string Filter { get; set; }
        public bool Evaluate(ISerializableItem item)
        {
            return _evaluationFunction(item);
        }
    }
}
