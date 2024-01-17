namespace DataService
{
    [System.Serializable]
    public class GameData
    {
        public float MasterVolume;
        public float MusicVolume;
        public float SfxVolume;
        public float UiSfxVolume;

        public GameData()
        {
            MasterVolume = 1f;
            MusicVolume = 1f;
            SfxVolume = 1f;
            UiSfxVolume = 1f;
        }
    }
}
