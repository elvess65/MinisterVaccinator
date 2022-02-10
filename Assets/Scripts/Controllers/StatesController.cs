using CoreFramework;
using MinisterVaccinator.Models;
using MinisterVaccinator.StateMachine;

namespace MinisterVaccinator.Controllers
{
    public class StatesController : BaseController
    {
        private GameStateMachine<GameState_Abstract> m_StateMachine;

        private GameModel m_GameModel;

        public StatesController(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public override void InitializeComplete()
        {
            m_GameModel = Dispatcher.GetModel<GameModel>();

            m_GameModel.OnMainMenuPending += MainMenuPendingHandler;
            m_GameModel.OnMainMenuReady += MainMenuReadyHandler;
            m_GameModel.OnStartGamePressed += StartGamePressedHandler;
            m_GameModel.OnStopGame += StopGameHandler;
            m_GameModel.OnVictory += VictoryHandler;
            m_GameModel.OnStartGame += StartGameHandler;
            m_GameModel.OnGameOver += GameOverHandler;
            m_GameModel.OnTransitionFromGameFinishToNewGame += TransitionFromGameFinishToNewGameHandler;
            m_GameModel.OnTransitionFromGameFinishToMainMenu += TransitionFromGameFinishToMainMenuHandler;
        }

        #region GameModelHandler

        private void MainMenuPendingHandler() => InitializeStateMachine();

        private void MainMenuReadyHandler() => m_StateMachine.ChangeState<GameState_MainMenu>();

        private void StartGamePressedHandler() => m_StateMachine.ChangeState<GameState_NoUI>();

        private void StartGameHandler() => m_StateMachine.ChangeState<GameState_Normal>();

        private void StopGameHandler(bool result)
        {
            if (result)
            {
                m_StateMachine.ChangeState<GameState_TransitionFromNormalToVictory>();
            }
            else
            {
                m_StateMachine.ChangeState<GameState_TransitionFromNormalToGameOver>();
            }
        }

        private void VictoryHandler() => m_StateMachine.ChangeState<GameState_Victory>();

        private void GameOverHandler() => m_StateMachine.ChangeState<GameState_GameOver>();

        private void TransitionFromGameFinishToNewGameHandler() => m_StateMachine.ChangeState<GameState_TransitionFromGameFinishToNewGame>();

        private void TransitionFromGameFinishToMainMenuHandler() => m_StateMachine.ChangeState<GameState_TransitionFromGameFinishToMainMenu>();


        #endregion

        private void InitializeStateMachine()
        {
            m_StateMachine = new GameStateMachine<GameState_Abstract>();

            m_StateMachine.AddState(new GameState_MainMenu());
            m_StateMachine.AddState(new GameState_NoUI());
            m_StateMachine.AddState(new GameState_Normal());
            m_StateMachine.AddState(new GameState_Victory());
            m_StateMachine.AddState(new GameState_GameOver());
            m_StateMachine.AddState(new GameState_MainMenuPending());
            m_StateMachine.AddState(new GameState_TransitionFromNormalToVictory());
            m_StateMachine.AddState(new GameState_TransitionFromNormalToGameOver());
            m_StateMachine.AddState(new GameState_TransitionFromGameFinishToNewGame()); 
            m_StateMachine.AddState(new GameState_TransitionFromGameFinishToMainMenu());

            m_StateMachine.Initialize<GameState_MainMenuPending>();
        }
    }
}
