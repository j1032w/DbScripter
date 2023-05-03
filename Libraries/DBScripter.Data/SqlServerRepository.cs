using System;
using System.Collections.Generic;
using System.Linq;
using DBScripter.Domain;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DBScripter.Data
{
    public class SqlServerRepository : IRepository
    {
        #region Private Members

        private Database _theDatabase;
        private Server _theServer;

        private ScriptingOptions _smoScriptingOptions;

        #endregion




        public SqlServerRepository()
        {
            setSmoScriptOption();
        }
 

    #region IDatabaseRepository Methods

        public string ServerName { get; set; }
   
        public string Username { get; set; }
   
        public string Password { get; set; }
   
        public string DatabaseName { get; set; }



        public CompatibilityLevel GetCompatibilityLevel()
        {
            connectDatabase();
            return _theDatabase.CompatibilityLevel;
        }


        // Cannot use generic, as Smo.Table, Smo.View, etc. are sealed class
        public IEnumerable<SqlObjectScript> GetStoredProcedures()
        {
            connectDatabase();


            return from StoredProcedure tehStoredProcedure in _theDatabase.StoredProcedures 
                   where !tehStoredProcedure.IsSystemObject 
                   let texts = tehStoredProcedure.Script(_smoScriptingOptions) 
                   let fullText = texts.Cast<string>().Aggregate(string.Empty, (current, s) => current + s + "\n" + "GO\n\n") 
                   select new SqlObjectScript() { Name = tehStoredProcedure.Name, Text = fullText };
        }


        public IEnumerable<SqlObjectScript> GetTables()
        {
            connectDatabase();

            return from Table theTable in _theDatabase.Tables 
                   where !theTable.IsSystemObject let texts = theTable.Script(_smoScriptingOptions) 
                   let fullText = texts.Cast<string>().Aggregate(string.Empty, (current, s) => current + s + "\n" + "GO\n\n") 
                   select new SqlObjectScript() { Name = theTable.Name, Text = fullText};
        }


        public IEnumerable<SqlObjectScript> GetViews()
        {
            connectDatabase();

            return from View theView in _theDatabase.Views 
                   where !theView.IsSystemObject 
                   let texts = theView.Script(_smoScriptingOptions) 
                   let fullText = texts.Cast<string>().Aggregate(string.Empty, (current, s) => current + s + "\n" + "GO\n\n") 
                   select new SqlObjectScript() { Name = theView.Name, Text = fullText };
        }


        public IEnumerable<SqlObjectScript> GetUserDefined_Aggregates()
        {
            connectDatabase();

            return from UserDefinedAggregate theAggregate in _theDatabase.UserDefinedAggregates 
                   let texts = theAggregate.Script(_smoScriptingOptions) 
                   let fullText = texts.Cast<string>().Aggregate(string.Empty, (current, s) => current + s + "\n" + "GO\n\n") 
                   select new SqlObjectScript() { Name = theAggregate.Name, Text = fullText };
        }


        public IEnumerable<SqlObjectScript> GetUserDefined_Functions()
        {
            connectDatabase();

            return from UserDefinedFunction theFunction in _theDatabase.UserDefinedFunctions 
                   where !theFunction.IsSystemObject 
                   let texts = theFunction.Script(_smoScriptingOptions) 
                   let fullText = texts.Cast<string>().Aggregate(string.Empty, (current, s) => current + s + "\n" + "GO\n\n") 
                   select new SqlObjectScript() { Name = theFunction.Name, Text = fullText };
        }


        public IEnumerable<SqlObjectScript> GetUserDefined_TableTypes()
        {
            connectDatabase();

            return from UserDefinedTableType theDataType in _theDatabase.UserDefinedTableTypes 
                   let texts = theDataType.Script(_smoScriptingOptions) 
                   let fullText = texts.Cast<string>().Aggregate(string.Empty, (current, s) => current + s + "\n" + "GO\n\n") 
                   select new SqlObjectScript() {Name = theDataType.Name, Text = fullText};
        }


        public IEnumerable<SqlObjectScript> GetUserDefined_DataTypes()
        {
            connectDatabase();

            return from UserDefinedDataType theDataType in _theDatabase.UserDefinedDataTypes
                   let texts = theDataType.Script(_smoScriptingOptions)
                   let fullText = texts.Cast<string>().Aggregate(string.Empty, (current, s) => current + s + "\n" + "GO\n\n")
                   select new SqlObjectScript() { Name = theDataType.Name, Text = fullText };
        }



        public IEnumerable<SqlObjectScript> GetUserDefined_Types()
        {
            connectDatabase();

            return from UserDefinedType theDataType in _theDatabase.UserDefinedTypes
                   let texts = theDataType.Script(_smoScriptingOptions)
                   let fullText = texts.Cast<string>().Aggregate(string.Empty, (current, s) => current + s + "\n" + "GO\n\n")
                   select new SqlObjectScript() { Name = theDataType.Name, Text = fullText };
        }



        #endregion




        private void connectDatabase()
        {
            ServerConnection theConnection = new ServerConnection(ServerName, Username, Password);
            _theServer = new Server(theConnection);
            _theDatabase = _theServer.Databases[DatabaseName];
            if (_theDatabase == null)
            {
                throw new Exception("Error: Cannot connect to database: " + DatabaseName);
            }
        }


        private void setSmoScriptOption()
        {
            _smoScriptingOptions = new ScriptingOptions()
                {
                    Triggers = true,
                    ClusteredIndexes = true,
                    DriAll = true,
                    Indexes = true,
                    SchemaQualifyForeignKeysReferences = true,
                    NoCollation = true,
                    AnsiPadding = true
                };
            
        }

        

    }
}
