using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace DataService
{
    public sealed class FileDataHandler
    {
        private string _dataDirPath = "";
        private string _dataFileName = "";
        private bool _useEncryption = false;
        private const string EncryptionCodeWord = "3NCr!p*t@çõe$:D";

        public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
        {
            _dataDirPath = dataDirPath;
            _dataFileName = dataFileName;
            _useEncryption = useEncryption;
        }

        public SettingsData Load()
        {
            string fullPath = Path.Combine(_dataDirPath, _dataFileName);
            SettingsData loadedData = null;

            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad = "";

                    using (FileStream stream = new(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    if (_useEncryption)
                    {
                        dataToLoad = EncryptDecrypt(dataToLoad);
                    }

                    loadedData = JsonConvert.DeserializeObject<SettingsData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error occured when trying to Load data from file:{fullPath} \n error:{e}");

                    throw;
                }
            }

            return loadedData;
        }

        public void Save(SettingsData data)
        {
            string fullPath = Path.Combine(_dataDirPath, _dataFileName);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                string dataToStore = JsonConvert.SerializeObject(data);

                if (_useEncryption)
                {
                    dataToStore = EncryptDecrypt(dataToStore);
                }

                using (FileStream stream = new(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occured when trying to save data to file:{fullPath} \n error:{e}");

                throw;
            }
        }

        private string EncryptDecrypt(string data)
        {
            string modifiedData = "";

            for (int i = 0; i < data.Length; i++)
            {
                modifiedData += (char)(data[i] ^ EncryptionCodeWord[i % EncryptionCodeWord.Length]);
            }

            return modifiedData;
        }
    }
}
