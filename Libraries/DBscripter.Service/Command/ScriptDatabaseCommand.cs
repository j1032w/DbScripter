using DBScripter.Domain;

namespace DBScripter.Service.Command
{
    public class ScriptDatabaseCommand
    {
        public ScripterConfig Config { set; get; }

        public IRepository Repository { get; set; }

    }
}
