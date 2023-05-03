using DBScripter.Domain;
using DBScripter.Service;
using DBScripter.Service.Command;
using DBScripter.Service.Factory;
using Microsoft.Practices.Unity;

namespace DBScripter.CompositionRoot
{
    public class DBScripterContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            #region Data
            
            Container.RegisterType<SqlObjectScript, SqlObjectScript>();

            #endregion
            


            #region Domain

            #endregion
            


            #region Service
            
            Container.RegisterType<ICommandHandler<ClearDirectoryCommand>, ClearDirectoryCommandHandler>();
            Container.RegisterType<ICommandHandler<LogCommand>, LogToConsoleCommandHandler>();
            Container.RegisterType<ICommandHandler<CreateFileCommand>, CreateFileCommandHandler>();
            Container.RegisterType<ICommandHandler<WriteScriptsCommand>, WriteScriptsCommandHandler>();
            Container.RegisterType<ICommandHandler<ScriptDatabaseCommand>, ScriptDatabaseCommandHandler>();


            Container.RegisterType<IFactoryHandler<string[], ScripterConfig>, ScripterConfigFactoryHandler>();
            Container.RegisterType<IFactoryHandler<ScripterConfig, IRepository>, RepositoryFactoryHandler>();

            Container.RegisterType<IScripterController, ScripterController>();

            #endregion
        }
    }
}
