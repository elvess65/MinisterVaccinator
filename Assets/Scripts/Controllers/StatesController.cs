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

            m_GameModel.OnMenuPending += GameInitializedHandler;
            m_GameModel.OnMenuReady += GameReadyHandler;
            m_GameModel.OnStartGamePressed += StartGameHandler;
        }

        private void GameInitializedHandler()
        {
            m_StateMachine = new GameStateMachine<GameState_Abstract>();
            m_StateMachine.AddState(new GameState_MainMenu());
            m_StateMachine.AddState(new GameState_Normal());
            m_StateMachine.AddState(new GameState_Pending());
            m_StateMachine.Initialize<GameState_Pending>();
        }

        private void GameReadyHandler()
        {
            m_StateMachine.ChangeState<GameState_MainMenu>();
        }

        private void StartGameHandler()
        {
            m_StateMachine.ChangeState<GameState_Normal>();
        }
    }
}
