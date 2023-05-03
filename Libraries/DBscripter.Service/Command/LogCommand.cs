
using System;

namespace DBScripter.Service.Command
{
    public class LogCommand
    {
        public LogCommand()
        {
            Color = ConsoleColor.Gray;
        }


        public string Text { set; get; }

        public ConsoleColor Color { set; get; }
    }
}
