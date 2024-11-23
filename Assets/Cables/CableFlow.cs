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

        private bool _selected;

        /// <summary>
        /// Called when the cable is deleted
        /// </summary>
        public event Action<CableFlow> OnCableDeleted;

        private void Start()
        {
            _renderer = GetComponent<CableRenderer>();
        }

        private void Update()
        {
            if(_input != null)
                _renderer.SetCableActive(Output);

            if (Input.GetMouseButtonDown(0))
                _selected = false;


            if (Input.GetKeyDown(KeyCode.Delete) && _selected)
                DeleteCable();
        }

        /// <summary>
        /// Assigns the cable to an input pin
        /// </summary>
        /// <param name="pin"></param>
        public void SetInputPin(Pin pin)
        {
            _input = pin;
        }

        /// <summary>
        /// Deletes the cable
        /// </summary>
        public void DeleteCable()
        {
            OnCableDeleted?.Invoke(this);
            Destroy(gameObject);
        }

        /// <summary>
        /// Marks the cable as selected
        /// </summary>
        public void SelectCable()
        {
            _selected = true;
        }
    }
}