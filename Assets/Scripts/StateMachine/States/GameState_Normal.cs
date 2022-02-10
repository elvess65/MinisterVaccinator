namespace MinisterVaccinator.StateMachine
{
    public class GameState_Normal : GameState_Abstract
    {
        public GameState_Normal() : base()
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            m_UIModel.UIViewGame.SetWidgetsActive(true, true, m_UIModel.UIViewGame.ExposedWidgetInputResult);
        }

        public override void ExitState()
        {
            base.ExitState();

            m_UIModel.UIViewGame.SetWidgetsActive(false, true);
        }
    }
}
