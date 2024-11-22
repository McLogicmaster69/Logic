using UnityEngine;

namespace Logic.Nodes
{
    [RequireComponent(typeof(PinManager))]
    public class LogicComponent : Clickable
    {
        [SerializeField] private int _outputs;

        public bool[] Output { get; protected set; }

        protected PinManager _pins;

        private void Awake()
        {
            Output = new bool[_outputs];
            _pins = GetComponent<PinManager>();
        }

        /// <summary>
        /// Deletes the gate
        /// </summary>
        public void DeleteGate()
        {
            _pins.DeleteGate();
            Destroy(gameObject);
        }

        protected override void Tick()
        {
            base.Tick();
            if (Input.GetKeyDown(KeyCode.Delete) && Selected)
                DeleteGate();
        }
    }
}