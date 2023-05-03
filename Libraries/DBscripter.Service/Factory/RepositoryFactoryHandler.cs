using System;
using DBScripter.Data;
using DBScripter.Domain;

namespace DBScripter.Service.Factory
{
    public class RepositoryFactoryHandler : IFactoryHandler<ScripterConfig, IRepository>
    {
        public IRepository Handle(ScripterConfig config)
        {
            IRepository repository;

            switch(config.DatabaseType)
            {
                case DatabaseVersionType.SQLServer:
                    repository = new SqlServerRepository();
                    break;

                default:
                    throw new Exception("Error: Unsupported database version. " + config.DatabaseType);

            }

            repository.ServerName =config.ServerName;
            repository.Username = config.Username;
            repository.Password = config.Password;
            repository.DatabaseName = config.DatabaseName;

            return repository;
        }
    }
}
