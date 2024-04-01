namespace NotTotalCommanderLib.Infrastructure.Directories
{
    public class DirectoryModel
    {
        public DirectoryModel(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}