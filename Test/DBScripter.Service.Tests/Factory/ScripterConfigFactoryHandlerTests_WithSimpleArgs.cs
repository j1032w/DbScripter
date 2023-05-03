using System;
using DBScripter.Domain;
using DBScripter.Service.Factory;
using NUnit.Framework;

namespace DBScripter.Service.Tests
{
    
    [TestFixture]
    public class ScripterConfigFactoryHandlerTests_WithSimpleArgs
    {
        [Test]
        [ExpectedException(typeof(Exception))]
        public void ThrowException_When_Input_InvalidArgs()
        {
            // Arrange
            string[] args = new string[] { "localhost" };
            ScripterConfigFactoryHandler scripterConfigFactoryHandler = new ScripterConfigFactoryHandler();
            

            //Act
             ScripterConfig config = scripterConfigFactoryHandler.Handle(args);
        }
        


        [Test]
        public void Can_Parse_DatabaseConnectionInfo_And_OutputRootFolder()
        {
            //Arrange
            string[] args = new string[] { "localhost", "sa", "test", "AdventureWorks2008R2", @"d:\output" };
            ScripterConfigFactoryHandler scripterConfigFactoryHandler = new ScripterConfigFactoryHandler();


            //Act
            ScripterConfig config = scripterConfigFactoryHandler.Handle(args);


            //Assert
            Assert.AreEqual(config.ServerName, args[0]);
            Assert.AreEqual(config.Username, args[1]);
            Assert.AreEqual(config.Password, args[2]);
            Assert.AreEqual(config.DatabaseName, args[3]);
            Assert.AreEqual(config.OutputDiretoryRoot, args[4]);
        }



        [Test]
        public void Can_Parse_DatabaseVersion()
        {
            //Arrange
            string[] args = new string[] { "localhost", "sa", "test", "AdventureWorks2008R2", @"d:\output" };
            ScripterConfigFactoryHandler scripterConfigFactoryHandler = new ScripterConfigFactoryHandler();


            //Act
            ScripterConfig config = scripterConfigFactoryHandler.Handle(args);


            //Assert
            Assert.AreEqual(config.DatabaseType, DatabaseVersionType.SQLServer);
           
        }



        [Test, Combinatorial]
        public void Can_Parse_ScriptOption(
            [Values("", "s", "S")] string storedProcedure,
            [Values("", "t", "T")] string table,
            [Values("", "v", "V")] string view,
            [Values("", "u", "U")] string userDefined)
        {
            //Arrange
            string scriptOption = storedProcedure + table + userDefined + view;
            string[] args = new string[] { "localhost", "sa", "test", "AdventureWorks2008R2", @"d:\output", scriptOption};
            ScripterConfigFactoryHandler scripterConfigFactoryHandler = new ScripterConfigFactoryHandler();


            //Act
            ScripterConfig config = scripterConfigFactoryHandler.Handle(args);


            //Assert
            if (scriptOption == "")
            {
                Assert.IsTrue(config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.All));
            }
            else
            {
                Assert.AreEqual(config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.StoredProcedure), (storedProcedure.ToUpper() == "S") );
                Assert.AreEqual(config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.Table), (table.ToUpper() == "T"));
                Assert.AreEqual(config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.View), (view.ToUpper() == "V"));

                Assert.AreEqual(config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_Aggregate), (userDefined.ToUpper() == "U") );
                Assert.AreEqual(config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_Type), (userDefined.ToUpper() == "U"));
                Assert.AreEqual(config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_DataType), (userDefined.ToUpper() == "U"));
                Assert.AreEqual(config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_TableType), (userDefined.ToUpper() == "U"));
                
                
            }
        }



        [Test]
        public void Test_Parse_NoScriptOption()
        {
            //Arrange
            string[] args = new string[] { "localhost", "sa", "test", "AdventureWorks2008R2", @"d:\output" };
            ScripterConfigFactoryHandler scripterConfigFactoryHandler = new ScripterConfigFactoryHandler();


            //Act
            ScripterConfig config = scripterConfigFactoryHandler.Handle(args);


            //Assert
            Assert.IsTrue(config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.All));
        }



        [Test]
        public void Can_Parse_FolderName()
        {
            //Arrange
            string[] args = new string[] { "localhost", "sa", "test", "AdventureWorks2008R2", @"d:\output" };
            ScripterConfigFactoryHandler scripterConfigFactoryHandler = new ScripterConfigFactoryHandler();


            //Act
            ScripterConfig config = scripterConfigFactoryHandler.Handle(args);


            //Assert
            Assert.AreEqual(config.FolderName_StoredProcedure, "StoredProcedures");
            Assert.AreEqual(config.FolderName_Table, "Tables");
            Assert.AreEqual(config.FolderName_View, "Views");

            Assert.AreEqual(config.FolderName_UserDefined_Aggregate, "UserDefinedAggregates");
            Assert.AreEqual(config.FolderName_UserDefined_Function, "UserDefinedFunctions");
            Assert.AreEqual(config.FolderName_UserDefined_DataType, "UserDefinedDataTypes");
            Assert.AreEqual(config.FolderName_UserDefined_TableType, "UserDefinedTableTypes");
            Assert.AreEqual(config.FolderName_UserDefined_Type, "UserDefinedTypes");
            
        }

    }
}
