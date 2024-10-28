using Logic.Nodes;
using UnityEngine;

namespace Logic.Cables
{
    public class CableBuilder : MonoBehaviour
    {
        public static CableBuilder Main { get; private set; }

        [SerializeField] private GameObject _cableObject;
        [SerializeField] private string _nodeIOTag;

        private GameObject _startingNode;
        private bool _isInput;
        private bool _unselectNextFrame = false;

        private void Awake()
        {
            Main = this;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                CheckMouseOverObject();
            }
        }

        private void CheckMouseOverObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit)
            {
                if (hit.collider.gameObject.CompareTag(_nodeIOTag))
                    EndBuilder(hit.collider.gameObject);
            }
        }

        /// <summary>
        /// Starts the builder with a starting node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="isInput"></param>
        public void StartBuilder(GameObject node, bool isInput)
        {
            _startingNode = node;
            _isInput = isInput;
        }

        /// <summary>
        /// Ends the builder with an ending node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="isInput"></param>
        private void EndBuilder(GameObject node)
        {
            if (_startingNode == null)
                return;
            if (node.GetComponent<NodeIO>().IsInput == _isInput)
                return;

            GameObject cable = Instantiate(_cableObject);
            CableRenderer renderer = cable.GetComponent<CableRenderer>();
            renderer.SetNodes(_startingNode, node);

            _startingNode = null;
        }
    }
}