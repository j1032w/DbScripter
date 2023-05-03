using DBScripter.Domain;
using DBScripter.Service.Command;
using DBScripter.Service.Factory;
using Moq;
using NUnit.Framework;

namespace DBScripter.Service.Tests
{
    [TestFixture]
    public class ScripterContorllerTests_WithSimpleArgs
    {
        private Mock< IFactoryHandler<string[], ScripterConfig> > _mockScripterConfigFactoryHandler;
        private Mock< IFactoryHandler<ScripterConfig, IRepository> > _mockRepositoryFactoryHandler; 
        private Mock< ICommandHandler<ScriptDatabaseCommand> > _mockScriptDatabaseCommandHandler;
        private Mock<IRepository> _mockRepository;
        
        private string[] _args;
        private ScripterConfig _config;
        private ScripterController _scripterController;




        [Test] 
        public void Can_Invoke_ExactlyOneTime_ScriptDatabaseCommandHandler()
        {
            // Arrange
            _args = new string[] { "localhost", "sa", "test", "AdventureWorks2008R2", @"d:\output" };
           Arrange();


            //Act
            _scripterController.Script(_args);
            

            //Assert
            _mockScriptDatabaseCommandHandler.Verify( foo => foo.Handle( It.IsAny<ScriptDatabaseCommand>() ), Times.Exactly(1) );
        }


        [Test]
        public void Can_Parse_Args()
        {
            // Arrange
            _args = new string[] { "localhost", "sa", "test", "AdventureWorks2008R2", @"d:\output" };
            Arrange();


            //Act
            _scripterController.Script(_args);

            //Assert
            _mockScriptDatabaseCommandHandler.Verify(
                foo => foo.Handle(It.Is<ScriptDatabaseCommand>(s => s.Config != null )));

            _mockScriptDatabaseCommandHandler.Verify(
                foo => foo.Handle(It.Is<ScriptDatabaseCommand>(s => s.Repository != null )));
        }


       
        private void Arrange()
        {
            Build_mockScripterConfigFactoryHandler();
            Build_mockRepositoryFactoryHandler();

            _mockScriptDatabaseCommandHandler = new Mock<ICommandHandler<ScriptDatabaseCommand>>();
            _scripterController = new ScripterController(
                                                _mockScripterConfigFactoryHandler.Object, 
                                                _mockRepositoryFactoryHandler.Object,
                                                _mockScriptDatabaseCommandHandler.Object);
            
        }

        private void Build_mockScripterConfigFactoryHandler()
        {
            _config = new ScripterConfig();
            _mockScripterConfigFactoryHandler = new Mock<IFactoryHandler<string[], ScripterConfig>>();
            _mockScripterConfigFactoryHandler.Setup(foo => foo.Handle(It.IsAny<string[]>())).Returns(_config);
            
        }

        private void Build_mockRepositoryFactoryHandler()
        {
            _mockRepository = new Mock<IRepository>();
            _mockRepositoryFactoryHandler = new Mock<IFactoryHandler<ScripterConfig, IRepository>>();
            _mockRepositoryFactoryHandler.Setup(foo => foo.Handle(It.IsAny<ScripterConfig>())).Returns(_mockRepository.Object);
        }




 
    }
}
