using UnityEngine;

namespace Logic.Nodes
{
    public class Constant : LogicComponent
    {
        [SerializeField] private bool _state;

        private void Start()
        {
            Output[0] = _state;
        }
    }
}