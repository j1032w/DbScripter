using DBScripter.Domain;
using DBScripter.Service.Command;
using DBScripter.Service.Factory;

namespace DBScripter.Service
{
    public class ScripterController : IScripterController
    {

        #region Private members

        private IFactoryHandler<string[], ScripterConfig> _scripterConfigFactoryHandler;
        private IFactoryHandler<ScripterConfig, IRepository> _repositoryFactoryHandler; 
        private ICommandHandler<ScriptDatabaseCommand> _scriptDatabaseCommandHandler;
        
        #endregion




        public ScripterController(  IFactoryHandler<string[], ScripterConfig> scripterConfigFactoryHandler,
                                    IFactoryHandler<ScripterConfig, IRepository> repostioryFactoryHandler,
                                    ICommandHandler<ScriptDatabaseCommand> scriptDatabaseCommandHandler)
        {
            _scripterConfigFactoryHandler = scripterConfigFactoryHandler;
            _repositoryFactoryHandler = repostioryFactoryHandler;
            _scriptDatabaseCommandHandler = scriptDatabaseCommandHandler;
        }



        
        
        
        public void Script(string[] args)
        {
            ScripterConfig config = _scripterConfigFactoryHandler.Handle(args);
            IRepository repository = _repositoryFactoryHandler.Handle(config);

            ScriptDatabaseCommand scriptDatabaseCommand = new ScriptDatabaseCommand() { Config = config, Repository = repository };
            _scriptDatabaseCommandHandler.Handle(scriptDatabaseCommand);
        }

        
    }
}
