using System.Collections.Generic;
using System.Linq;
using NotTotalCommanderLib.Infrastructure.Exceptions;

namespace NotTotalCommanderLib.Infrastructure.Directories
{
    public class DirectoryValidator
    {
        private static readonly char[] ForbiddenSymbols = new[]
        {
            '\\', '"', '\\', '/', ':', '|', '<', '>', '*', '?'
        };

        public void ValidateOnCreation(string directoryName)
        {
            var errors = new List<string>();

            if (directoryName.Length > 255)
                errors.Add("Длина названия папки не должна превышать 255 символов");

            if (directoryName.Any(x => ForbiddenSymbols.Contains(x)))
                errors.Add("Название папки не должно содержать символов - " + string.Join(",", ForbiddenSymbols));

            if (errors.Any())
                throw new CreateDirectoryValidationFailedException(string.Join("\r\n", errors));
        }
    }
}