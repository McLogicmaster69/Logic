using Logic.Cables;
using UnityEngine;

namespace Logic.Nodes
{
    public class NodeIO : MonoBehaviour
    {
        [SerializeField] private bool _isInput;

        public bool IsInput => _isInput;

        private void OnMouseDown()
        {
            CableBuilder.Main.StartBuilder(gameObject, _isInput);
        }
    }
}