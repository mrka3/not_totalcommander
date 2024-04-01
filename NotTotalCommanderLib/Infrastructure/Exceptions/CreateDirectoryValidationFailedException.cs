using System;

namespace NotTotalCommanderLib.Infrastructure.Exceptions
{
    public class CreateDirectoryValidationFailedException : Exception
    {
        public CreateDirectoryValidationFailedException(string message) : base(message)
        {
        }
    }
}