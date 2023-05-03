using Microsoft.SqlServer.Management.Smo;

namespace DBScripter.Domain
{
    public class ScripterConfig
    {
     
        public string ServerName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }

        public DatabaseVersionType DatabaseType { get; set; }
        public string OutputDiretoryRoot { get; set; }
       
        public DatabaseObjectType TheDatabaseObjectTypes { get; set; }

        public string FolderName_Table { set; get; }
        public string FolderName_StoredProcedure { set; get; }
        public string FolderName_View { set; get; }

        public string FolderName_UserDefined_Function { set; get; }
        public string FolderName_UserDefined_Aggregate { set; get; }
        public string FolderName_UserDefined_TableType { set; get; }
        public string FolderName_UserDefined_DataType { set; get; }
        public string FolderName_UserDefined_Type { set; get; }
        
       
    }


  
}
