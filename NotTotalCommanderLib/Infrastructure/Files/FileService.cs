using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NotTotalCommanderLib.Infrastructure.Files
{
    public class FileService
    {
        public IEnumerable<FileModel> GetFiles(string path)
        {
            return Directory.GetFiles(path)
                .Select(Path.GetFileName)
                .Select(x => new FileModel(x));
        }

    }
}