using CoreFramework;
using MinisterVaccinator.Models;

namespace MinisterVaccinator.Controllers
{
    public class VaccinationController : BaseController
    {
        private GameModel m_GameModel;
        private UpdateModel m_UpdateModel;
        private GameplayModel m_GameplayModel;
        private GameDataModel m_GameDataModel;
        private VaccinationModel m_VaccinationModel;
        private float m_InfectedFloatCounter = 0;

        public VaccinationController(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public override void InitializeComplete()
        {
            base.InitializeComplete();

            m_GameModel = Dispatcher.GetModel<GameModel>();
            m_UpdateModel = Dispatcher.GetModel<UpdateModel>();
            m_GameplayModel = Dispatcher.GetModel<GameplayModel>();
            m_GameDataModel = Dispatcher.GetModel<GameDataModel>();
            m_VaccinationModel = Dispatcher.GetModel<VaccinationModel>();

            m_GameModel.OnStartGame += StartGameHandler;
            m_GameModel.OnStopGame += StopGameHandler;
        }

        private void StartGameHandler() => m_UpdateModel.OnUpdate += HandleUpdate;

        private void StopGameHandler(bool result) => m_UpdateModel.OnUpdate -= HandleUpdate;

        private void HandleUpdate(float deltaTime)
        {
            if (m_InfectedFloatCounter <= 1)
            {
                m_InfectedFloatCounter += m_GameplayModel.CurrentMode.Virus.SpreadSpreadSec * deltaTime;
            }
            else
            {
                m_InfectedFloatCounter = 0;
                m_VaccinationModel.CurInfectedAmount++;
            }
        }
    }
}
