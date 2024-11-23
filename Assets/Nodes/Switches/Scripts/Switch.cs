using Logic.Nodes;
using UnityEngine;
using UnityEngine.EventSystems;

public class Switch : LogicComponent
{
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    protected override void Clicked()
    {
        Output[0] = !Output[0];
        _renderer.sprite = Output[0] ? _onSprite : _offSprite;
        base.Clicked();
    }
}
