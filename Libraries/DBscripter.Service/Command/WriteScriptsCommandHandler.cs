using System;
using System.Linq;
using DBScripter.Domain;

namespace DBScripter.Service.Command
{
    public class WriteScriptsCommandHandler : ICommandHandler<WriteScriptsCommand>
    {
        private ICommandHandler<LogCommand> _logCommandHandler;

        private ICommandHandler<CreateFileCommand> _writeFileCommandHandler;

        private ICommandHandler<ClearDirectoryCommand> _clearDirectoryCommandHandler;

        private WriteScriptsCommand _writeScriptsCommand;

        private int _counter;



        public WriteScriptsCommandHandler( ICommandHandler<LogCommand> logCommandHandler,
                                            ICommandHandler<CreateFileCommand> writeFileCommandHandler,
                                            ICommandHandler<ClearDirectoryCommand> clearDirectoryHander )
        {
            _logCommandHandler = logCommandHandler;
            _writeFileCommandHandler = writeFileCommandHandler;
            _clearDirectoryCommandHandler = clearDirectoryHander;
        }





        public void Handle(WriteScriptsCommand scriptObjectsCommand)
        {
            _writeScriptsCommand = scriptObjectsCommand;

            Log("Scripting " + _writeScriptsCommand.ObjectType + " . . .", ConsoleColor.Cyan);
            ClearOutputDirectory();
            WriteStatements();
            Log("Total of " + _counter + " " + _writeScriptsCommand.ObjectType + "(s) have been scripted. \n\n", ConsoleColor.Green);
        }



        private void ClearOutputDirectory()
        {
            ClearDirectoryCommand clearDirectoryCommand = new ClearDirectoryCommand() { DirectoryPath = _writeScriptsCommand.OutputPath };
            _clearDirectoryCommandHandler.Handle(clearDirectoryCommand);
        }

        private void WriteStatements()
        {
            _counter = 0;

            if (_writeScriptsCommand.Scripts.Any() == false)
            {
                return;
            }


            CreateFileCommand writeFileCommand = new CreateFileCommand() { DirectoryPath = _writeScriptsCommand.OutputPath };
            foreach (SqlObjectScript script in _writeScriptsCommand.Scripts)
            {
                writeFileCommand.Filename = script.Name + ".sql";
                writeFileCommand.Text = script.Text;
                _writeFileCommandHandler.Handle(writeFileCommand);

                _counter++;
                Log("  " + script.Name);
            }
        }


        private void Log(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            LogCommand logCommand = new LogCommand() { Text = message, Color = color};
            _logCommandHandler.Handle(logCommand);
        }
    }
}
