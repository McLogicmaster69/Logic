using Logic.Files;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.Menu
{
    public class EscapeMenuNavigator : MonoBehaviour
    {
        [SerializeField] private GameObject _menuObject;
        [SerializeField] private Button _saveButton;
        [SerializeField] private TMP_Text _statusText;

        private bool _visible = false;

        private void Start()
        {
            _menuObject.SetActive(_visible);
            SaveManager.CheckSaveDirectoryExists();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ToggleMenu(!_visible);
        }

        private void ToggleMenu(bool toggle)
        {
            _visible = toggle;
            _menuObject.SetActive(true);
            _saveButton.interactable = SaveManager.CurrentFilePath != "";
        }

        public void ShowMenu() => ToggleMenu(true);

        public void HideMenu() => ToggleMenu(false);

        public void SaveButton()
        {
            SaveManager.SaveToFilePath();
            HideMenu();
        }

        public void SaveAsButton()
        {

        }

        public void LoadButton()
        {

        }

        public void QuitButton()
        {

        }

        private void UpdateStatus(TextUpdateArgs updateArgs)
        {
            _statusText.text = updateArgs.Text;
            _statusText.color = updateArgs.Color;
        }
    }
}