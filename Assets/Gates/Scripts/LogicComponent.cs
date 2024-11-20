using UnityEngine;

namespace Logic.Nodes
{
    public class LogicComponent : MonoBehaviour
    {
        [SerializeField] private int _outputs;

        public bool[] Output { get; protected set; }

        private void Awake()
        {
            Output = new bool[_outputs];
        }
    }
}