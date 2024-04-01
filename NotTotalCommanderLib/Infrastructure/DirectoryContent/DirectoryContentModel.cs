namespace NotTotalCommanderLib.Infrastructure.DirectoryContent
{
    public class DirectoryContentModel
    {
        public DirectoryContentModel(string name, DirectoryContentType directoryContentType)
        {
            Name = name;
            DirectoryContentType = directoryContentType;
        }

        public string Name { get; set; }
        public DirectoryContentType DirectoryContentType { get; set; }
    }
}