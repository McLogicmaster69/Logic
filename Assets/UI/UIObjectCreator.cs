using Logic.Nodes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic.UI
{
    public class UIObjectCreator : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject _gate;

        public void OnPointerDown(PointerEventData eventData)
        {
            GameObject o = Instantiate(_gate, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, ObjectStorage.Main.transform);
            o.GetComponent<NodeDrag>().StartTempDrag();
        }
    }
}