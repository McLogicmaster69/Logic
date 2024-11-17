using Logic.Nodes;
using System;
using UnityEngine;

namespace Logic.Cables
{
    [RequireComponent(typeof(CableRenderer))]
    public class CableFlow : MonoBehaviour
    {
        /// <summary>
        /// The gate that is outputting to the cable
        /// </summary>
        [SerializeField] private Pin _input;

        /// <summary>
        /// The state of the cable
        /// </summary>
        public bool Output => _input.Output;

        /// <summary>
        /// The pin of the gate that is outputting to the cable
        /// </summary>
        private int _inputPin;

        /// <summary>
        /// The pin of the gate that the cable is being fed into
        /// </summary>
        private int _outputPin;

        /// <summary>
        /// The renderer of the cable
        /// </summary>
        private CableRenderer _renderer;

        /// <summary>
        /// Called when the cable is deleted
        /// </summary>
        public event Action OnCableDeleted;

        private void Start()
        {
            _renderer = GetComponent<CableRenderer>();
        }

        private void Update()
        {
            if(_input != null)
                _renderer.SetCableActive(Output);
        }

        public void SetInputPin(Pin pin)
        {
            _input = pin;
        }
    }
}