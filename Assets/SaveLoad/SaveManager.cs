using Logic.Files.Profiles;
using Logic.Menu;
using System;
using System.IO;
using UnityEngine;

namespace Logic.Files
{

    public static class SaveManager
    {
        public static string CurrentFilePath { get; private set; } = "";

        public const string SAVE_DIRECTORY = "saves";
        public const string FILE_EXTENSION = ".lc";

        public static string SaveDirectory => $"{Application.persistentDataPath}/{SAVE_DIRECTORY}";

        public static void CheckSaveDirectoryExists()
        {
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);
        }

        public static FileError SaveToFilePath(Action<TextUpdateArgs> statusUpdate = null)
        {
            RunStatusUpdate(statusUpdate, "Saving...");
            if (string.IsNullOrEmpty(CurrentFilePath))
            {
                RunStatusUpdate(statusUpdate, "ERROR: Could not save to file path, try Save As instead", Color.red);
                return FileError.NoSaveFilePath;
            }

            MasterSaveProfile profile = ObjectStorage.Main.CreateProfile(statusUpdate);

            RunStatusUpdate(statusUpdate, "Saving to file...");
            FileErrorMessage message = SaveSystem.Save<MasterSaveProfile>(profile, CurrentFilePath);

            if (message.Error == FileError.None)
            {
                RunStatusUpdate(statusUpdate, $"Saved! Path: {CurrentFilePath}");
                return FileError.None;
            }

            RunStatusUpdate(statusUpdate, FileErrorToString(message), Color.red);
            return message.Error;
        }

        public static FileError SaveToFilePath(string fileName, Action<TextUpdateArgs> statusUpdate = null)
        {
            CurrentFilePath = $"{SaveDirectory}/{fileName}{FILE_EXTENSION}";
            return SaveToFilePath(statusUpdate);
        }

        public static FileInfo[] GetAllFilesInDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(SaveDirectory);
            return dir.GetFiles($"*{FILE_EXTENSION}");
        }

        public static FileError LoadFileFromPath(string fileName, Action<TextUpdateArgs> statusUpdate = null)
        {
            RunStatusUpdate(statusUpdate, "Loading...");
            FileErrorMessage message = SaveSystem.Load<MasterSaveProfile>($"{SaveDirectory}/{fileName}", out MasterSaveProfile file);

            if(message.Error != FileError.None)
            {
                RunStatusUpdate(statusUpdate, FileErrorToString(message), Color.red);
                return message.Error;
            }

            ObjectStorage.Main.LoadMasterSaveProfile(file);
            return FileError.None;
        }

        private static void RunStatusUpdate(Action<TextUpdateArgs> statusUpdate, string text)
        {
            statusUpdate?.Invoke(new TextUpdateArgs(text) { Color = Color.white });
        }

        private static void RunStatusUpdate(Action<TextUpdateArgs> statusUpdate, string text, Color color)
        {
            statusUpdate?.Invoke(new TextUpdateArgs(text) { Color = color });
        }

        private static string FileErrorToString(FileErrorMessage message)
        {
            return message.Error switch
            {
                FileError.DirectoryNotFound => $"ERROR: Directory was not found! Message: {message.Message}",
                FileError.DriveNotFound => $"ERROR: Drive was not found! Message: {message.Message}",
                FileError.EndOfStream => $"ERROR: End of stream was reached! Message: {message.Message}",
                FileError.FileLoad => $"ERROR: Something went wrong while loading! Message: {message.Message}",
                FileError.FileNotFound => $"ERROR: File was not found! Message: {message.Message}",
                FileError.InternalBufferOverflow => $"ERROR: There was an internal buffer overflow! Message: {message.Message}",
                FileError.InvalidData => $"ERROR: Invalid data was inputted! Message: {message.Message}",
                FileError.None => "",
                FileError.NoSaveFilePath => $"ERROR: There is no saved file path! Message: {message.Message}",
                FileError.Other => $"ERROR: Unknown error occured! Message: {message.Message}",
                FileError.PathTooLong => $"ERROR: The path to the file was too long! Message: {message.Message}",
                _ => "",
            };
        }
    }
}