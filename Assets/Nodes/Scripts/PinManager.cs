using Logic.Cables;
using UnityEngine;

namespace Logic.Nodes
{
    public class PinManager : MonoBehaviour
    {
        [SerializeField] private Pin[] _inputPins;

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
    }
}