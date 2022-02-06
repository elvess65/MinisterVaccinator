using CoreFramework;
using CoreFramework.UI.View;
using CoreFramework.UI.Widget;
using MinisterVaccinator.Extensions;
using MinisterVaccinator.Models;
using MinisterVaccinator.Widgets;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MinisterVaccinator.Views
{
    public class UIView_Menu : UIView_Abstract
    {
        [SerializeField] private UIWidget_Text m_UIWidget_Title = null;
        [SerializeField] private UIWidget_Text m_UIWidget_Description = null;
        [SerializeField] private UIWidget_Button m_UIWidget_PlayButton = null;

        [Header("Animation Sequence")]
        [SerializeField] private Animator[] m_IdleAnimators = null;
        [SerializeField] private Animator m_HandAnimatorController = null;
        [SerializeField] private AnimationEventListener m_HandAnimationEventHandler = null;
        [SerializeField] private Image Image_FillButtonPlay = null;

        private UIModel m_UIModel;
        private GameModel m_GameModel;

        private Animator m_UIWidgetTitleAnimator;

        public override void Initialize()
        {
            m_UIModel = Dispatcher.GetModel<UIModel>();
            m_GameModel = Dispatcher.GetModel<GameModel>();

            m_UIModel.UIViewMenu = this;

            m_UIWidget_PlayButton.Initialize();
            m_UIWidget_Title.Initialize("Minister Vaccinator");
            m_UIWidget_Description.Initialize("You have been elected as health care minister. Save You country.");

            RegisterWidget(m_UIWidget_Title);
            RegisterWidget(m_UIWidget_PlayButton);
            RegisterWidget(m_UIWidget_Description);

            m_UIWidget_PlayButton.OnWidgetPress += UIWidgetPlayButtonPressHandler;
            m_HandAnimationEventHandler.OnAnimationEvent += HandAnimationEventHandler;

            m_UIWidgetTitleAnimator = m_UIWidget_Title.GetComponent<Animator>();
            m_UIWidgetTitleAnimator.enabled = false;
            m_UIWidget_Title.OnShowSequenceFinished += UIWidgetTitleShowSequenceFinishedHandler;
        }

        private void UIWidgetTitleShowSequenceFinishedHandler()
        {
            m_UIWidgetTitleAnimator.enabled = true;
        }

        private void UIWidgetPlayButtonPressHandler()
        {
            m_UIWidget_PlayButton.LockInput(true);

            foreach (Animator animator in m_IdleAnimators)
                animator.SetTrigger("ToNone");

            //Next event is triggering by HandAnimationEventHandler
            m_HandAnimatorController.SetTrigger("Play");
        }

        private void HandAnimationEventHandler()
        {
            m_UIWidgetTitleAnimator.enabled = false;
            Image_FillButtonPlay.DOFillAmount(1, 0.5f).OnComplete(FillAnimationFinishedHandler);
        }

        private void FillAnimationFinishedHandler()
        {
            Image_FillButtonPlay.GetComponent<RectTransform>().SetParent(m_UIWidget_PlayButton.Root);
            m_GameModel.OnStartGamePressed?.Invoke();

            m_UIWidget_Title.OnHideSequenceFinished += TitleWidgetHideSequenceFinished;
        }

        private void TitleWidgetHideSequenceFinished()
        {
            m_UIWidget_Title.OnHideSequenceFinished -= TitleWidgetHideSequenceFinished;

            m_GameModel.OnStartGame?.Invoke();
        }
    }
}
