using CoreFramework;
using FrameworkPackages.MinMax;
using MinisterVaccinator.Data.Entities;
using MinisterVaccinator.Gameplay;
using MinisterVaccinator.Models;
using System.Collections.Generic;

namespace MinisterVaccinator.Controllers
{
    public class GameplayController : BaseController
    {
        private GameModel m_GameModel;
        private GameplayModel m_GameplayModel;
        private GameDataModel m_GameDataModel;
        private VaccinationModel m_VaccinationModel;

        private PersonFactory m_PersonFactory;
        private List<EntityData_Person> m_WrongPersonsBuffer;

        public GameplayController(Dispatcher dispatcher) : base(dispatcher)
        {
            m_PersonFactory = new PersonFactory();
            m_WrongPersonsBuffer = new List<EntityData_Person>();
        }

        public override void InitializeComplete()
        {
            base.InitializeComplete();

            m_GameModel = Dispatcher.GetModel<GameModel>();
            m_GameplayModel = Dispatcher.GetModel<GameplayModel>();
            m_GameDataModel = Dispatcher.GetModel<GameDataModel>();
            m_VaccinationModel = Dispatcher.GetModel<VaccinationModel>();

            m_GameModel.OnStartGame += StartGameHandler;

            m_GameplayModel.OnPrepareIteration += PrepareIterationHandler;
            m_GameplayModel.OnValidateResult += ValidateInputResultHandler;

            m_VaccinationModel.OnAllPopulationVaccinated += PopulationVaccinatedHandler;
            m_VaccinationModel.OnAllPopulationInfected += PopulationInfectedHandler;
        }


        private void StartGameHandler()
        {
            m_GameplayModel.CurrentLevel = m_GameDataModel.CurrentLevel;

            //Cache mode  
            m_GameplayModel.CurrentMode = m_GameDataModel.Modes[m_GameplayModel.CurrentLevel];     //Get level
            m_GameplayModel.CurrentTask = m_GameplayModel.CurrentMode.Tasks[UnityEngine.Random.Range(0, m_GameplayModel.CurrentMode.Tasks.Length)]; //Get random task from level

            //Cache vaccination
            m_VaccinationModel.Population = m_GameplayModel.CurrentMode.Population.Population;
            m_VaccinationModel.CurVaccinatedAmount = m_GameplayModel.CurrentMode.Population.GetVaccinatedAmount();
            m_VaccinationModel.CurInfectedAmount = m_GameplayModel.CurrentMode.Population.GetInfectedAmount();

            //Prepare iteration
            m_GameplayModel.OnPrepareIteration?.Invoke();
        }

        private void PrepareIterationHandler()
        {  
            //Сreate correct person
            EntityData_Person correctPerson = m_PersonFactory.GetCorrectPerson(m_GameplayModel.CurrentTask);

            //Create wrong person
            for (int i = 1; i < m_GameplayModel.CurrentMode.PersonsToShow; i++)
                m_WrongPersonsBuffer.Add(m_PersonFactory.GetWrongPerson(m_GameplayModel.CurrentMode, m_GameplayModel.CurrentTask));

            //Display persons
            m_GameplayModel.OnDisplayModeIteration?.Invoke(correctPerson, m_WrongPersonsBuffer, m_GameplayModel.CurrentTask);

            m_WrongPersonsBuffer.Clear();
        }

        private bool ValidateInputResultHandler(EntityData_Person person)
        {
            bool correctAge = false;

            foreach(MinMax ageBound in m_GameplayModel.CurrentTask.Cure.AgesToApply)
            {
                if (person.Age >= ageBound.Min && person.Age < ageBound.Max)
                {
                    correctAge = true;
                    break;
                }
            }

            bool correctRole = m_GameplayModel.CurrentTask.RolesToVaccinate.HasFlag(person.Role);

            bool isSuccess = correctAge && correctRole;

            if (isSuccess)
                m_VaccinationModel.CurVaccinatedAmount++;

            return isSuccess;
        }

        private void PopulationVaccinatedHandler() => m_GameModel.OnStopGame?.Invoke(true);

        private void PopulationInfectedHandler() => m_GameModel.OnStopGame?.Invoke(false);
    }
}
