using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializationRules.Providers;

namespace Test
{
    [TestClass]
    public class PathProvider
    {
        [TestMethod]
        public void GetPath_Returns_ItemPath()
        {
            //arrange
            var pathProvider = new SerializationRules.Providers.PathProvider();
            var serializableItem = new FakeSerializableItem("home", "/sitecore/content/home");
            //act
            var path = pathProvider.GetPath(serializableItem, @"d:\serialization");
            //assert
            Assert.AreEqual(path, @"d:\serialization\master\sitecore\content\home.item");
        }

        [TestMethod]
        public void GetFolderPath_Returns_ItemPath()
        {
            //arrange
            var pathProvider = new SerializationRules.Providers.PathProvider();
            var serializableItem = new FakeSerializableItem("home", "/sitecore/content/home");
            //act
            var path = pathProvider.GetFolderPath(serializableItem, @"d:\serialization");
            //assert
            Assert.AreEqual(path, @"d:\serialization\master\sitecore\content\home");
        }

        [TestMethod]
        public void GetFolderOldPath_Returns_ItemPath()
        {
            //arrange
            var pathProvider = new SerializationRules.Providers.PathProvider();
            var oldSerializableItem = new FakeSerializableItem("home", "/sitecore/content/home");
            var serializableItem = new FakeSerializableItem("sample item", "sample item");
            //act
            var path = pathProvider.GetOldFolderPath(serializableItem,oldSerializableItem, @"d:\serialization");
            //assert
            Assert.AreEqual(path, @"d:\serialization\master\sitecore\content\home\sample item");
        }

        [TestMethod]
        public void GetOldPath_Returns_ItemPath()
        {
            //arrange
            var pathProvider = new SerializationRules.Providers.PathProvider();
            var oldSerializableItem = new FakeSerializableItem("home", "/sitecore/content/home");
            var serializableItem = new FakeSerializableItem("sample item", "sample item");
            //act
            var path = pathProvider.GetOldPath(serializableItem, oldSerializableItem, @"d:\serialization");
            //assert
            Assert.AreEqual(path, @"d:\serialization\master\sitecore\content\home\sample item.item");
        }

        [TestMethod]
        public void GetOldPath_Returns_Empty_Root_When_Null()
        {
            //arrange
            var pathProvider = new SerializationRules.Providers.PathProvider();
            var oldSerializableItem = new FakeSerializableItem("home", "/sitecore/content/home");
            var serializableItem = new FakeSerializableItem("sample item", "sample item");
            //act
            var path = pathProvider.GetOldPath(serializableItem, oldSerializableItem,null);
            //assert
            Assert.AreEqual(path, @"\master\sitecore\content\home\sample item.item");
        }

        [TestMethod]
        public void GetPath_Avoids_Double_Slashes()
        {
            //arrange
            var pathProvider = new SerializationRules.Providers.PathProvider();
            var oldSerializableItem = new FakeSerializableItem("home", "/sitecore/content/home");
            var serializableItem = new FakeSerializableItem("sample item", "sample item");
            //act
            var path = pathProvider.GetOldPath(serializableItem, oldSerializableItem,@"d:\serialization\");
            //assert
            Assert.AreEqual(@"d:\serialization\master\sitecore\content\home\sample item.item",path);
        }

    }
}
