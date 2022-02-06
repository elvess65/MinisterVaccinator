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

            m_UIModel.UIViewGame.SetWidgetsActive(true, true, m_UIModel.UIViewGame.ExposedWidgetTask, 
                                                              m_UIModel.UIViewGame.ExposedWidget_Bar_Virus, 
                                                              m_UIModel.UIViewGame.ExposedWidget_Bar_Vaccine);
        }

        public override void ExitState()
        {
            base.ExitState();

            m_UIModel.UIViewGame.SetWidgetsActive(false, true);
        }
    }
}
