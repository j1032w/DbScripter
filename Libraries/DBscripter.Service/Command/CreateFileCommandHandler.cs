using System.IO;

namespace DBScripter.Service.Command
{
    public class CreateFileCommandHandler : ICommandHandler<CreateFileCommand>
    {
        public void Handle(CreateFileCommand command)
        {
            string fileName = command.Filename;
            string filePath = Path.Combine(command.DirectoryPath, fileName);
            File.WriteAllText(filePath, command.Text);
        }
    }
}
