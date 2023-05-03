using System.Collections.Generic;
using System.Linq;
using DBScripter.Domain;
using DBScripter.Service.Command;
using Moq;
using NUnit.Framework;

namespace DBScripter.Service.Tests.Command
{
    [TestFixture]
    public class WriteStatementsCommandHandlerTests
    {
        private Mock<ICommandHandler<CreateFileCommand>> _mockCreateFileCommandHandler;
        private Mock<ICommandHandler<ClearDirectoryCommand>> _mockClearDirectoryCommandHandler;
        private Mock<ICommandHandler<LogCommand>> _mockLogCommandHandler;
        
        private List<SqlObjectScript> _statementsList;
        private WriteScriptsCommand _writeScriptsCommand;
        private WriteScriptsCommandHandler _writeScriptsCommandHandler;





        [SetUp]
        public void Setup()
        {
            _mockClearDirectoryCommandHandler = new Mock<ICommandHandler<ClearDirectoryCommand>>();
            _mockCreateFileCommandHandler =  new Mock<ICommandHandler<CreateFileCommand>>();
            _mockLogCommandHandler = new Mock<ICommandHandler<LogCommand>>();

            _writeScriptsCommandHandler = new WriteScriptsCommandHandler(    _mockLogCommandHandler.Object, 
                                                                                        _mockCreateFileCommandHandler.Object, 
                                                                                        _mockClearDirectoryCommandHandler.Object);

            SetupWriteStatementsCommand();

        }

        private void SetupWriteStatementsCommand()
        {
            _writeScriptsCommand = new WriteScriptsCommand();
            _writeScriptsCommand.OutputPath = @"d:\output\Tables";
            _writeScriptsCommand.ObjectType = DatabaseObjectType.Table;
            _statementsList = new List<SqlObjectScript>
                {
                    new SqlObjectScript() { Name = "Table1", Text = "CREATE TABLE Table1" },
                    new SqlObjectScript() { Name = "Table2", Text = "CREATE TABLE Table1" },
                    new SqlObjectScript() { Name = "Table3", Text = "CREATE TABLE Table3" }
                };
            _writeScriptsCommand.Scripts = _statementsList;
        }




        [Test]
        public void Can_Clear_Directory()
        {
            //Arrange
  
            //Act
            _writeScriptsCommandHandler.Handle(_writeScriptsCommand);


            //Assert
            _mockClearDirectoryCommandHandler.Verify(
                foo => foo.Handle(It.Is<ClearDirectoryCommand>(s => s.DirectoryPath == @"d:\output\Tables")), Times.Exactly(1) );

        }


        [Test]
        public void Can_Write_Statement()
        {
            //Arrange
            int statementsListSize = _writeScriptsCommand.Scripts.Count();

            //Act
            _writeScriptsCommandHandler.Handle(_writeScriptsCommand);


            //Assert
            _mockCreateFileCommandHandler.Verify(
                foo=>foo.Handle(It.Is<CreateFileCommand>(s => s.DirectoryPath == @"d:\output\Tables")), Times.Exactly(statementsListSize));
        }


       

    }
}
