using System.Collections.Generic;
using System.Linq;

namespace NotTotalCommanderLib.Infrastructure.Routing
{
    public class PathService
    {
        private readonly List<string> pathStore = new List<string>();

        public string GetPath()
        {
            return string.Join("/", pathStore);
        }

        public void AddPath(string directoryName)
        {
            if (!string.IsNullOrEmpty(directoryName))
                pathStore.Add(directoryName);
        }

        public void RemoveLast()
        {
            pathStore.Remove(pathStore.Last());
        }
    }
}