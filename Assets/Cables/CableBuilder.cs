using Logic.Nodes;
using UnityEngine;

namespace Logic.Cables
{
    public class CableBuilder : MonoBehaviour
    {
        public static CableBuilder Main { get; private set; }

        [SerializeField] private GameObject _cableObject;
        [SerializeField] private GameObject _mouseNode;
        [SerializeField] private string _nodeIOTag;

        private GameObject _startingNode;
        private GameObject _tempCable;
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
                if(_tempCable != null)
                {
                    Destroy(_tempCable);
                    _tempCable = null;
                }

                CheckMouseOverObject();
                _startingNode = null;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _mouseNode.transform.position = new Vector3(mouse.x, mouse.y, 0f);
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

            _tempCable = Instantiate(_cableObject);
            CableRenderer renderer = _tempCable.GetComponent<CableRenderer>();
            if(isInput)
                renderer.SetNodes(_mouseNode, node);
            else
                renderer.SetNodes(node, _mouseNode);
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
            if(_isInput)
                renderer.SetNodes(node, _startingNode);
            else
                renderer.SetNodes(_startingNode, node);
        }
    }
}