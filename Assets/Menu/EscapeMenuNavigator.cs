using Logic.Files;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.Menu
{
    public class EscapeMenuNavigator : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Button _saveButton;
        [SerializeField] private TMP_Text _statusText;
        [SerializeField] private TMP_InputField _saveFilePathInput;
        [SerializeField] private TMP_InputField _loadFilePathInput;

        [Header("Objects")]
        [SerializeField] private GameObject _menuObject;
        [SerializeField] private GameObject _loadingMenu;
        [SerializeField] private GameObject _buttonObjects;
        [SerializeField] private GameObject _saveAsMenu;
        [SerializeField] private GameObject _loadMenu;
        [SerializeField] private GameObject _confirmQuitMenu;
        [SerializeField] private float _textTime = 3f;

        private bool _visible = false;
        private int _menuState = 0;
        private Coroutine _hidingTextCoroutine;

        private void Start()
        {
            _menuObject.SetActive(_visible);
            SaveManager.CheckSaveDirectoryExists();
            UpdateMenuState(0);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _menuState != -1)
                ToggleMenu(!_visible);
        }

        private void ToggleMenu(bool toggle)
        {
            _visible = toggle;
            _menuObject.SetActive(_visible);
            _saveButton.interactable = SaveManager.CurrentFilePath != "";
        }

        public void ShowMenu() => ToggleMenu(true);

        public void HideMenu() => ToggleMenu(false);

        public void SaveButton()
        {
            SaveManager.SaveToFilePath();
            HideMenu();
        }

        public void ReturnMainMenu() => UpdateMenuState(0);

        public void SaveAsButton() => UpdateMenuState(1);

        public void LoadButton() => UpdateMenuState(2);

        public void QuitButton() => UpdateMenuState(3);

        public void SaveFileButton()
        {
            if (string.IsNullOrEmpty(_saveFilePathInput.text))
            {
                UpdateStatus(new TextUpdateArgs("ERROR: Input file name") { Color = Color.red });
                StartHidingText();
                return;
            }

            UpdateMenuState(-1);
            SaveManager.SaveToFilePath(_saveFilePathInput.text, UpdateStatus);
            UpdateMenuState(0);
            HideMenu();
            StartHidingText();
        }

        private void UpdateStatus(TextUpdateArgs updateArgs)
        {
            _statusText.text = updateArgs.Text;
            _statusText.color = updateArgs.Color;
            Debug.Log(_statusText.text);
        }

        private void UpdateMenuState(int state)
        {
            _menuState = state;
            _loadingMenu.SetActive(state == -1);
            _buttonObjects.SetActive(state == 0);
            _saveAsMenu.SetActive(state == 1);
            return;
            _loadMenu.SetActive(state == 2);
            _confirmQuitMenu.SetActive(state == 3);
        }

        private void StartHidingText()
        {
            if (_hidingTextCoroutine != null)
                StopCoroutine(_hidingTextCoroutine);
            _hidingTextCoroutine = StartCoroutine(HideText());
        }

        private IEnumerator HideText()
        {
            yield return new WaitForSeconds(_textTime);
            _statusText.text = "";
            _hidingTextCoroutine = null;
        }
    }
}