using UnityEngine;

namespace Logic.UI
{
    public class UINavigator : MonoBehaviour
    {
        [SerializeField] private GameObject _menuObject;
        [SerializeField] private GameObject _expandObject;

        private void Start()
        {
            _menuObject.SetActive(true);
            _expandObject.SetActive(false);
        }

        public void ExpandMenu()
        {
            _menuObject.SetActive(true);
            _expandObject.SetActive(false);
        }

        public void CollapseMenu()
        {
            _menuObject.SetActive(false);
            _expandObject.SetActive(true);
        }
    }
}