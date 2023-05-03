using System;
using DBScripter.Domain;

namespace DBScripter.Service.Factory
{
    public class ScripterConfigFactoryHandler : IFactoryHandler<string[], ScripterConfig>
    {
        // Do NOT change simple args folder name, the simple args cannot config folder name, and the deployed version control system could be affected.
        private readonly string _SIMPLE_ARGS_FOLDER_TABLE = "Tables";
        private readonly string _SIMPLE_ARGS_FOLDER_STORED_PROCEDURE = "StoredProcedures";
        private readonly string _SIMPLE_ARGS_FOLDER_VIEWS = "Views";
        private readonly string _SIMPLE_ARGS_FOLDER_USER_DEFINED_FUNCTION = "UserDefinedFunctions";
        private readonly string _SIMPLE_ARGS_FOLDER_USER_DEFINED_AGGREGATE = "UserDefinedAggregates";
        private readonly string _SIMPLE_ARGS_FOLDER_USER_DEFINED_DATA_TYPE = "UserDefinedDataTypes";
        private readonly string _SIMPLE_ARGS_FOLDER_USER_DEFINED_TABLE_TYPE = "UserDefinedTableTypes";
        private readonly string _SIMPLE_ARGS_FOLDER_USER_DEFINED_TYPE = "UserDefinedTypes";
        

        private string[] _args;

        private ScripterConfig _config;





        public ScripterConfig Handle(string[] args)
        {
            _args = args;

            if (argsIsNotValid())
            {
                throw new Exception("Error: Invalid Args.");
            }
            
            _config = new ScripterConfig();
            fillWith_SimpleArgs(); // Todo: at present only simple args is supported
            
            return _config;

        }


        private bool argsIsNotValid()
        {
            if (_args.Length < 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void fillWith_SimpleArgs()
        {
            fillFolderName_WithSimpleArgs();
            fillConnectionInfo_WithSimpleArgs();
            fillOptions_WithSimpleArgs();
        }
        

        private void fillConnectionInfo_WithSimpleArgs()
        {
            _config.ServerName = _args[0];
            _config.Username = _args[1];
            _config.Password = _args[2];
            _config.DatabaseName = _args[3];
            _config.DatabaseType = DatabaseVersionType.SQLServer; 
        }


        private void fillFolderName_WithSimpleArgs()
        {
            _config.FolderName_Table = _SIMPLE_ARGS_FOLDER_TABLE;
            _config.FolderName_StoredProcedure = _SIMPLE_ARGS_FOLDER_STORED_PROCEDURE;
            _config.FolderName_View = _SIMPLE_ARGS_FOLDER_VIEWS;
            _config.FolderName_UserDefined_Function = _SIMPLE_ARGS_FOLDER_USER_DEFINED_FUNCTION;
            _config.FolderName_UserDefined_Aggregate = _SIMPLE_ARGS_FOLDER_USER_DEFINED_AGGREGATE;
            _config.FolderName_UserDefined_TableType = _SIMPLE_ARGS_FOLDER_USER_DEFINED_TABLE_TYPE;
            _config.FolderName_UserDefined_Type = _SIMPLE_ARGS_FOLDER_USER_DEFINED_TYPE;
            _config.FolderName_UserDefined_DataType = _SIMPLE_ARGS_FOLDER_USER_DEFINED_DATA_TYPE;


            _config.OutputDiretoryRoot = _args[4];
        }
        

        private void fillOptions_WithSimpleArgs()
        {
            if (_args.Length < 6)
            {
                _config.TheDatabaseObjectTypes = DatabaseObjectType.All;
                return;
            }

            _config.TheDatabaseObjectTypes = DatabaseObjectType.None;

            char[] inputParameterExportObjects = _args[5].ToUpper().ToCharArray();

            if (Array.IndexOf(inputParameterExportObjects, 'S') != -1) 
                _config.TheDatabaseObjectTypes = _config.TheDatabaseObjectTypes | DatabaseObjectType.StoredProcedure;
            

            if (Array.IndexOf(inputParameterExportObjects, 'T') != -1)
                _config.TheDatabaseObjectTypes = _config.TheDatabaseObjectTypes | DatabaseObjectType.Table;


            if (Array.IndexOf(inputParameterExportObjects, 'V') != -1)
                _config.TheDatabaseObjectTypes = _config.TheDatabaseObjectTypes | DatabaseObjectType.View;


            if (Array.IndexOf(inputParameterExportObjects, 'U') != -1)
            {
                _config.TheDatabaseObjectTypes = _config.TheDatabaseObjectTypes | DatabaseObjectType.UserDefined_Function;
                _config.TheDatabaseObjectTypes = _config.TheDatabaseObjectTypes | DatabaseObjectType.UserDefined_Aggregate;
                _config.TheDatabaseObjectTypes = _config.TheDatabaseObjectTypes | DatabaseObjectType.UserDefined_DataType;
                _config.TheDatabaseObjectTypes = _config.TheDatabaseObjectTypes | DatabaseObjectType.UserDefined_TableType;
                _config.TheDatabaseObjectTypes = _config.TheDatabaseObjectTypes | DatabaseObjectType.UserDefined_Type;

            }
                
            // legacy support, the U would include all of user defined objects
            if (Array.IndexOf(inputParameterExportObjects, 'A') != -1)
                _config.TheDatabaseObjectTypes = _config.TheDatabaseObjectTypes | DatabaseObjectType.UserDefined_Aggregate;

            
            if (Array.IndexOf(inputParameterExportObjects, 'F') != -1)
                _config.TheDatabaseObjectTypes = _config.TheDatabaseObjectTypes | DatabaseObjectType.UserDefined_Function;
            

            if (_config.TheDatabaseObjectTypes == DatabaseObjectType.None) 
                _config.TheDatabaseObjectTypes = DatabaseObjectType.All;
            
        }




    }
}
