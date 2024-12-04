namespace Logic.Files.Profiles
{
    [System.Serializable]
    public class MasterSaveProfile
    {
        public ComponentSaveProfile[] Components;
        public CableSaveProfile[] Cables;

        public MasterSaveProfile(ComponentSaveProfile[] components, CableSaveProfile[] cables)
        {
            Components = components;
            Cables = cables;
        }
    }
}