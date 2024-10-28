using UnityEngine;

namespace Logic.Cables
{
    public class CableRenderer : MonoBehaviour
    {
        [SerializeField] private GameObject _nodeA;
        [SerializeField] private GameObject _nodeB;
        [SerializeField] private GameObject[] _cables;

        public const float CABLE_GAP = 0.5f;
        public const float CABLE_WIDTH = 0.1f;

        private void Update()
        {
            Vector2 positionA = _nodeA.transform.position; 
            Vector2 positionB = _nodeB.transform.position;

            if (positionA.x + CABLE_GAP * 2 < positionB.x)
                Render3Cable(positionA, positionB);
            else
                Render5Cable(positionA, positionB);
        }

        /// <summary>
        /// Renders the cable with 3 cables
        /// </summary>
        /// <param name="positionA"></param>
        /// <param name="positionB"></param>
        private void Render3Cable(Vector2 positionA, Vector2 positionB)
        {
            float xLength = positionB.x - positionA.x;
            float yLength = positionB.y - positionA.y;

            _cables[0].transform.position = new Vector3(positionA.x + xLength / 4, positionA.y);
            _cables[0].transform.localScale = new Vector3(xLength / 2, CABLE_WIDTH, 1f);

            _cables[1].transform.position = new Vector3(positionA.x + xLength / 2, positionA.y + yLength / 2);
            _cables[1].transform.localScale = new Vector3(CABLE_WIDTH, Mathf.Abs(yLength) + CABLE_WIDTH, 1f);

            _cables[2].transform.position = new Vector3(positionB.x - xLength / 4, positionB.y);
            _cables[2].transform.localScale = new Vector3(xLength / 2, CABLE_WIDTH, 1f);

            _cables[3].SetActive(false);
            _cables[4].SetActive(false);
        }

        /// <summary>
        /// Renders the cable with 5 cables
        /// </summary>
        /// <param name="positionA"></param>
        /// <param name="positionB"></param>
        private void Render5Cable(Vector2 positionA, Vector2 positionB)
        {
            float xLength = CABLE_GAP * 2 + positionA.x - positionB.x;
            float yLength = positionB.y - positionA.y;

            _cables[0].transform.position = new Vector3(positionA.x + CABLE_GAP / 2, positionA.y);
            _cables[0].transform.localScale = new Vector3(CABLE_GAP, CABLE_WIDTH, 1f);

            _cables[1].transform.position = new Vector3(positionA.x + CABLE_GAP, positionA.y + yLength / 4);
            _cables[1].transform.localScale = new Vector3(CABLE_WIDTH, Mathf.Abs(yLength) / 2 + CABLE_WIDTH, 1f);

            _cables[2].transform.position = new Vector3((positionA.x + positionB.x) / 2, (positionA.y + positionB.y) / 2);
            _cables[2].transform.localScale = new Vector3(xLength, CABLE_WIDTH, 1f);

            _cables[3].SetActive(true);
            _cables[3].transform.position = new Vector3(positionB.x - CABLE_GAP, positionB.y - yLength / 4);
            _cables[3].transform.localScale = new Vector3(CABLE_WIDTH, Mathf.Abs(yLength) / 2 + CABLE_WIDTH, 1f);

            _cables[4].SetActive(true);
            _cables[4].transform.position = new Vector3(positionB.x - CABLE_GAP / 2, positionB.y);
            _cables[4].transform.localScale = new Vector3(CABLE_GAP, CABLE_WIDTH, 1f);
        }

        /// <summary>
        /// Sets the nodes for the renderer to create a cable between
        /// </summary>
        /// <param name="nodeA"></param>
        /// <param name="nodeB"></param>
        public void SetNodes(GameObject nodeA, GameObject nodeB)
        {
            _nodeA = nodeA;
            _nodeB = nodeB;
        }
    }
}