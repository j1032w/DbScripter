using System.IO;

namespace DBScripter.Service.Command
{
    public class ClearDirectoryCommandHandler : ICommandHandler<ClearDirectoryCommand>
    {
        public void Handle(ClearDirectoryCommand command)
        {
            if (Directory.Exists(command.DirectoryPath))
            {
                Directory.Delete(command.DirectoryPath, true);
            }

            Directory.CreateDirectory(command.DirectoryPath);
        }
    }
}
