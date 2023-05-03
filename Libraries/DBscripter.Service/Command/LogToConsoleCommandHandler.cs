using System;

namespace DBScripter.Service.Command
{
    public class LogToConsoleCommandHandler : ICommandHandler<LogCommand>
    {
        public void Handle(LogCommand command)
        {
            Console.ForegroundColor = command.Color;    
            Console.WriteLine(command.Text);
            Console.ResetColor();
        }
    }
}
