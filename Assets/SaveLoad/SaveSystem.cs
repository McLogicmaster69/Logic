using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Logic.Files
{
    /// <summary>
    /// Saves and loads any serialized class in and out from files
    /// </summary>
    public static class SaveSystem
    {
        /// <summary>
        /// The formatter used to create and read files
        /// </summary>
        private static readonly BinaryFormatter _formatter = new BinaryFormatter();

        /// <summary>
        /// Saves a serialized class to a file path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        public static void Save<T>(T file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            _formatter.Serialize(stream, file);
            stream.Close();
        }

        /// <summary>
        /// Saves a serialized class with a file name and extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        public static void Save<T>(T file, string fileName, string fileExtension)
        {
            string path = Application.persistentDataPath + $"/{fileName}.{fileExtension}";
            FileStream stream = new FileStream(path, FileMode.Create);
            _formatter.Serialize(stream, file);
            stream.Close();
        }

        /// <summary>
        /// Loads a serialized class from a file path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Load<T>(string path)
        {
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                T file = (T)_formatter.Deserialize(stream);
                stream.Close();
                return file;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Loads a serialized class from a file name and extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public static T Load<T>(string fileName, string fileExtension)
        {
            string path = Application.persistentDataPath + $"/{fileName}.{fileExtension}";
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                T file = (T)_formatter.Deserialize(stream);
                stream.Close();
                return file;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Checks if a file exists
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public static bool Exists(string fileName, string fileExtension)
        {
            string path = Application.persistentDataPath + $"/{fileName}.{fileExtension}";
            return File.Exists(path);
        }
    }
}
