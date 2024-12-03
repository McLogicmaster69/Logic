using Logic.Cables;

namespace Logic.Files.Profiles
{
    [System.Serializable]
    public class CableSaveProfile
    {
        public int InputIndex;
        public int OutputIndex;

        public CableSaveProfile(int input, int output)
        {
            InputIndex = input;
            OutputIndex = output;
        }
    }
}
