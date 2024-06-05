using System;
using System.Collections.Generic;
using System.IO;
using Systems.Persistence;
using UnityEngine;

namespace Systems.Persistance
{
    public class FileDataService : IDataService 
    {
        ISerializer serializer;
        string dataPath;
        string fileExtension;

        public FileDataService(ISerializer serializer) {

            this.dataPath = Application.streamingAssetsPath + "/Analiticas/";
            this.fileExtension = "json";
            this.serializer = serializer;
        }

        string GetPathToFile(string fileName) 
        {
            return Path.Combine(dataPath, string.Concat(fileName, ".", fileExtension));
        }

        public void Save (GameData data, bool overwrite = true)
        {
            string fileLocation = GetPathToFile(data.Name);

            if (!overwrite && File.Exists(fileLocation)) {
                throw new IOException(message: $"The file '{data.Name}.{fileExtension}'already exist and cannot be overwriten.");
            }

            File.WriteAllText(path: fileLocation, contents: serializer.Serialize(data));
        }

        public GameData Load (string name) {
            string fileLocation = GetPathToFile(name);

            if (!File.Exists(fileLocation)) {
                throw new ArgumentException(message: $"No persisted GameData With name '{name}");
            }

            return serializer.Deserialize<GameData>(json:File.ReadAllText(fileLocation));
        }

        public void Delete (string name)
        {
            string fileLocation = GetPathToFile(name);

            if (File.Exists(fileLocation)) {
                File.Delete(fileLocation);
            }
        }   
        
        public void DeleteAll () {
            foreach (string filePath in Directory.GetFiles(dataPath))
            {
                File.Delete(filePath);
            }
        }

        public IEnumerable<string> ListSaves () {
            foreach (string path in Directory.EnumerateFiles(dataPath)) {
                if (Path.GetExtension(path) == fileExtension)                 
                { 
                    yield return Path.GetFileNameWithoutExtension(path);
                }
            }
        }
    }
}
