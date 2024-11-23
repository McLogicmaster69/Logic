using UnityEngine;

namespace Logic
{
    public class Clickable : MonoBehaviour
    {
        protected bool Selected { get; private set; }

        private Vector3 _startPosition;

        private void Update()
        {
            Tick();
        }

        private void OnMouseUpAsButton()
        {
            if (_startPosition == transform.position)
            {
                Clicked();
            }
        }

        private void OnMouseDown()
        {
            _startPosition = transform.position;
        }

        /// <summary>
        /// Called when the component has been clicked
        /// </summary>
        protected virtual void Clicked()
        {
            Selected = true;
        }

        /// <summary>
        /// Runs every frame
        /// </summary>
        protected virtual void Tick()
        {
            if (Input.GetMouseButtonDown(0))
                Selected = false;
        }
    }
}