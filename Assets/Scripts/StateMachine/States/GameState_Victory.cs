﻿namespace MinisterVaccinator.StateMachine
{
    public class GameState_Victory : GameState_Abstract
    {
        public GameState_Victory() : base()
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            m_UIModel.UIViewGame.SetWidgetsActive(false, true);
            m_UIModel.UIViewFinishGame.SetWidgetsActive(true, true, m_UIModel.UIViewFinishGame.ExposedUIWidget_GameOver);
        }

        public override void ExitState()
        {
            base.ExitState();

            m_UIModel.UIViewFinishGame.SetWidgetsActive(false, true);
        }
    }
}