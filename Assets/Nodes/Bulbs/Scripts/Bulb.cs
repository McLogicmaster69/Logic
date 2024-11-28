using UnityEngine;

namespace Logic.Nodes
{
    public class Bulb : LogicComponent
    {
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        private SpriteRenderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        protected override void Tick()
        {
            base.Tick();
            _renderer.sprite = _pins.GetInputPin(0) ? _onSprite : _offSprite;
        }
    }
}