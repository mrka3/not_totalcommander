namespace NotTotalCommanderLib.Infrastructure.Files
{
    public class FileModel
    {
        public FileModel(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}