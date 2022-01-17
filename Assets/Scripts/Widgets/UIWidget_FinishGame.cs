using CoreFramework.UI.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace MinisterVaccinator.Widgets
{
    public class UIWidget_FinishGame : UIWidget
    {
        [SerializeField] private UIWidget_Text m_UIWidget_Result = null;
        [SerializeField] private Image Image_Success = null;
        [SerializeField] private Image Image_GameOver = null;
        [SerializeField] private UIWidget_Button m_UIWidget_NextButton = null;
        [SerializeField] private UIWidget_Button m_UIWidget_ReplayButton = null;
        [SerializeField] private UIWidget_Button m_UIWidget_MenuButton = null;

        public void Initialize()
        {
            InternalInitialize();

            Image_Success.enabled = false;
            Image_GameOver.enabled = false;

            m_UIWidget_NextButton.Initialize();
            m_UIWidget_ReplayButton.Initialize();
            m_UIWidget_MenuButton.Initialize();

            m_UIWidget_NextButton.OnWidgetPress += UIWidgetNextButtonPressHandler;
            m_UIWidget_ReplayButton.OnWidgetPress += UIWidgetReplayButtonPressHandler;
            m_UIWidget_MenuButton.OnWidgetPress += UIWidgetMenuButtonPressHandler;
        }

        #region PressHandlers

        private void UIWidgetNextButtonPressHandler()
        {
            Debug.Log("Next");
        }

        private void UIWidgetReplayButtonPressHandler()
        {
            Debug.Log("Replay");
        }

        private void UIWidgetMenuButtonPressHandler()
        {
            Debug.Log("Menu");
        }

        #endregion

    }
}
