using UnityEngine;

namespace Logic.Nodes
{
    public class ZOSwitch : LogicComponent
    {
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        private SpriteRenderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        protected override void ClickedNoDrag()
        {
            Output[0] = !Output[0];
            _renderer.sprite = Output[0] ? _onSprite : _offSprite;
            base.ClickedNoDrag();
        }
    }
}