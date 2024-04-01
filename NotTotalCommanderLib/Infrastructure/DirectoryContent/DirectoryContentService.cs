using System.Linq;
using NotTotalCommanderLib.Infrastructure.Directories;
using NotTotalCommanderLib.Infrastructure.Drives;
using NotTotalCommanderLib.Infrastructure.Files;

namespace NotTotalCommanderLib.Infrastructure.DirectoryContent
{
    public class DirectoryContentService
    {
        private readonly DirectoryService directoryService;
        private readonly DriveService driveService;
        private readonly FileService fileService;

        public DirectoryContentService(DirectoryService directoryService,
            DriveService driveService,
            FileService fileService)
        {
            this.directoryService = directoryService;
            this.driveService = driveService;
            this.fileService = fileService;
        }

        public DirectoryContentModel[] GetContent(string path)
        {
            DirectoryContentModel[] content;

            if (string.IsNullOrEmpty(path))
            {
                content = driveService.GetDrives()
                    .Select(x => new DirectoryContentModel(x.Name, DirectoryContentType.Drive))
                    .ToArray();
            }
            else
            {
                var directories = directoryService.GetDirectories(path)
                    .Select(x => new DirectoryContentModel(x.Name, DirectoryContentType.Directory));

                var files = fileService.GetFiles(path)
                    .Select(x => new DirectoryContentModel(x.Name, DirectoryContentType.File));

                content = directories.Concat(files).OrderBy(x => x.DirectoryContentType).ToArray();
            }

            return content;
        }

        public void CreateDirectory(string path)
        {
            directoryService.CreateDirectory(path);
        }

        public void DeleteDirectory(string path)
        {
            directoryService.DeleteDirectory(path);
        }
    }
}