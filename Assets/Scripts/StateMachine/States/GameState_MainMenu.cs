namespace MinisterVaccinator.StateMachine
{
    public class GameState_MainMenu : GameState_Abstract
    {
        public GameState_MainMenu() : base()
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            m_UIModel.UIViewMenu.SetWidgetsActive(true, true);
        }

        public override void ExitState()
        {
            base.ExitState();

            m_UIModel.UIViewMenu.SetWidgetsActive(false, true);
        }
    }
}
