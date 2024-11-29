using UnityEngine;

namespace Logic.Nodes
{
    public class Switch : LogicComponent
    {
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;
        [SerializeField] private Sprite _onHighlight;
        [SerializeField] private Sprite _offHighlight;
        [SerializeField] private SpriteRenderer _highlightRenderer;

        private SpriteRenderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        protected override void Clicked()
        {
            Output[0] = !Output[0];
            _renderer.sprite = Output[0] ? _onSprite : _offSprite;
            _highlightRenderer.sprite = Output[0] ? _onHighlight : _offHighlight;
            base.Clicked();
        }
    }
}