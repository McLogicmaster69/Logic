namespace Logic.Files
{
    public enum FileError
    {
        None,

        DirectoryNotFound,
        DriveNotFound,
        EndOfStream,
        FileLoad,
        FileNotFound,
        InternalBufferOverflow,
        InvalidData,
        NoSaveFilePath,
        PathTooLong,
        Other
    }
}
