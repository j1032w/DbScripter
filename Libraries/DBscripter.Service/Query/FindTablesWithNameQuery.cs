using System.Collections.Generic;
using DBScripter.Domain;

namespace DBScripter.Service.Query
{
    public class FindTablesWithNameQuery : IQuery<IEnumerable<SqlObjectScript>>
    {
        public IRepository Repository { get; set; }

        public string NameInclude { get; set; }
    }
}
