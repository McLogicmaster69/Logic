using UnityEngine;

namespace Logic.Cables
{
    public class CableSegment : Clickable
    {
        [SerializeField] private CableFlow _cable;

        protected override void Clicked()
        {
            _cable.SelectCable();
        }
    }
}