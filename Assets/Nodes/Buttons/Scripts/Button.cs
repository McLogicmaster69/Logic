using UnityEngine;

namespace Logic.Nodes
{
    public class Button : LogicComponent
    {
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        private bool _mouseDown;
        private SpriteRenderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void OnMouseDown()
        {
            Output[0] = true;
            _renderer.sprite = _onSprite;
        }

        private void OnMouseUp()
        {
            Output[0] = false;
            _renderer.sprite = _offSprite;
        }
    }
}