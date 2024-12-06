namespace Logic.Files
{
    public enum FileError
    {
        None,
        NoSaveFilePath,
        DirectoryNotFound,
        DriveNotFound,
        EndOfStream,
        FileLoad,
        InternalBufferOverflow,
        InvalidData,
        PathTooLong,
        Other
    }
}
