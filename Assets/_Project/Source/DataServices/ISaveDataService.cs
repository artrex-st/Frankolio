
namespace DataService
{
    public interface ISaveDataService
    {
        public GameData GameData { get; }
        public void Initialize(string fileName, bool useEncryption);
        void SaveData(GameData data);
        public void SaveGame();
    }
}
