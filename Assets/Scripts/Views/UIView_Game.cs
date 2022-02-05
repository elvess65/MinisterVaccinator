using CoreFramework;
using CoreFramework.UI.View;
using MinisterVaccinator.Data.Entities;
using MinisterVaccinator.Models;
using MinisterVaccinator.Widgets;
using System.Collections.Generic;
using UnityEngine;

namespace MinisterVaccinator.Views
{
    public class UIView_Game : UIView_Abstract
    {
        [SerializeField] private RectTransform m_CardsParent = null;
        [SerializeField] private UIWidget_Task m_UIWidget_Task = null;
        [SerializeField] private UIWidget_Bar m_UIWidget_Bar_Virus = null;
        [SerializeField] private UIWidget_Bar m_UIWidget_Bar_Vaccine = null;
        [SerializeField] private UIWidget_InputResult m_UIWidget_InputResult = null;
        [SerializeField] private UIWidget_FinishGame m_UIWidget_FinishGame = null;

        private UIModel m_UIModel;
        private GameModel m_GameModel;
        private GameDataModel m_GameDataModel;
        private GameplayModel m_GameplayModel;
        private VaccinationModel m_VaccinationModel;

        private int m_AnimationsFinishedAmount = 0;
        private EntityData_Person m_SelectedPersonData;
        private List<UIWidget_PersonCard> m_CardsBuffer;
        private List<EntityData_Person> m_RandomPersonDataBuffer;

        public UIWidget_Text ExposedWidgetTask => m_UIWidget_Task;
        public UIWidget_Bar ExposedWidget_Bar_Virus => m_UIWidget_Bar_Virus;
        public UIWidget_Bar ExposedWidget_Bar_Vaccine => m_UIWidget_Bar_Vaccine;
        public UIWidget_FinishGame ExposedWidget_FinishGame => m_UIWidget_FinishGame;

        public override void Initialize()
        {
            m_UIModel = Dispatcher.GetModel<UIModel>();
            m_GameModel = Dispatcher.GetModel<GameModel>();
            m_GameDataModel = Dispatcher.GetModel<GameDataModel>();
            m_GameplayModel = Dispatcher.GetModel<GameplayModel>();
            m_VaccinationModel = Dispatcher.GetModel<VaccinationModel>();

            m_UIModel.UIViewGame = this;
            m_CardsBuffer = new List<UIWidget_PersonCard>();
            m_RandomPersonDataBuffer = new List<EntityData_Person>();

            m_UIWidget_InputResult.Initialize();
            m_UIWidget_Bar_Virus.Initialize();
            m_UIWidget_Bar_Vaccine.Initialize();
            m_UIWidget_FinishGame.Initialize();

            RegisterWidget(m_UIWidget_Task);
            RegisterWidget(m_UIWidget_Bar_Virus);
            RegisterWidget(m_UIWidget_Bar_Vaccine);
            RegisterWidget(m_UIWidget_InputResult);
            RegisterWidget(m_UIWidget_FinishGame);

            m_GameModel.OnStartGame += StartGameHandler;
  
            m_GameplayModel.OnDisplayModeIteration += DisplayModeIterationHandler;
            m_VaccinationModel.OnInfectedChanged += InfectedChangedHandler;
            m_VaccinationModel.OnVaccinatedChanged += VaccinatedChangedHandler;
        }

        private void StartGameHandler()
        {
            m_UIWidget_Task.Initialize();
  
            m_UIWidget_Task.SetWidgetActive(true, true);
            m_UIWidget_Bar_Virus.SetWidgetActive(true, true);
            m_UIWidget_Bar_Vaccine.SetWidgetActive(true, true);
        }


        private void DisplayModeIterationHandler(EntityData_Person correctPerson, List<EntityData_Person> wrongPersons, EntityData_Task task)
        {
            m_RandomPersonDataBuffer.Add(correctPerson);
            m_RandomPersonDataBuffer.AddRange(wrongPersons);

            for (int i = 0; i < m_RandomPersonDataBuffer.Count; i++)
            {
                int rndIndex = Random.Range(0, m_RandomPersonDataBuffer.Count);
                m_CardsBuffer.Add(CreateCard(m_RandomPersonDataBuffer[rndIndex]));
                m_RandomPersonDataBuffer.RemoveAt(rndIndex);
                i--;
            }

            m_RandomPersonDataBuffer.Clear();
        }

        private void InfectedChangedHandler()
        {
            m_UIWidget_Bar_Virus.UpdateProgress(m_VaccinationModel.CurInfectedAmount, m_GameplayModel.CurrentMode.Population.Population);
        }

        private void VaccinatedChangedHandler()
        {
            m_UIWidget_Bar_Vaccine.UpdateProgress(m_VaccinationModel.CurVaccinatedAmount, m_GameplayModel.CurrentMode.Population.Population);
        }


        private UIWidget_PersonCard CreateCard(EntityData_Person personData)
        {
            UIWidget_PersonCard personCard = m_GameDataModel.UIAssets.InstantiatePrefab(m_GameDataModel.UIAssets.PersonCardPrefab);
            personCard.Root.SetParent(m_CardsParent);
            personCard.transform.localPosition = Vector3.zero;
            personCard.Initialize(personData);

            personCard.OnShowSequenceFinished += PersonCardShowAnimationFinished;
            personCard.OnHideSequenceFinished += PersonCardHideAnimationFinished;
            personCard.OnWidgetPress += () => PersonCardPressHandler(personCard);

            return personCard;
        }

        private void PersonCardPressHandler(UIWidget_PersonCard selectedPersonCard)
        {
            m_SelectedPersonData = selectedPersonCard.PersonData;

            foreach (UIWidget_PersonCard card in m_CardsBuffer)
            {
                card.SetWidgetActive(false, true);
                card.LockInput(true);
            }

            m_UIWidget_InputResult.ShowResult(m_GameplayModel.OnValidateResult(m_SelectedPersonData));
        }

        private void PersonCardShowAnimationFinished()
        {
            m_AnimationsFinishedAmount++;

            if (m_AnimationsFinishedAmount != m_CardsBuffer.Count)
                return;

            m_AnimationsFinishedAmount = 0;

            foreach (UIWidget_PersonCard card in m_CardsBuffer)
            {
                card.LockInput(false);
            }
        }

        private void PersonCardHideAnimationFinished()
        {
            m_AnimationsFinishedAmount++;

            if (m_AnimationsFinishedAmount != m_CardsBuffer.Count)
                return;

            m_AnimationsFinishedAmount = 0;

            for (int i = 0; i < m_CardsBuffer.Count; i++)
                Destroy(m_CardsBuffer[i].gameObject);

            m_CardsBuffer.Clear();

            Debug.Log("Prepare next iteration");
            m_GameplayModel.OnPrepareIteration?.Invoke();
        }
    }
}
