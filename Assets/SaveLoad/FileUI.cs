using TMPro;
using UnityEngine;

namespace Logic.Menu
{
    public class FileUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;

        private string _fileName;

        public void Initialise(string name)
        {
            _fileName = name;
            _nameText.text = name;
        }

        public void Load()
        {
            EscapeMenuNavigator.Main.LoadFile(_fileName);
        }
    }
}