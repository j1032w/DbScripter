using System.Collections.Generic;
using DBScripter.Domain;

namespace DBScripter.Service.Command
{
    public class WriteScriptsCommand
    {
        public IEnumerable<SqlObjectScript> Scripts { get; set; }

        public string OutputPath { get; set; }

        public DatabaseObjectType ObjectType { get; set; }
    }
}
