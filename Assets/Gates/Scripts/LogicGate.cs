using Logic.Cables;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Nodes
{
    public class LogicGate : LogicComponent
    {
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
            switch (Type)
            {
                case ComponentType.AND:
                    return Pins.GetInputPin(0) && Pins.GetInputPin(1);
                case ComponentType.OR:
                    return Pins.GetInputPin(0) || Pins.GetInputPin(1);
                case ComponentType.NOT:
                    return !Pins.GetInputPin(0);
                case ComponentType.XOR:
                    return Pins.GetInputPin(0) != Pins.GetInputPin(1);
                case ComponentType.NAND:
                    return !(Pins.GetInputPin(0) && Pins.GetInputPin(1));
                case ComponentType.NOR:
                    return !(Pins.GetInputPin(0) || Pins.GetInputPin(1));
            }

            return false;
        }
    }
}