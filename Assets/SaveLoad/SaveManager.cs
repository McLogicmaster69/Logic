using UnityEngine;

namespace Logic.Files
{
    public enum FileErrors
    {
        None,
        NoSaveFilePath,
        Other
    }

    public static class SaveManager
    {
        private static string _currentFilePath = "";

        public static FileErrors SaveToFilePath()
        {
            if (string.IsNullOrEmpty(_currentFilePath))
                return FileErrors.NoSaveFilePath;

            return FileErrors.None;
        }
    }
}