namespace NotTotalCommanderLib.Infrastructure.Drives
{
    public class DriveModel
    {
        public DriveModel(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}