using System;
using DBScripter.Data;
using DBScripter.Domain;
using DBScripter.Service.Factory;
using NUnit.Framework;

namespace DBScripter.Service.Tests.Factory
{
    [TestFixture]
    public class RepositoryFactoryHandlerTests
    {
        [Test]
        [ExpectedException(typeof(Exception))]
        public void ThrowException_When_Inupt_Unsupported_DatabaseVersion()
        {
            //Arrange
            ScripterConfig config = new ScripterConfig() { DatabaseType = DatabaseVersionType.Unsupported };
            RepositoryFactoryHandler theFactoryHandler = new RepositoryFactoryHandler();


            //Act
            IRepository sqlserverRepository = theFactoryHandler.Handle(config);

        }




        [Test]
        public void Can_Create_SqlServerRepository()
        {
            //Arrange
            ScripterConfig config = new ScripterConfig() { DatabaseType = DatabaseVersionType.SQLServer };
            RepositoryFactoryHandler theFactoryHandler = new RepositoryFactoryHandler();


            //Act
            IRepository sqlserverRepository = theFactoryHandler.Handle(config);


            //Assert
            Assert.IsInstanceOf(typeof(SqlServerRepository), sqlserverRepository);

        }



        [Test]
        public void Can_Assign_ConnectionInfo()
        {
            //Arrange
            ScripterConfig config = new ScripterConfig() 
                { ServerName = "localhost", 
                    Username = "sa", 
                    Password = "test", 
                    DatabaseName = "AdventureWorks2008R2", 
                    DatabaseType = DatabaseVersionType.SQLServer 
                };
            
            RepositoryFactoryHandler theFactoryHandler = new RepositoryFactoryHandler();


            //Act
            IRepository sqlserverRepository = theFactoryHandler.Handle(config);


            //Assert
            Assert.AreEqual(sqlserverRepository.ServerName, config.ServerName);
            Assert.AreEqual(sqlserverRepository.Username, config.Username);
            Assert.AreEqual(sqlserverRepository.Password, config.Password);
            Assert.AreEqual(sqlserverRepository.DatabaseName, config.DatabaseName);


        }
    }
}
