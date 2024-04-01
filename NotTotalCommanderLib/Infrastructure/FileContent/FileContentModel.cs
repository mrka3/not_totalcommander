namespace NotTotalCommanderLib.Infrastructure.FileContent
{
    public class FileContentModel
    {
        public FileContentModel(string content, FileContentType fileContentType)
        {
            Content = content;
            FileContentType = fileContentType;
        }
    
        public string Content { get; private set; }
        public FileContentType FileContentType { get; private set; }
    }
}