using CoreFramework.UI.View;
using MinisterVaccinator.Models;
using MinisterVaccinator.Widgets;
using UnityEngine;

namespace MinisterVaccinator.Views
{
    public class UIView_FinishGame : UIView_Abstract
    {
        [SerializeField] private UIWidget_FinishGame m_UIWidget_FinishGame = null;

        //public UIWidget_FinishGame ExposedWidget_FinishGame => m_UIWidget_FinishGame;

        private UIModel m_UIModel;

        public override void Initialize()
        {
            m_UIModel = Dispatcher.GetModel<UIModel>();

            m_UIModel.UIViewFinishGame = this;

            m_UIWidget_FinishGame.Initialize();

            RegisterWidget(m_UIWidget_FinishGame);
        }
    }
}
