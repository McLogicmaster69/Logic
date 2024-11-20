using Logic.Nodes;
using UnityEngine;
using UnityEngine.EventSystems;

public class Switch : LogicComponent
{
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    private Vector3 _startPosition;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        _startPosition = transform.position;
    }

    private void OnMouseUpAsButton()
    {
        if (_startPosition == transform.position)
        {
            ToggleSwitch();
        }
    }

    private void ToggleSwitch()
    {
        Output = !Output;
        _renderer.sprite = Output ? _onSprite : _offSprite;
    }
}
