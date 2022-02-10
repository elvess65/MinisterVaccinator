using CoreFramework.UI.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace MinisterVaccinator.Widgets
{
    public class UIWidget_Image : UIWidget
    {
        [SerializeField] private Image m_Image = null;

        public void Initialize(Sprite sprite = null)
        {
            InternalInitialize();

            if (sprite != null)
                m_Image.sprite = sprite;
        }

        public void SetDisplay(bool isDisplayed) => m_Image.enabled = isDisplayed;
    }
}
