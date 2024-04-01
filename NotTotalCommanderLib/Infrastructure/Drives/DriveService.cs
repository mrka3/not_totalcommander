using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NotTotalCommanderLib.Infrastructure.Drives
{
    public class DriveService
    {
        public IEnumerable<DriveModel> GetDrives()
        {
            return DriveInfo.GetDrives().Select(x => new DriveModel(x.Name));
        }
    }
}