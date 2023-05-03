using System.Collections.Generic;
using System.IO;
using DBScripter.Domain;
using Microsoft.SqlServer.Management.Smo;

namespace DBScripter.Service.Command
{
    public class ScriptDatabaseCommandHandler : ICommandHandler<ScriptDatabaseCommand>
    {
        private IRepository _repository;
        private ICommandHandler<WriteScriptsCommand> _statementsToFileCommandHandler;
        private ScripterConfig _config;
        




        public ScriptDatabaseCommandHandler(ICommandHandler<WriteScriptsCommand> statementsToFileCommandHandler )
        {
            _statementsToFileCommandHandler = statementsToFileCommandHandler;
        }



        public void Handle(ScriptDatabaseCommand command)
        {
            _config = command.Config;
            _repository = command.Repository;

            CompatibilityLevel theCompatibilityLevel = _repository.GetCompatibilityLevel(); 

            if ( _config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.StoredProcedure) ) 
                scriptStoredProcedures();

            if (_config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.Table) ) 
                scriptTables();

            if (_config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.View))
                scriptViews();

            if (_config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_Function))
                scriptUserDefine_Functions();

            if ( _config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_Aggregate) && 
                theCompatibilityLevel != CompatibilityLevel.Version80) //SQL Server 2000
                scriptUserDefined_Aggregates();

            
            if (_config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_DataType) &&
                theCompatibilityLevel != CompatibilityLevel.Version80 && //SQL Server 2000
                theCompatibilityLevel != CompatibilityLevel.Version90) // SQL Server 2005
                scriptUserDefined_DataType();


            if (_config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_TableType) &&
                 theCompatibilityLevel != CompatibilityLevel.Version80 && //SQL Server 2000
                theCompatibilityLevel != CompatibilityLevel.Version90) // SQL Server 2005)
                scriptUserDefined_TableType();



            if (_config.TheDatabaseObjectTypes.HasFlag(DatabaseObjectType.UserDefined_Type) &&
                 theCompatibilityLevel != CompatibilityLevel.Version80 && //SQL Server 2000
                theCompatibilityLevel != CompatibilityLevel.Version90) // SQL Server 2005)
                scriptUserDefined_Type();
        }




        private void scriptObjects(string outputPath, IEnumerable<SqlObjectScript> scripts, DatabaseObjectType objectType )
        {
            WriteScriptsCommand scriptToFileCommand = new WriteScriptsCommand() 
            { 
                OutputPath = outputPath,
                Scripts = scripts,
                ObjectType = objectType 
            };

            _statementsToFileCommandHandler.Handle(scriptToFileCommand);
        }



        private void scriptStoredProcedures()
        {
            string outputPath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_StoredProcedure);
            IEnumerable<SqlObjectScript> scripts = _repository.GetStoredProcedures();
            DatabaseObjectType objectType = DatabaseObjectType.StoredProcedure;
            scriptObjects(outputPath, scripts, objectType);
        }


        private void scriptTables()
        {
            string outputPath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_Table);
            IEnumerable<SqlObjectScript> scripts = _repository.GetTables();
            DatabaseObjectType objectType = DatabaseObjectType.Table;
            scriptObjects(outputPath, scripts, objectType);
        }


        private void scriptViews()
        {
            string outputPath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_View);
            IEnumerable<SqlObjectScript> scripts = _repository.GetViews();
            DatabaseObjectType objectType = DatabaseObjectType.View;
            scriptObjects(outputPath, scripts, objectType);
        }


        private void scriptUserDefine_Functions()
        {
            string outputPath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_UserDefined_Function);
            IEnumerable<SqlObjectScript> scripts = _repository.GetUserDefined_Functions();
            DatabaseObjectType objectType = DatabaseObjectType.UserDefined_Function;
            scriptObjects(outputPath, scripts, objectType);
        }


        private void scriptUserDefined_Aggregates()
        {
            string outputPath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_UserDefined_Aggregate);
            IEnumerable<SqlObjectScript> scripts = _repository.GetUserDefined_Aggregates();
            DatabaseObjectType objectType = DatabaseObjectType.UserDefined_Aggregate;
            scriptObjects(outputPath, scripts, objectType);
        }


        private void scriptUserDefined_TableType()
        {
            string outputPath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_UserDefined_TableType);
            IEnumerable<SqlObjectScript> scripts = _repository.GetUserDefined_TableTypes();
            DatabaseObjectType objectType = DatabaseObjectType.UserDefined_TableType;
            scriptObjects(outputPath, scripts, objectType);
        }


        private void scriptUserDefined_DataType()
        {
            string outputPath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_UserDefined_DataType);
            IEnumerable<SqlObjectScript> scripts = _repository.GetUserDefined_DataTypes();
            DatabaseObjectType objectType = DatabaseObjectType.UserDefined_DataType;
            scriptObjects(outputPath, scripts, objectType);
        }


        private void scriptUserDefined_Type()
        {
            string outputPath = Path.Combine(_config.OutputDiretoryRoot, _config.FolderName_UserDefined_Type);
            IEnumerable<SqlObjectScript> scripts = _repository.GetUserDefined_Types();
            DatabaseObjectType objectType = DatabaseObjectType.UserDefined_Type;
            scriptObjects(outputPath, scripts, objectType);
        }

    }
}
