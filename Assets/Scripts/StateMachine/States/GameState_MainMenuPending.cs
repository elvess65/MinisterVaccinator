namespace MinisterVaccinator.StateMachine
{
    public class GameState_MainMenuPending : GameState_Pending
    {
        public GameState_MainMenuPending() : base()
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            m_UIModel.UIViewGame.SetWidgetsActive(false, false);
            m_UIModel.UIViewMenu.SetWidgetsActive(false, false);
            m_UIModel.UIViewFinishGame.SetWidgetsActive(false, false);
        }
    }
}
