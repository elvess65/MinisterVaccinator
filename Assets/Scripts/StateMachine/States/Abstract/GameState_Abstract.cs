using CoreFramework.StateMachine;
using MinisterVaccinator.Models;

namespace MinisterVaccinator.StateMachine
{
    public abstract class GameState_Abstract : AbstractState
    {
        protected UIModel m_UIModel;

        public GameState_Abstract()
        {
            m_UIModel = Dispatcher.GetModel<UIModel>();
        }
    }
}
