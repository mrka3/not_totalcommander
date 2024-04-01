using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NotTotalCommanderLib.Infrastructure.Directories
{
    public class DirectoryService
    {
        public IEnumerable<DirectoryModel> GetDirectories(string path)
        {
            if (!Directory.Exists(path))
                throw new Exception("Такой папки не существует");

            return Directory.GetDirectories(path).Select(x => new DirectoryInfo(x))
                .Select(x => new DirectoryModel(x.Name)).ToArray();
        }

        public void CreateDirectory(string path)
        {
            var dirInfo = new DirectoryInfo(path);
        
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        public void DeleteDirectory(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            if (dirInfo.Exists)
            {
                dirInfo.Delete(true);
                Console.WriteLine("Каталог удален");
            }
            else
            {
                Console.WriteLine("Каталог не существует");
            }
        }
    }
}