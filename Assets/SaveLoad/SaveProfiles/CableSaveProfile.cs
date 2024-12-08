using Logic.Cables;

namespace Logic.Files.Profiles
{
    [System.Serializable]
    public class CableSaveProfile
    {
        public int InputIndex;
        public int InputPin;
        public int OutputIndex;
        public int OutputPin;

        public CableSaveProfile(int input, int inputPin, int output, int outputPin)
        {
            InputIndex = input;
            InputPin = inputPin;
            OutputIndex = output;
            OutputPin = outputPin;
        }
    }
}
