using Logic.Cables;
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

        public bool Output => _gate.Output;

        private CableFlow _cable;

        /// <summary>
        /// Gets the output of the connected cable
        /// <para>If there is no connected cable, output will be false</para>
        /// </summary>
        public bool GetOutput()
        {
            if (_cable == null)
                return false;
            return _cable.Output;
        }

        /// <summary>
        /// Outputs if a cable can be connected to this pin
        /// </summary>
        /// <returns></returns>
        public bool CanConnectCable() => _cable == null || _pinMode == IO.Output;

        /// <summary>
        /// Connects a cable to a pin
        /// </summary>
        public bool ConnectCable(CableFlow cable)
        {
            if (_cable != null && _pinMode == IO.Input)
                return false;

            _cable = cable;
            cable.OnCableDeleted += DisconnectCable;
            return true;
        }

        /// <summary>
        /// Disconnects a cable from a pin
        /// </summary>
        private void DisconnectCable()
        {
            _cable.OnCableDeleted -= DisconnectCable;
            _cable = null;
        }
    }
}