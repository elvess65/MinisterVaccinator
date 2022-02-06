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
        }

        #region GameModelHandler

        private void MainMenuPendingHandler() => InitializeStateMachine();

        private void MainMenuReadyHandler() => m_StateMachine.ChangeState<GameState_MainMenu>();

        private void StartGamePressedHandler() => m_StateMachine.ChangeState<GameState_Normal>();

        private void StopGameHandler(bool result)
        {
            if (result)
            {
                m_StateMachine.ChangeState<GameState_Victory>();
            }
            else
            {
                m_StateMachine.ChangeState<GameState_GameOver>();
            }
        }

        #endregion

        private void InitializeStateMachine()
        {
            m_StateMachine = new GameStateMachine<GameState_Abstract>();

            m_StateMachine.AddState(new GameState_MainMenu());
            m_StateMachine.AddState(new GameState_Normal());
            m_StateMachine.AddState(new GameState_Pending());
            m_StateMachine.AddState(new GameState_Victory());
            m_StateMachine.AddState(new GameState_GameOver());

            m_StateMachine.Initialize<GameState_Pending>();
        }
    }
}
