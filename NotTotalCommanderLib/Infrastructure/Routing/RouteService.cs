using System;
using System.IO;
using NotTotalCommanderLib.Infrastructure.Directories;
using NotTotalCommanderLib.Infrastructure.DirectoryContent;
using NotTotalCommanderLib.Infrastructure.FileContent;

namespace NotTotalCommanderLib.Infrastructure.Routing
{
    public class RouteService
    {
        private readonly DirectoryContentService directoryContentService;
        private readonly FileContentService fileContentService;
        private readonly PathService pathService;
        private readonly DirectoryValidator directoryValidator;

        public RouteService(DirectoryContentService directoryContentService,
            FileContentService fileContentService,
            PathService pathService, DirectoryValidator directoryValidator)
        {
            this.directoryContentService = directoryContentService;
            this.fileContentService = fileContentService;
            this.pathService = pathService;
            this.directoryValidator = directoryValidator;
        }

        public GetDirectoryContentModel GetCurrentDirectoryContent()
        {
            var currentPath = pathService.GetPath();

            var directoryContent = directoryContentService.GetContent(currentPath);

            return new GetDirectoryContentModel(directoryContent, isRoot: string.IsNullOrEmpty(currentPath));
        }

        public void DirectoryDown(string path)
        {
            pathService.AddPath(path);
        }

        public void DirectoryUp()
        {
            pathService.RemoveLast();
        }

        public void DeleteDirectory(string directoryName)
        {
            var currentPath = pathService.GetPath();
            directoryContentService.DeleteDirectory($"{currentPath}/{directoryName}");
        }

        public void CreateDirectory(string directoryName)
        {
            directoryValidator.ValidateOnCreation(directoryName);
            
            var currentPath = pathService.GetPath();
            directoryContentService.CreateDirectory($"{currentPath}/{directoryName}");
        }

        public FileContentModel GetFileContent(string fileName, bool withPrefix = false)
        {
            var currentPath = pathService.GetPath();
            var fullpath = $"{currentPath}/{fileName}";
            var extension = Path.GetExtension(fullpath);

            switch (extension)
            {
                case ".jpg":
                case ".bmp":
                case ".png":
                    return fileContentService.GetBase64Image(fullpath, extension, withPrefix);
                case ".txt":
                    return fileContentService.GetFileText(fullpath);
                default:
                    return new FileContentModel(string.Empty, FileContentType.Other);
            }
        }
    }
}