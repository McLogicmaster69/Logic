using Logic.Cables;
using UnityEngine;

namespace Logic.Nodes
{
    public class PinManager : MonoBehaviour
    {
        [SerializeField] private Pin[] _inputPins;
        [SerializeField] private Pin[] _outputPins;

        /// <summary>
        /// Gets the output of a pin at an index
        /// <para>If there is no pin at that index, output will be false</para>
        /// </summary>
        public bool GetInputPin(int index)
        {
            if (index >= _inputPins.Length)
                return false;
            return _inputPins[index].GetOutput();
        }

        /// <summary>
        /// Gets an array of pin outputs
        /// </summary>
        /// <param name="pins"></param>
        /// <returns></returns>
        public bool[] GetOutputPins(int pins)
        {
            bool[] output = new bool[pins];
            for (int i = 0; i < pins; i++)
            {
                output[i] = GetInputPin(i);
            }
            return output;
        }

        /// <summary>
        /// Deletes the logic gate
        /// </summary>
        public void DeleteGate()
        {
            foreach(Pin pin in _inputPins)
            {
                pin.DeleteGate();
            }

            foreach(Pin pin in _outputPins)
            {
                pin.DeleteGate();
            }
        }
    }
}