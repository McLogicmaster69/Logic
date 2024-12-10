using Logic.Cables;
using Logic.Files.Profiles;
using Logic.Menu;
using Logic.Nodes;
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Logic
{
    public class ObjectStorage : MonoBehaviour
    {
        public static ObjectStorage Main { get; private set; }

        [SerializeField] private GameObject _ANDGate;
        [SerializeField] private GameObject _ORGate;
        [SerializeField] private GameObject _NOTGate;
        [SerializeField] private GameObject _XORGate;
        [SerializeField] private GameObject _NANDGate;
        [SerializeField] private GameObject _NORGate;
        [SerializeField] private GameObject _switch;
        [SerializeField] private GameObject _bulb;
        [SerializeField] private GameObject _clock;
        [SerializeField] private GameObject _button;
        [SerializeField] private GameObject _constantOne;
        [SerializeField] private GameObject _constantZero;
        [SerializeField] private GameObject _ZOInput;
        [SerializeField] private GameObject _ZOOutput;
        [SerializeField] private GameObject _cable;

        private void Awake()
        {
            Main = this;
        }

        public LogicComponent[] GetLogicGates() => GetComponentsInChildren<LogicComponent>();

        public CableFlow[] GetCableFlows() => GetComponentsInChildren<CableFlow>();

        public MasterSaveProfile CreateProfile(Action<TextUpdateArgs> statusUpdate = null)
        {
            LogicComponent[] gates = GetLogicGates();
            CableFlow[] cables = GetCableFlows();

            ComponentSaveProfile[] componentProfiles = new ComponentSaveProfile[gates.Length];
            CableSaveProfile[] cableProfiles = new CableSaveProfile[cables.Length];

            RunStatusUpdate(statusUpdate, "Saving (Creating component profiles)");
            for (int i = 0; i < gates.Length; i++)
            {
                componentProfiles[i] = new ComponentSaveProfile(gates[i]);
            }

            RunStatusUpdate(statusUpdate, "Saving (Creating cable profiles)");
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
                    if (inputIndex != -1 && outputIndex != -1)
                        break;
                }

                cableProfiles[i] = new CableSaveProfile(inputIndex, cable.InputPin.ID, outputIndex, cable.OutputPin.ID);
            }

            RunStatusUpdate(statusUpdate, "Saving (Creating master profile)");
            return new MasterSaveProfile(ProjectInfo.CURRENT_VERSION, componentProfiles, cableProfiles);
        }

        public void LoadMasterSaveProfile(MasterSaveProfile profile)
        {
            DestroyAllChildren();
            List<GameObject> components = BuildComponents(profile);
            BuildCables(profile, components);
        }

        private List<GameObject> BuildComponents(MasterSaveProfile profile)
        {
            List<GameObject> components = new List<GameObject>();
            foreach(ComponentSaveProfile comp in profile.Components)
            {
                GameObject o = Instantiate(GetGateObject(comp.GateType), new Vector3(comp.X, comp.Y, 0f), Quaternion.identity, transform);
                components.Add(o);
            }
            return components;
        }

        private void BuildCables(MasterSaveProfile profile, List<GameObject> components)
        {
            foreach(CableSaveProfile cable in profile.Cables)
            {
                BuildCable(cable, components[cable.InputIndex], components[cable.OutputIndex]);
            }
        }

        private void BuildCable(CableSaveProfile profile, GameObject inputNode, GameObject outputNode)
        {
            Pin inputPin = inputNode.GetComponent<LogicComponent>().Pins.GetOutputPinObject(profile.InputPin);
            Pin outputPin = outputNode.GetComponent<LogicComponent>().Pins.GetInputPinObject(profile.OutputPin);

            GameObject cable = Instantiate(_cable, transform);
            CableRenderer renderer = cable.GetComponent<CableRenderer>();
            CableFlow flow = cable.GetComponent<CableFlow>();
            renderer.SetNodes(inputPin.gameObject, outputPin.gameObject);

            inputPin.ConnectCable(flow);
            outputPin.ConnectCable(flow);
            flow.SetInputPin(inputPin);
            flow.SetOutputPin(outputPin);
        }

        private GameObject GetGateObject(int gateType)
        {
            return gateType switch
            {
                0 => _ANDGate,
                1 => _ORGate,
                2 => _NOTGate,
                3 => _XORGate,
                4 => _NANDGate,
                5 => _NORGate,
                6 => _switch,
                7 => _bulb,
                8 => _clock,
                9 => _button,
                10 => _constantOne,
                11 => _constantZero,
                12 => _ZOInput,
                13 => _ZOOutput,
                _ => null
            };
        }

        private void DestroyAllChildren()
        {
            foreach(Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        private void RunStatusUpdate(Action<TextUpdateArgs> statusUpdate, string text)
        {
            statusUpdate?.Invoke(new TextUpdateArgs(text) { Color = Color.white });
        }

        private void RunStatusUpdate(Action<TextUpdateArgs> statusUpdate, string text, Color color)
        {
            statusUpdate?.Invoke(new TextUpdateArgs(text) { Color = color });
        }
    }
}