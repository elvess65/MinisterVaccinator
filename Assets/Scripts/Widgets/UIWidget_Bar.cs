using CoreFramework.UI.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace MinisterVaccinator.Widgets
{
    public class UIWidget_Bar : UIWidget
    {
        [SerializeField] private Image m_Image_FG = null;
        [SerializeField] private Text m_Text_Amount = null;

        public void Initialize()
        {
            InternalInitialize();
        }

        public void UpdateProgress(int curAmount, int totalAmount)
        {
            m_Text_Amount.text = $"{curAmount}/{totalAmount}";

            float progress = (float)curAmount / totalAmount;
            m_Image_FG.fillAmount = progress;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                m_ShowSequence.PlaySequence();
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                m_HideSequence.PlaySequence();
            }
        }
    }
}
