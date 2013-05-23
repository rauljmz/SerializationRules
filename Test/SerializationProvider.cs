using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializationRules.Providers;
using Moq;
using Sitecore.Data;

namespace Test
{
    [TestClass]
    public class SerializationProvider
    {
        private const string TempFolder = @"d:\temp";

        private static SerializationRules.Providers.SerializationProvider _serializationProvider;
        private static SerializationRules.Providers.PathProvider _pathProvider;
        private static IDefinitionsProvider _definitionsProviderObject;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext )
        {
            var definitionsProvider = new Mock<IDefinitionsProvider>();
            _pathProvider = new SerializationRules.Providers.PathProvider();
            _serializationProvider = new SerializationRules.Providers.SerializationProvider(_pathProvider,definitionsProvider.Object);
            
            definitionsProvider.Setup(x => x.GetSerializationDefinitions(It.IsAny<Database>()))
                               .Returns(new[]
                                   {
                                       new FakeSerializationDefinition(i => i.FullPath.StartsWith("/sitecore/content/home"), TempFolder,"")
                                   });
            _definitionsProviderObject = definitionsProvider.Object;

        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void Serialize_Item_Under_Home()
        {
            //arrange


            var serializableItem = new FakeSerializableItem("Home", "/sitecore/content/home");
            //act
            _serializationProvider.Serialize(serializableItem);
            //assert 
            //throws exception when item ump
        }

        [TestMethod]
        public void Do_NOT_Serialize_Item_NOT_Under_Home()
        {
            //arrange
           
            var serializableItem = new FakeSerializableItem("rules", "/sitecore/content/system/settings/rules");

            //act
            _serializationProvider.Serialize(serializableItem);

            //assert
            //no exception was thrown
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Remove_Serialized_Files()
        {

            var serializableItem = new FakeSerializableItem("sample item", "sample item");
            var oldParentItem = new FakeSerializableItem("home", "/sitecore/content/home");

            var directory = new DirectoryInfo(_pathProvider.GetOldFolderPath(serializableItem, oldParentItem, TempFolder));
            directory.Create();
            
            var file = new FileInfo(_pathProvider.GetOldPath(serializableItem, oldParentItem, TempFolder));
            
            try
            {
                //arrange

                file.Create().Close();


                //act
                _serializationProvider.Remove(serializableItem, oldParentItem);

                //assert

                Assert.IsFalse(file.Exists);
            }
            finally
            {
                if(directory.Exists)directory.Delete(true);
            }
        }

        [TestMethod]
        public void Is_Serialized()
        {

            
            var serializableItem = new FakeSerializableItem("home", "/sitecore/content/home");

            var directory = new DirectoryInfo(_pathProvider.GetFolderPath(serializableItem, TempFolder));
            directory.Create();

            var file = new FileInfo(_pathProvider.GetPath(serializableItem, TempFolder));

            try
            {
                //arrange

                file.Create().Close();


                //act
                var isSerialized = _serializationProvider.IsSerialized(serializableItem,
                                                    _definitionsProviderObject.GetSerializationDefinitions(null).First());

                //assert

                Assert.IsTrue(isSerialized);
            }
            finally
            {
                if (directory.Exists) directory.Delete(true);
            }
        }
    }
}
