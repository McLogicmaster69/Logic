using Logic.Cables;
using Logic.Files.Profiles;
using Logic.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class ObjectStorage : MonoBehaviour
    {
        public static ObjectStorage _main { get; private set; }

        private void Awake()
        {
            _main = this;
        }

        public LogicGate[] GetLogicGates() => GetComponentsInChildren<LogicGate>();

        public CableFlow[] GetCableFlows() => GetComponentsInChildren<CableFlow>();

        public MasterSaveProfile CreateProfile()
        {
            LogicGate[] gates = GetLogicGates();
            CableFlow[] cables = GetCableFlows();

            ComponentSaveProfile[] componentProfiles = new ComponentSaveProfile[gates.Length];
            CableSaveProfile[] cableProfiles = new CableSaveProfile[cables.Length];

            for (int i = 0; i < gates.Length; i++)
            {
                componentProfiles[i] = new ComponentSaveProfile(gates[i]);
            }

            for (int i = 0; i < cables.Length; i++)
            {
                int inputIndex = -1;
                int outputIndex = -1;
                CableFlow cable = cables[i];

                for (int j = 0; j < gates.Length; j++)
                {
                    if (gates[j] == cable.InputPin.Gate)
                        inputIndex = j;
                    if (gates[j] == cable.OutputPin.Gate)
                        outputIndex = j;
                    if (inputIndex != -1 || outputIndex != -1)
                        break;
                }

                cableProfiles[i] = new CableSaveProfile(inputIndex, outputIndex);
            }

            return new MasterSaveProfile(componentProfiles, cableProfiles);
        }
    }
}