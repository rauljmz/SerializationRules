using Moq;
using NUnit.Framework;
using SerializationRules.Entities;

namespace Tests
{
    class PathProviderTest
    {
        [TestFixture]
        public class PathProvider
        {
            private Mock<ISerializableItem> _serializableItem;
            [SetUp]
            public void Setup()
            {
                _serializableItem = new Mock<ISerializableItem>();
                _serializableItem.SetupGet(i => i.Name).Returns("home");
                _serializableItem.SetupGet(i => i.FullPath).Returns("/sitecore/content/home");
                _serializableItem.SetupGet(i => i.Database.Name).Returns("master");
            }

            [Test]
            public void GetPath_Returns_ItemPath()
            {
                //arrange
                var pathProvider = new SerializationRules.Providers.PathProvider();
                
                //act
                var path = pathProvider.GetPath(_serializableItem.Object, @"d:\serialization");
                //assert
                Assert.AreEqual(path, @"d:\serialization\master\sitecore\content\home.item");
            }

            [Test]
            public void GetFolderPath_Returns_ItemPath()
            {
                //arrange
                var pathProvider = new SerializationRules.Providers.PathProvider();
                
                //act
                var path = pathProvider.GetFolderPath(_serializableItem.Object, @"d:\serialization");
                //assert
                Assert.AreEqual(path, @"d:\serialization\master\sitecore\content\home");
            }

            [Test]
            public void GetFolderOldPath_Returns_ItemPath()
            {
                //arrange
                var pathProvider = new SerializationRules.Providers.PathProvider();
                var oldSerializableItem = _serializableItem.Object;
                var movedSerializableItem = new Mock<ISerializableItem>();
                movedSerializableItem.SetupGet(ms => ms.Name).Returns("sample item");
                //act
                var path = pathProvider.GetOldFolderPath(movedSerializableItem.Object, oldSerializableItem, @"d:\serialization");
                //assert
                Assert.AreEqual(path, @"d:\serialization\master\sitecore\content\home\sample item");
            }

            [Test]
            public void GetOldPath_Returns_ItemPath()
            {
                //arrange
                var pathProvider = new SerializationRules.Providers.PathProvider();
                var oldSerializableItem = _serializableItem.Object;
                var movedSerializableItem = new Mock<ISerializableItem>();
                movedSerializableItem.SetupGet(ms => ms.Name).Returns("sample item");
                //act
                var path = pathProvider.GetOldPath(movedSerializableItem.Object, oldSerializableItem, @"d:\serialization");
                //assert
                Assert.AreEqual(path, @"d:\serialization\master\sitecore\content\home\sample item.item");
            }

            [Test]
            public void GetOldPath_Returns_Empty_Root_When_Null()
            {
                //arrange
                var pathProvider = new SerializationRules.Providers.PathProvider();
                var oldSerializableItem = _serializableItem.Object;
                var movedSerializableItem = new Mock<ISerializableItem>();
                movedSerializableItem.SetupGet(ms => ms.Name).Returns("sample item");

                //act
                var path = pathProvider.GetOldPath(movedSerializableItem.Object, oldSerializableItem, null);
                //assert
                Assert.AreEqual(path, @"\master\sitecore\content\home\sample item.item");
            }

            [Test]
            public void GetPath_Avoids_Double_Slashes()
            {
                //arrange
                var pathProvider = new SerializationRules.Providers.PathProvider();
                
                //act
                var path = pathProvider.GetPath(_serializableItem.Object, @"d:\serialization\");
                //assert
                Assert.AreEqual(@"d:\serialization\master\sitecore\content\home.item", path);
            }

        }
    }
}
