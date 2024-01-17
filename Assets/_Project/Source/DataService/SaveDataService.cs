using UnityEngine;

namespace DataService
{
    public sealed class SaveDataService : MonoBehaviour, ISaveDataService
    {
        public GameData GameData { get; private set; }
        private bool _useEncryption;
        private FileDataHandler _fileDataHandler;

        public void Initialize(string fileName, bool useEncryption)
        {
            _useEncryption = useEncryption;
#if UNITY_EDITOR
            _fileDataHandler = new FileDataHandler(Application.dataPath + "/Saves/", fileName, _useEncryption);
#else
            _fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, _useEncryption);
#endif
            LoadGame();
        }

        public void SaveData(GameData data)
        {
            GameData = data;
        }

        public void SaveGame()
        {
            _fileDataHandler.Save(GameData);
        }

        private void NewGame()
        {
            GameData = new GameData();
        }

        private void LoadGame()
        {
            GameData = _fileDataHandler.Load();

            if(GameData == null)
            {
                Debug.Log($"<color=green>New Game</color>, set all to default");
                NewGame();
            }
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }
    }
}

