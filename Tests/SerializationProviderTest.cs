using System;
using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;
using SerializationRules.Entities;
using SerializationRules.Providers;

namespace Tests
{
    [TestFixture]
    class SerializationProviderTest
    {
        private const string TempFolder = @"d:\temp";

        private static SerializationProvider _serializationProvider;
        private static PathProvider _pathProvider;
        private static IDefinitionsProvider _definitionsProviderObject;
        private Mock<ISerializationDefinition> _serializationDefinition;
        private Mock<ISerializableItem> _homeItem;

        [SetUp]
        public void Setup()
        {
            var definitionsProvider = new Mock<IDefinitionsProvider>();
            _pathProvider = new PathProvider();
            _serializationProvider = new SerializationProvider(_pathProvider,definitionsProvider.Object);
            _serializationDefinition = new Mock<ISerializationDefinition>();
            _serializationDefinition.SetupGet(def => def.Path).Returns(TempFolder);
            
            
            definitionsProvider.Setup(x => x.GetSerializationDefinitions(It.IsAny<IDatabase>()))
                               .Returns(new[]
                                   {
                                       _serializationDefinition.Object
                                   });
            _definitionsProviderObject = definitionsProvider.Object;

            _homeItem = new Mock<ISerializableItem>();
            _homeItem.SetupGet(i => i.Name).Returns("home");
            _homeItem.SetupGet(i => i.FullPath).Returns("/sitecore/content/home");
            _homeItem.SetupGet(i => i.Database.Name).Returns("master");
            _homeItem.Setup(i => i.Dump(It.IsAny<string>())).Throws<Exception>();

        }

        [Test]        
        public void Serialize_Item_Passing_Evaluation()
        {
            //arrange
            var serializableItem = _homeItem.Object;
            _serializationDefinition.Setup(d => d.Evaluate(It.IsAny<ISerializableItem>())).Returns(true);
            
            //act 
            TestDelegate testDelegate = () => _serializationProvider.Serialize(serializableItem);
            

            //assert
            //throws exception when item Dump
            Assert.Throws<Exception>(testDelegate);

        }

        [Test]
        public void Do_not_Serialize_Item_Failing_Evaluation()
        {
            //arrange            
            var serializableItem = _homeItem.Object;
            _serializationDefinition.Setup(d => d.Evaluate(It.IsAny<ISerializableItem>())).Returns(false);

            //act 
            TestDelegate testDelegate = () => _serializationProvider.Serialize(serializableItem);
            
            //assert
            
            Assert.DoesNotThrow(testDelegate);
        }

        [Test]
        public void Remove_Serialized_Files()
        {
            //arrange
            var serializableItemMock = new Mock<ISerializableItem>();
            serializableItemMock.SetupGet(i => i.Name).Returns("sample item");
            serializableItemMock.SetupGet(i => i.FullPath).Returns("sample item");
            var serializableItem = serializableItemMock.Object;
            var oldParentItem = _homeItem.Object;

            var directory = new DirectoryInfo(_pathProvider.GetOldFolderPath(serializableItem, oldParentItem, TempFolder));
            directory.Create();
            
            var file = new FileInfo(_pathProvider.GetOldPath(serializableItem, oldParentItem, TempFolder));
            
            try
            {
             

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

        [Test]
        public void Is_Serialized()
        {


            var serializableItem = _homeItem.Object;

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

 

