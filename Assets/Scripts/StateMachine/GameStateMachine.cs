using CoreFramework.StateMachine;

namespace MinisterVaccinator.StateMachine
{
    public class GameStateMachine<T> : AbstractStateMachine<T> where T: GameState_Abstract
    {
    }
}
