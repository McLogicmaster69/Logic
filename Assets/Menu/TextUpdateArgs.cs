using UnityEngine;

namespace Logic.Menu
{
    public class TextUpdateArgs
    {
        public string Text { get; set; }
        public Color Color { get; set; }

        public TextUpdateArgs(string text)
        {
            Text = text;
        }
    }
}