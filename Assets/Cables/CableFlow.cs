using Logic.Nodes;
using UnityEngine;

namespace Logic.Cables
{
    public class CableFlow : MonoBehaviour
    {
        [SerializeField] private LogicGate Input;
        public bool Output => Input.Output;
    }
}