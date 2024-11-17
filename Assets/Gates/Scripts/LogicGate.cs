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

        public bool Output { get; private set; }

        private PinManager _pins;

        private void Start()
        {
            _pins = GetComponent<PinManager>();
        }

        private void Update()
        {
            Output = GetOutput();
        }

        /// <summary>
        /// Gets the output of the gate
        /// </summary>
        protected virtual bool GetOutput()
        {
            switch (_state)
            {
                case LogicState.AND:
                    return _pins.GetInputPin(0) && _pins.GetInputPin(1);
                case LogicState.OR:
                    return _pins.GetInputPin(0) || _pins.GetInputPin(1);
            }

            return false;
        }
    }
}