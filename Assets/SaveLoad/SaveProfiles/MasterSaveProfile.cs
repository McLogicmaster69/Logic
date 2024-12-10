namespace Logic.Files.Profiles
{
    [System.Serializable]
    public class MasterSaveProfile
    {
        public string Version;
        public ComponentSaveProfile[] Components;
        public CableSaveProfile[] Cables;

        public MasterSaveProfile(string version, ComponentSaveProfile[] components, CableSaveProfile[] cables)
        {
            Version = version;
            Components = components;
            Cables = cables;
        }
    }
}