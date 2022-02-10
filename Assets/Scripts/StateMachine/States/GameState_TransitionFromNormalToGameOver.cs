﻿using MinisterVaccinator.Models;

namespace MinisterVaccinator.StateMachine
{
    public class GameState_TransitionFromNormalToGameOver : GameState_Abstract
    {
        private GameModel m_GameModel;

        public GameState_TransitionFromNormalToGameOver() : base()
        {
            m_GameModel = Dispatcher.GetModel<GameModel>();
        }

        public override void EnterState()
        {
            base.EnterState();

            m_UIModel.UIViewGame.OnAllWidgetHideSequenceFinished += UIViewFinishGameAllWidgetHideSequenceFinishedHandler;
        }

        private void UIViewFinishGameAllWidgetHideSequenceFinishedHandler()
        {
            m_UIModel.UIViewGame.OnAllWidgetHideSequenceFinished -= UIViewFinishGameAllWidgetHideSequenceFinishedHandler;
            m_GameModel.OnGameOver();
        }
    }
}
