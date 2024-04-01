using NotTotalCommanderLib.Infrastructure.DirectoryContent;

namespace NotTotalCommanderLib.Infrastructure.Routing
{
    public class GetDirectoryContentModel
    {
        public GetDirectoryContentModel(DirectoryContentModel[] directoryContent, bool isRoot)
        {
            DirectoryContent = directoryContent;
            IsRoot = isRoot;
        }

        public DirectoryContentModel[] DirectoryContent { get; }
        public bool IsRoot { get; }
    }
}