namespace MinisterVaccinator.StateMachine
{
    public class GameState_GameOver : GameState_Abstract
    {
        public GameState_GameOver() : base()
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            m_UIModel.UIViewGame.SetWidgetsActive(false, true);
            m_UIModel.UIViewFinishGame.SetWidgetsActive(true, true);
        }

        public override void ExitState()
        {
            base.ExitState();


            m_UIModel.UIViewFinishGame.SetWidgetsActive(false, true);

        }
    }
}
