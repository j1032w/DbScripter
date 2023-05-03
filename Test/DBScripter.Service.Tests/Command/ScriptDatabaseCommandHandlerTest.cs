using System.Collections.Generic;
using System.IO;
using System.Linq;
using DBScripter.Domain;
using DBScripter.Service.Command;
using Moq;
using NUnit.Framework;

namespace DBScripter.Service.Tests.Command
{
    [TestFixture]
    public class ScriptDatabaseCommandHandlerTest
    {
        private ScripterConfig _config;
        private ScriptDatabaseCommand _scriptDatabaeCommand;

        private Mock<IEnumerable<SqlObjectScript>> _mockStoredProcedures;
        private Mock<IEnumerable<SqlObjectScript>> _mockTables;
        private Mock<IEnumerable<SqlObjectScript>> _mockViews;
        private Mock<IEnumerable<SqlObjectScript>> _mockUserDefined_Functions;
        private Mock<IEnumerable<SqlObjectScript>> _mockUserDefined_Aggregates;
        private Mock<IEnumerable<SqlObjectScript>> _mockUserDefined_DataTypes;
        private Mock<IEnumerable<SqlObjectScript>> _mockUserDefined_TableTypes;
        private Mock<IEnumerable<SqlObjectScript>> _mockUserDefined_Types;

        private Mock<IRepository> _mockRepository;
        private Mock<ICommandHandler<WriteScriptsCommand>> _mockStatementsToFileCommandHandler;

        private ScriptDatabaseCommandHandler _scriptDatabasecommandHnadler;


        private static IEnumerable<DatabaseObjectType> _exportObjectSource
        {
            get
            {
                int start = (int)DatabaseObjectType.None;
                int count = (int)DatabaseObjectType.All - start + 1;
                return Enumerable.Range(start, count).Select(i => (DatabaseObjectType)i);
            }
        }



        [SetUp]
        public void Setup()
        {
            _mockStatementsToFileCommandHandler = new Mock<ICommandHandler<WriteScriptsCommand>>();
            
            setupRepository();
            setupScriptDatabasesCommand();
            
            _scriptDatabasecommandHnadler = new ScriptDatabaseCommandHandler(_mockStatementsToFileCommandHandler.Object);
            
        }



        [Test, TestCaseSource("_exportObjectSource")]
        public void Can_Invoke_StatementsToFilesHandler(DatabaseObjectType databaseObjectTypes)
        {
            //Arrange
            _config.TheDatabaseObjectTypes = databaseObjectTypes;
    
            //Act
            _scriptDatabasecommandHnadler.Handle(_scriptDatabaeCommand);


            //Assert
            Verify_StatementsToFilesCommandHandler_Call_For_StoredProcedure(databaseObjectTypes);
            Verify_StatementsToFilesCommandHandler_Call_For_Table(databaseObjectTypes);
            verify_StatementsToFilesCommandHandler_Call_For_View(databaseObjectTypes);

            Verify_StatementsToFilesCommandHandler_Call_For_UserDefined_Aggregate(databaseObjectTypes);
            verify_StatementsToFilesCommandHandler_Call_For_UserDefined_Function(databaseObjectTypes);
            verify_StatementsToFilesCommandHandler_Call_For_UserDefined_TableType(databaseObjectTypes);
            verify_StatementsToFilesCommandHandler_Call_For_UserDefined_Type(databaseObjectTypes);
            verify_StatementsToFilesCommandHandler_Call_For_UserDefined_DataType(databaseObjectTypes);


        }


    


        private void Verify_StatementsToFilesCommandHandler_Call_For_StoredProcedure(DatabaseObjectType theDatabaseObjectTypes)
        {
            int runTimes = theDatabaseObjectTypes.HasFlag(DatabaseObjectType.StoredProcedure) ? 1 : 0;
            string storedProcedurePath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_StoredProcedure);


            _mockStatementsToFileCommandHandler.Verify(
                    foo => foo.Handle(It.Is<WriteScriptsCommand>(s => s.OutputPath == storedProcedurePath)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.ObjectType == DatabaseObjectType.StoredProcedure)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.Scripts == _mockRepository.Object.GetStoredProcedures())), Times.Exactly(runTimes));
        }



        private void Verify_StatementsToFilesCommandHandler_Call_For_Table(DatabaseObjectType theDatabaseObjectTypes)
        {
            int runTimes = theDatabaseObjectTypes.HasFlag(DatabaseObjectType.Table) ? 1 : 0;
            string tablePath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_Table);


            _mockStatementsToFileCommandHandler.Verify(
                    foo => foo.Handle(It.Is<WriteScriptsCommand>(
                        s => s.OutputPath == tablePath)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.ObjectType == DatabaseObjectType.Table)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.Scripts == _mockRepository.Object.GetTables())), Times.Exactly(runTimes));
        }



        private void Verify_StatementsToFilesCommandHandler_Call_For_UserDefined_Aggregate(DatabaseObjectType theDatabaseObjectTypes)
        {
            int runTimes = theDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_Aggregate) ? 1 : 0;
            string userDefinedAggregatePath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_UserDefined_Aggregate);


            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>( s => s.OutputPath == userDefinedAggregatePath)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>( s => s.ObjectType == DatabaseObjectType.UserDefined_Aggregate)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>( s => s.Scripts == _mockRepository.Object.GetUserDefined_Aggregates())), Times.Exactly(runTimes));
        }


        private void verify_StatementsToFilesCommandHandler_Call_For_UserDefined_Function(DatabaseObjectType theDatabaseObjectTypes)
        {
            int runTimes = theDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_Function) ? 1 : 0;
            string userDefinedFunctionPath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_UserDefined_Function);


            _mockStatementsToFileCommandHandler.Verify(
                    foo => foo.Handle(It.Is<WriteScriptsCommand>(
                        s => s.OutputPath == userDefinedFunctionPath)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.ObjectType == DatabaseObjectType.UserDefined_Function)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.Scripts == _mockRepository.Object.GetUserDefined_Functions())), Times.Exactly(runTimes));
        }


        private void verify_StatementsToFilesCommandHandler_Call_For_UserDefined_TableType(DatabaseObjectType theDatabaseObjectTypes)
        {
            int runTimes = theDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_TableType) ? 1 : 0;
            string tableTypePath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_UserDefined_TableType);


            _mockStatementsToFileCommandHandler.Verify(
                    foo => foo.Handle(It.Is<WriteScriptsCommand>(
                        s => s.OutputPath == tableTypePath)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.ObjectType == DatabaseObjectType.UserDefined_TableType)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.Scripts == _mockRepository.Object.GetUserDefined_TableTypes())), Times.Exactly(runTimes));
        }



        private void verify_StatementsToFilesCommandHandler_Call_For_UserDefined_DataType(DatabaseObjectType theDatabaseObjectTypes)
        {
            int runTimes = theDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_DataType) ? 1 : 0;
            string outputPath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_UserDefined_DataType);


            _mockStatementsToFileCommandHandler.Verify(
                    foo => foo.Handle(It.Is<WriteScriptsCommand>(
                        s => s.OutputPath == outputPath)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.ObjectType == DatabaseObjectType.UserDefined_DataType)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.Scripts == _mockRepository.Object.GetUserDefined_DataTypes())), Times.Exactly(runTimes));
        }


        private void verify_StatementsToFilesCommandHandler_Call_For_UserDefined_Type(DatabaseObjectType theDatabaseObjectTypes)
        {
            int runTimes = theDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_Type) ? 1 : 0;
            string outputPath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_UserDefined_Type);


            _mockStatementsToFileCommandHandler.Verify(
                    foo => foo.Handle(It.Is<WriteScriptsCommand>(
                        s => s.OutputPath == outputPath)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.ObjectType == DatabaseObjectType.UserDefined_Type)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.Scripts == _mockRepository.Object.GetUserDefined_Types())), Times.Exactly(runTimes));
        }



        private void verify_StatementsToFilesCommandHandler_Call_For_View(DatabaseObjectType theDatabaseObjectTypes)
        {
            int runTimes = theDatabaseObjectTypes.HasFlag(DatabaseObjectType.View) ? 1 : 0;
            string viewPath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_View);


            _mockStatementsToFileCommandHandler.Verify(
                    foo => foo.Handle(It.Is<WriteScriptsCommand>(
                        s => s.OutputPath == viewPath)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.ObjectType == DatabaseObjectType.View)), Times.Exactly(runTimes));

            _mockStatementsToFileCommandHandler.Verify(
               foo => foo.Handle(It.Is<WriteScriptsCommand>(
                   s => s.Scripts == _mockRepository.Object.GetViews())), Times.Exactly(runTimes));
        }


        private void setupScriptDatabasesCommand()
        {
            _config = new ScripterConfig()
                {
                    OutputDiretoryRoot = @"d:\output",

                    FolderName_StoredProcedure = "StoredProcedures",
                    FolderName_Table = "Tables",
                    FolderName_View = "Views",

                    FolderName_UserDefined_Aggregate = "UserDefinedAggregates",
                    FolderName_UserDefined_Function = "UserDefinedFunctions",
                    FolderName_UserDefined_DataType = "UserDefinedDataTypes",
                    FolderName_UserDefined_TableType = "UserDefinedTableTypes",
                    FolderName_UserDefined_Type = "UserDefinedTypes"
                };

            
            _scriptDatabaeCommand = new ScriptDatabaseCommand() { Config = _config, Repository = _mockRepository.Object };
            _mockStatementsToFileCommandHandler = new Mock<ICommandHandler<WriteScriptsCommand>>();

        }


        private void setupRepository()
        {
            _mockRepository = new Mock<IRepository>();

            _mockStoredProcedures = new Mock<IEnumerable<SqlObjectScript>>();
            _mockTables = new Mock<IEnumerable<SqlObjectScript>>();
            _mockViews = new Mock<IEnumerable<SqlObjectScript>>();

            _mockUserDefined_Aggregates = new Mock<IEnumerable<SqlObjectScript>>();
            _mockUserDefined_Functions = new Mock<IEnumerable<SqlObjectScript>>();
            _mockUserDefined_DataTypes = new Mock<IEnumerable<SqlObjectScript>>();
            _mockUserDefined_TableTypes = new Mock<IEnumerable<SqlObjectScript>>();
            _mockUserDefined_Types = new Mock<IEnumerable<SqlObjectScript>>();
            

            _mockRepository.Setup(foo => foo.GetStoredProcedures()).Returns(_mockStoredProcedures.Object);
            _mockRepository.Setup(foo => foo.GetTables()).Returns(_mockTables.Object);
            _mockRepository.Setup(foo => foo.GetViews()).Returns(_mockViews.Object);

            _mockRepository.Setup(foo => foo.GetUserDefined_Aggregates()).Returns(_mockUserDefined_Aggregates.Object);
            _mockRepository.Setup(foo => foo.GetUserDefined_Functions()).Returns(_mockUserDefined_Functions.Object);
            _mockRepository.Setup(foo => foo.GetUserDefined_DataTypes()).Returns(_mockUserDefined_DataTypes.Object);
            _mockRepository.Setup(foo => foo.GetUserDefined_TableTypes()).Returns(_mockUserDefined_TableTypes.Object);
            _mockRepository.Setup(foo => foo.GetUserDefined_Types()).Returns(_mockUserDefined_Types.Object);
        }
        
    }
}
