using CoreFramework.UI.View;
using CoreFramework.UI.Widget;
using MinisterVaccinator.Models;
using MinisterVaccinator.Widgets;
using UnityEngine;

namespace MinisterVaccinator.Views
{
    public class UIView_FinishGame : UIView_Abstract
    {
        [SerializeField] private UIWidget_Text m_UIWidget_Result = null;
        [SerializeField] private UIWidget_Image m_UIWidget_BG = null;
        [SerializeField] private UIWidget_Image m_UIWidget_Success = null;
        [SerializeField] private UIWidget_Image m_UIWidget_GameOver = null;
        [SerializeField] private UIWidget_Button m_UIWidget_NextButton = null;
        [SerializeField] private UIWidget_Button m_UIWidget_RestartButton = null;
        [SerializeField] private UIWidget_Button m_UIWidget_MenuButton = null;

        private UIModel m_UIModel;
        private GameModel m_GameModel;
        private GameplayModel m_GameplayModel;
        private GameDataModel m_GameDataModel;

        public UIWidget_Button ExposedUIWidget_NextButton => m_UIWidget_NextButton;

        public override void Initialize()
        {
            m_UIModel = Dispatcher.GetModel<UIModel>();
            m_GameModel = Dispatcher.GetModel<GameModel>();
            m_GameplayModel = Dispatcher.GetModel<GameplayModel>();
            m_GameDataModel = Dispatcher.GetModel<GameDataModel>();

            GameDataModel gameDataModel = Dispatcher.GetModel<GameDataModel>();
            var spriteAssets = gameDataModel.UISpriteAssets;

            m_UIModel.UIViewFinishGame = this;

            m_UIWidget_BG.Initialize();
            m_UIWidget_NextButton.Initialize();
            m_UIWidget_MenuButton.Initialize();
            m_UIWidget_RestartButton.Initialize();
            m_UIWidget_Success.Initialize(spriteAssets.Victory);
            m_UIWidget_GameOver.Initialize(spriteAssets.GameOver);

            RegisterWidget(m_UIWidget_NextButton);
            RegisterWidget(m_UIWidget_RestartButton);
            RegisterWidget(m_UIWidget_MenuButton);
            RegisterWidget(m_UIWidget_Result);
            RegisterWidget(m_UIWidget_Success);
            RegisterWidget(m_UIWidget_GameOver);
            RegisterWidget(m_UIWidget_BG);

            m_GameModel.OnStopGame += StopGameHandler;

            m_UIWidget_NextButton.OnWidgetPress += UIWidgetNextButtonPressHandler;
            m_UIWidget_RestartButton.OnWidgetPress += UIWidgetRestartButtonPressHandler;
            m_UIWidget_MenuButton.OnWidgetPress += UIWidgetMenuButtonPressHandler;
        }

        #region Handlers

        private void StopGameHandler(bool result)
        {
            m_UIWidget_Result.Initialize(result ? "Victory" : "Defeat");

            if (result)
            {
                m_UIWidget_GameOver.SetDisplay(false);
                m_UIWidget_NextButton.Root.gameObject.SetActive(true);
            }
            else
            {
                m_UIWidget_Success.SetDisplay(false);
                m_UIWidget_NextButton.Root.gameObject.SetActive(false);
            }
        }

        #region PressHandlers

        private void UIWidgetNextButtonPressHandler()
        {
            m_GameDataModel.CurrentLevel++;

            //TODO: Restart the level

            Debug.Log("UIView_FinishGame - NextButtonPress. TODO: Increment level. Restart the level");
            m_GameModel.OnTransitionFromGameFinishToNewGame?.Invoke();
        }

        private void UIWidgetRestartButtonPressHandler()
        {
            m_GameDataModel.CurrentLevel++;
            //TODO: Restart the level

            Debug.Log("UIView_FinishGame - RestartButtonPress. TODO: Restart the level");
            m_GameModel.OnTransitionFromGameFinishToNewGame?.Invoke();
        }

        private void UIWidgetMenuButtonPressHandler()
        {
            //TODO: Go to main menu

            Debug.Log("UIView_FinishGame - MenuButtonPress. TODO: Go to main menu");
            m_GameModel.OnTransitionFromGameFinishToMainMenu?.Invoke();
        }

        #endregion

        #endregion
    }
}
