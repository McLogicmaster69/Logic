using Logic.Files.Profiles;
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

            MasterSaveProfile profile = ObjectStorage._main.CreateProfile();
            SaveSystem.Save<MasterSaveProfile>(profile, _currentFilePath);
            return FileErrors.None;
        }
    }
}