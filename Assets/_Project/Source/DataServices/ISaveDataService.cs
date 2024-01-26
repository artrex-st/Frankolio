
namespace DataService
{
    public interface ISaveDataService
    {
        public SettingsData SettingsData { get; }
        public void Initialize(string fileName, bool useEncryption);
        void SaveData(SettingsData data);
        public void SaveGame();
    }
}
