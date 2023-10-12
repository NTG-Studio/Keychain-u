using System;
using Nova;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class UINode
    {
        public UIBlock2D button;
        public TextBlock text;

        public void SetColors(Color buttonColor, Color textColor)
        {
            button.Color = buttonColor;
            text.Color = textColor;
        }

        public void EnableAndSetText(string txt)
        {
            button.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
            text.Text = txt;
        }
    
        public void Enable()
        {
            button.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
        }
    
        public void Disable()
        {
            button.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        }
    }
}
