using Logic.Cables;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Nodes
{
    public enum IO
    {
        Input = 0,
        Output = 1
    }

    public class Pin : MonoBehaviour
    {
        [SerializeField] private IO _pinMode;
        [SerializeField] private LogicComponent _gate;
        [SerializeField] private int _outputID;

        public bool Output => _gate.Output[_outputID];
        public LogicComponent Gate => _gate;

        private List<CableFlow> _cables = new List<CableFlow>();

        /// <summary>
        /// Gets the output of the connected cable
        /// <para>If there is no connected cable, output will be false</para>
        /// </summary>
        public bool GetOutput()
        {
            if (_cables.Count == 0)
                return false;
            return _cables[0].Output;
        }

        /// <summary>
        /// Outputs if a cable can be connected to this pin
        /// </summary>
        /// <returns></returns>
        public bool CanConnectCable() => _cables.Count == 0 || _pinMode == IO.Output;

        /// <summary>
        /// Connects a cable to a pin
        /// </summary>
        public bool ConnectCable(CableFlow cable)
        {
            if (_cables.Count != 0 && _pinMode == IO.Input)
                return false;

            _cables.Add(cable);
            cable.OnCableDeleted += DisconnectCable;
            return true;
        }

        /// <summary>
        /// Disconnects a cable from a pin
        /// </summary>
        private void DisconnectCable(CableFlow cable)
        {
            cable.OnCableDeleted -= DisconnectCable;
            _cables.Remove(cable);
        }

        /// <summary>
        /// Deletes the gate
        /// </summary>
        public void DeleteGate()
        {
            while(_cables.Count > 0)
            {
                _cables[0].DeleteCable();
            }
        }
    }
}