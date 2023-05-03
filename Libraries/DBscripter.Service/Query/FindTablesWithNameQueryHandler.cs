using System.Collections.Generic;
using DBScripter.Domain;

namespace DBScripter.Service.Query
{
    public class FindTablesWithNameQueryHandler : IQueryHandler<FindTablesWithNameQuery, IEnumerable<SqlObjectScript>>
    {
        public IEnumerable<SqlObjectScript> Handle(FindTablesWithNameQuery query)
        {
            return query.Repository.GetTables();
        }
    }
}
