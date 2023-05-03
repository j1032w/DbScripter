namespace DBScripter.Service.Command
{
    public class CreateFileCommand
    {
        public string DirectoryPath { get; set; }

        public string Filename { get; set; }

        public string Text { get; set; }
    }
}
