using CoreFramework;
using CoreFramework.Input;
using MinisterVaccinator.Controllers;
using MinisterVaccinator.Models;

namespace MinisterVaccinator.Setup
{
    /// <summary>
    /// Setup для боя
    /// </summary>
    public class BehaviourSetup : IGameSetup
    {
        public void Setup()
        {
            Dispatcher dispatcher = Dispatcher.Instance;

            // Controllers
            dispatcher.CreateController<InputController>();
            dispatcher.CreateController<StatesController>();
            dispatcher.CreateController<GameplayController>();
            dispatcher.CreateController<VaccinationController>();

            // Models
            dispatcher.CreateModel<InputModel>();
            dispatcher.CreateModel<UIModel>();
            dispatcher.CreateModel<GameModel>();
            dispatcher.CreateModel<GameplayModel>();
            dispatcher.CreateModel<VaccinationModel>();
        }
    }
}
