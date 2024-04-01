using System;
using System.IO;

namespace NotTotalCommanderLib.Infrastructure.FileContent
{
    public class FileContentService
    {
        public FileContentModel GetBase64Image(string path, string extension, bool withPrefix = false)
        {
            var imageBytes = File.ReadAllBytes(path);
            var imageBase64 = Convert.ToBase64String(imageBytes);
            
            string format;
            
            switch (extension)
            {
                case ".jpg":
                    format = "jpeg";
                    break;
                default:
                    format = extension.Replace(".", string.Empty);
                    break;
            }

            var content = withPrefix ? $"data:image/{format};base64,{imageBase64}" : imageBase64;

            return new FileContentModel(content, FileContentType.Image);
        }

        public FileContentModel GetFileText(string path)
        {
            return new FileContentModel(File.ReadAllText(path), FileContentType.Text);
        }
    }
}