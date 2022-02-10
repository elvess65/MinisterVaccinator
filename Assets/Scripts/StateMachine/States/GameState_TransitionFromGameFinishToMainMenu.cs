using MinisterVaccinator.Models;

namespace MinisterVaccinator.StateMachine
{
    public class GameState_TransitionFromGameFinishToMainMenu : GameState_Abstract
    {
        private GameModel m_GameModel;

        public GameState_TransitionFromGameFinishToMainMenu() : base()
        {
            m_GameModel = Dispatcher.GetModel<GameModel>();
        }

        public override void EnterState()
        {
            base.EnterState();

            m_UIModel.UIViewFinishGame.OnAllWidgetHideSequenceFinished += UIViewFinishGameAllWidgetHideSequenceFinishedHandler;
        }

        private void UIViewFinishGameAllWidgetHideSequenceFinishedHandler()
        {
            m_UIModel.UIViewFinishGame.OnAllWidgetHideSequenceFinished -= UIViewFinishGameAllWidgetHideSequenceFinishedHandler;
            m_GameModel.OnMainMenuReady();
        }
    }
}
