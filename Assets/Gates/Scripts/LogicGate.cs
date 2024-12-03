using Logic.Cables;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Nodes
{
    public enum LogicState
    {
        UNKNOWN = -1,
        AND = 0,
        OR = 1,
        NOT = 2,
        XOR = 3,
        NAND = 4,
        NOR = 5
    }

    public class LogicGate : LogicComponent
    {
        [SerializeField] private LogicState _state;

        public LogicState State => _state;

        protected override void Tick()
        {
            base.Tick();
            Output[0] = GetOutput();
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
                case LogicState.NOT:
                    return !_pins.GetInputPin(0);
                case LogicState.XOR:
                    return _pins.GetInputPin(0) != _pins.GetInputPin(1);
                case LogicState.NAND:
                    return !(_pins.GetInputPin(0) && _pins.GetInputPin(1));
                case LogicState.NOR:
                    return !(_pins.GetInputPin(0) || _pins.GetInputPin(1));
            }

            return false;
        }
    }
}