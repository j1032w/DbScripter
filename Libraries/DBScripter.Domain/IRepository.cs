using System.Collections.Generic;
using Microsoft.SqlServer.Management.Smo;

namespace DBScripter.Domain
{
    public interface IRepository
    {
        string ServerName { set; get; }
        string Username  { set; get; }
        string Password  { set; get; }
        string DatabaseName  { set; get; }
        
        IEnumerable<SqlObjectScript> GetStoredProcedures();
        IEnumerable<SqlObjectScript> GetTables();
        IEnumerable<SqlObjectScript> GetViews();

        IEnumerable<SqlObjectScript> GetUserDefined_Aggregates();
        IEnumerable<SqlObjectScript> GetUserDefined_Functions();
        IEnumerable<SqlObjectScript> GetUserDefined_TableTypes();
        IEnumerable<SqlObjectScript> GetUserDefined_DataTypes();
        IEnumerable<SqlObjectScript> GetUserDefined_Types();

        CompatibilityLevel GetCompatibilityLevel();
    }
}
