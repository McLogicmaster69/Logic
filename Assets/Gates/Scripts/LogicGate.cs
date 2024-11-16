using Logic.Cables;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Nodes
{
    public enum LogicState
    {
        AND,
        OR
    }

    public class LogicGate : MonoBehaviour
    {
        [SerializeField] private LogicState _state;
        [SerializeField] private List<CableFlow> _inputs;
        public bool Output { get; private set; }

        private void Update()
        {
            SetOutput();
        }

        private void SetOutput()
        {
            switch (_state)
            {
                case LogicState.AND:
                    if (_inputs.Count < 2)
                        return;
                    Output = _inputs[0].Output && _inputs[1].Output;
                    break;
                case LogicState.OR:
                    if (_inputs.Count < 2)
                        return;
                    Output = _inputs[0].Output || _inputs[1].Output;
                    break;
            }
        }
    }
}