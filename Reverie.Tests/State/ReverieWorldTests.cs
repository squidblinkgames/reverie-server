namespace Reverie.Tests.State
{
    using System.IO;
    using NUnit.Framework;
    using Reverie.State;


    [TestFixture]
    public class ReverieWorldTests
    {
        [TestFixture]
        class Constructor
        {
            [Test]
            public void CanCreate()
            {
                IReverieDatabase database = null;
                ReverieWorld reverieWorld = new ReverieWorld(database);
            }
        }


        [TestFixture]
        class Load : TestBase
        {
            [Test]
            public void CanLoadFromExistingSource()
            {
                this.reverieWorld.Load();
            }
        }


        [TestFixture]
        class Save : TestBase
        {
            [Test]
            public void CanSaveToDatabase()
            {                
                this.reverieWorld.Save();
            }
        }


        abstract class TestBase
        {
            #region Fields
            protected ReverieWorld reverieWorld;
            #endregion


            [TestFixtureSetUp]
            public void SetUp()
            {
                IReverieDatabase database = null;
                this.reverieWorld = new ReverieWorld(database);
            }
        }
    }
}