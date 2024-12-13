using Logic.Menu;
using UnityEngine;

namespace Logic
{
    public class Clickable : MonoBehaviour
    {
        [SerializeField] protected GameObject _highlightSprite;

        protected bool Selected { get; private set; }

        private Vector3 _startPosition;

        private void Update()
        {
            if(!EscapeMenuNavigator.Main.Paused)
                Tick();
        }

        private void OnMouseUpAsButton()
        {
            if (EscapeMenuNavigator.Main.Paused)
                return;

            if (_startPosition == transform.position)
                ClickedNoDrag();
            else
                ClickedAndDrag();
        }

        private void OnMouseDown()
        {
            _startPosition = transform.position;
        }

        /// <summary>
        /// Called when the component has been clicked
        /// </summary>
        protected virtual void ClickedAndDrag()
        {
            Selected = true;
        }

        protected virtual void ClickedNoDrag()
        {
            Selected = true;
        }

        /// <summary>
        /// Runs every frame
        /// </summary>
        protected virtual void Tick()
        {
            if (Input.GetMouseButtonDown(0))
                Deselect();
            _highlightSprite.SetActive(Selected);
        }

        /// <summary>
        /// Sets the Selected value to false
        /// </summary>
        protected void Deselect() => Selected = false;
    }
}