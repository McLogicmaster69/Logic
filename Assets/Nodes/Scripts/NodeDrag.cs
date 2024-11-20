using UnityEngine;

namespace Logic.Nodes
{
    public class NodeDrag : MonoBehaviour
    {
        private Vector2 _difference = Vector2.zero;
        private bool _tempDrag = false;
        private float _buffer = 0f;

        private const float DRAG_BUFFER = 0.07f;

        private void Update()
        {
            DragUntilMouseUp();
            if (_buffer < DRAG_BUFFER)
                _buffer += Time.deltaTime;
        }

        private void OnMouseDown()
        {
            _buffer = 0f;
            _difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        }

        private void OnMouseDrag()
        {
            if(_buffer >= DRAG_BUFFER)
                transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - _difference;
        }

        /// <summary>
        /// Starts the temporary drag where the object follows the mouse
        /// </summary>
        public void StartTempDrag()
        {
            _tempDrag = true;
        }

        /// <summary>
        /// Drags the object to the mouse positions until the mouse is up
        /// </summary>
        private void DragUntilMouseUp()
        {
            // Checks if the object should be able to be dragged by the mouse
            if (!_tempDrag)
                return;

            // Checks if the mouse is still being held down
            if (!Input.GetMouseButton(0))
            {
                _tempDrag = false;
                return;
            }

            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}