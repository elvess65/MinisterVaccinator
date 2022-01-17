using CoreFramework.UI.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace MinisterVaccinator.Widgets
{
    public class UIWidget_Text : UIWidget
    {
        [SerializeField] private Text m_Text = null;

        public void Initialize(string text)
        {
            InternalInitialize();

            m_Text.text = text;
        }
    }
}
