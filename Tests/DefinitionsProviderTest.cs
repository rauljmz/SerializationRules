using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SerializationRules.Entities;
using SerializationRules.Providers;

namespace Tests
{
    class DefinitionsProviderTest
    {
        [Test]
        public void Get_Children_Of_Definition_Folder()
        {
            //arrange
            var folder = new Mock<IItem>();
            var child = new Mock<IItem>();
            child.SetupGet(c => c["path"]).Returns("d:\\");
            child.SetupGet(c => c["filter"]).Returns(string.Empty);
            child.SetupGet(c => c["enabled"]).Returns("1");
            folder.SetupGet(f => f.Children).Returns(new []
                {
                    child.Object
                });
            var database = new Mock<IDatabase>();
            database.Setup(d => d.GetItem(new Guid("{6AC30241-2EB5-418E-94B1-13915F6B104C}"))).Returns(folder.Object);
            var definitionsProvider = new DefinitionsProvider();

            //act
            var definitions = definitionsProvider.GetSerializationDefinitions(database.Object).ToArray();

            //assert
            Assert.That(definitions.First().Path,Is.EqualTo("d:\\"));
            Assert.That(definitions.First().Filter, Is.EqualTo(string.Empty));
            Assert.That(definitions.First().Enabled, Is.EqualTo(true));
        }
    }
}
