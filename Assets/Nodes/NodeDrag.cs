using UnityEngine;

namespace Logic.Nodes
{
    public class NodeDrag : MonoBehaviour
    {
        private Vector2 _difference = Vector2.zero;

        private void OnMouseDown()
        {
            _difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        }

        private void OnMouseDrag()
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - _difference;
        }
    }
}