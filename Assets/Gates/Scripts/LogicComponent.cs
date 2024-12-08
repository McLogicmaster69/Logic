using UnityEngine;

namespace Logic.Nodes
{
    [RequireComponent(typeof(PinManager))]
    public class LogicComponent : Clickable
    {
        [SerializeField] private int _outputs;

        public bool[] Output { get; protected set; }

        public PinManager Pins { get; protected set; }

        private void Awake()
        {
            Output = new bool[_outputs];
            Pins = GetComponent<PinManager>();
        }

        /// <summary>
        /// Deletes the gate
        /// </summary>
        public void DeleteGate()
        {
            Pins.DeleteGate();
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