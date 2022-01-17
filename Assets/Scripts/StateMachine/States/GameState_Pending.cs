namespace MinisterVaccinator.StateMachine
{
    public class GameState_Pending : GameState_Abstract
    {
        public GameState_Pending() : base()
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            m_UIModel.UIViewGame.SetWidgetsActive(false, false);
            m_UIModel.UIViewMenu.SetWidgetsActive(false, false);
        }
    }
}
