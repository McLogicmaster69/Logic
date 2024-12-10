using UnityEngine;

namespace Logic.Files
{
    public class FileErrorMessage
    {
        public FileError Error { get; private set; }
        public string Message { get; private set; }

        public static FileErrorMessage None => new FileErrorMessage(FileError.None, "");

        public FileErrorMessage(FileError error, string message)
        {
            Error = error;
            Message = message;
        }
    }
}