using UnityEngine;

namespace Logic.Cables
{
    public class CableSegment : Clickable
    {
        [SerializeField] private CableFlow _cable;

        protected override void ClickedNoDrag()
        {
            _cable.SelectCable();
        }

        protected override void Tick()
        {
            if (Input.GetMouseButtonDown(0))
                Deselect();
        }
    }
}