using Logic.Files;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.Menu
{
    public class EscapeMenuNavigator : MonoBehaviour
    {
        public static EscapeMenuNavigator Main { get; private set; }

        [Header("UI")]
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _nextLoadPageButton;
        [SerializeField] private Button _previousLoadPageButton;
        [SerializeField] private TMP_Text _statusText;
        [SerializeField] private TMP_InputField _saveFilePathInput;

        [Header("Objects")]
        [SerializeField] private GameObject _menuObject;
        [SerializeField] private GameObject _backButton;
        [SerializeField] private GameObject _loadingMenu;
        [SerializeField] private GameObject _buttonObjects;
        [SerializeField] private GameObject _saveAsMenu;
        [SerializeField] private GameObject _loadMenu;
        [SerializeField] private GameObject _confirmQuitMenu;
        [SerializeField] private GameObject _filesParent;
        [SerializeField] private float _textTime = 3f;

        [Header("Prefabs")]
        [SerializeField] private GameObject _loadFileObject;

        private bool _visible = false;
        private int _menuState = 0;
        private int _loadPage = 0;
        private FileInfo[] _fileInfo;
        private Coroutine _hidingTextCoroutine;
        private List<GameObject> _loadFileObjects = new List<GameObject>();

        public const int LOAD_OBJECTS_PER_PAGE = 8;
        public const float LOAD_FILE_OBJECT_SPACE = 40f;

        #region Unity Methods

        private void Awake()
        {
            Main = this;
        }

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

        #endregion

        #region Status

        private void UpdateStatus(TextUpdateArgs updateArgs)
        {
            _statusText.text = updateArgs.Text;
            _statusText.color = updateArgs.Color;
            Debug.Log(_statusText.text);
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

        #endregion

        #region Main Menu

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
            UpdateMenuState(-1);
            SaveManager.SaveToFilePath(UpdateStatus);
            UpdateMenuState(0);
            HideMenu();
            StartHidingText();
        }

        public void ReturnMainMenu() => UpdateMenuState(0);

        public void SaveAsButton() => UpdateMenuState(1);

        public void LoadButton()
        {
            UpdateMenuState(2);
            InitLoadMenu();
        }

        public void QuitButton() => UpdateMenuState(3);

        private void UpdateMenuState(int state)
        {
            _menuState = state;
            _backButton.SetActive(state == 1 || state == 2);
            _loadingMenu.SetActive(state == -1);
            _buttonObjects.SetActive(state == 0);
            _saveAsMenu.SetActive(state == 1);
            _loadMenu.SetActive(state == 2);
            return;
            _confirmQuitMenu.SetActive(state == 3);
        }

        #endregion

        #region Save Menu

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

        #endregion

        #region Load Menu

        private void InitLoadMenu()
        {
            _fileInfo = SaveManager.GetAllFilesInDirectory();
            LoadPage();
        }

        private void DestroyAllLoadObjects()
        {
            while(_loadFileObjects.Count > 0)
            {
                Destroy(_loadFileObjects[0]);
                _loadFileObjects.RemoveAt(0);
            }
        }

        private void LoadPage()
        {
            DestroyAllLoadObjects();
            SetLoadButtonsActive();

            int index = _loadPage * LOAD_OBJECTS_PER_PAGE;
            float posY = 160f;

            while(index < (_loadPage + 1) * LOAD_OBJECTS_PER_PAGE && index < _fileInfo.Length)
            {
                GameObject o = Instantiate(_loadFileObject, _filesParent.transform);
                RectTransform rect = o.GetComponent<RectTransform>();
                rect.localPosition = new Vector3(rect.localPosition.x, posY, rect.localPosition.z);
                o.GetComponent<FileUI>().Initialise(_fileInfo[index].Name);
                _loadFileObjects.Add(o);
                index += 1;
                posY -= LOAD_FILE_OBJECT_SPACE;
            }
        }

        private void SetLoadButtonsActive()
        {
            _nextLoadPageButton.interactable = (_loadPage + 1) * LOAD_OBJECTS_PER_PAGE < _fileInfo.Length;
            _previousLoadPageButton.interactable = _loadPage > 0;
        }

        public void NextLoadPage()
        {
            _loadPage++;
            LoadPage();
        }

        public void PreviousLoadPage()
        {
            _loadPage--;
            LoadPage();
        }

        public void LoadFile(string fileName)
        {
            UpdateMenuState(-1);
            SaveManager.LoadFileFromPath(fileName, UpdateStatus);
            UpdateMenuState(0);
            HideMenu();
            StartHidingText();
        }

        #endregion
    }
}