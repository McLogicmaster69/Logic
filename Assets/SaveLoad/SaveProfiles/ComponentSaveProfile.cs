using Logic.Nodes;

namespace Logic.Files.Profiles
{
    [System.Serializable]
    public class ComponentSaveProfile
    {
        public float X;
        public float Y;
        public int GateType;

        public ComponentSaveProfile(LogicGate gate)
        {
            X = gate.transform.position.x;
            Y = gate.transform.position.y;
            GateType = (int)gate.State;
        }
    }
}