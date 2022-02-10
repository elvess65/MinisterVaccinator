using CoreFramework;
using CoreFramework.UI.View;
using CoreFramework.UI.Widget;
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
     
        private UIModel m_UIModel;
        private GameModel m_GameModel;
        private GameDataModel m_GameDataModel;
        private GameplayModel m_GameplayModel;
        private VaccinationModel m_VaccinationModel;

        private int m_AnimationsFinishedAmount = 0;
        private List<UIWidget_PersonCard> m_CardsBuffer;
        private List<EntityData_Person> m_RandomPersonDataBuffer;

        public UIWidget_InputResult ExposedWidgetInputResult => m_UIWidget_InputResult;

    
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

            RegisterWidget(m_UIWidget_Task);
            RegisterWidget(m_UIWidget_Bar_Virus);
            RegisterWidget(m_UIWidget_Bar_Vaccine);
            RegisterWidget(m_UIWidget_InputResult);

            m_GameModel.OnStartGame += StartGameHandler;
            m_GameModel.OnStopGame += StopGameHandler;

            m_GameplayModel.OnDisplayModeIteration += DisplayModeIterationHandler;

            m_VaccinationModel.OnInfectedChanged += InfectedChangedHandler;
            m_VaccinationModel.OnVaccinatedChanged += VaccinatedChangedHandler;
        }

        #region Handlers

        private void StartGameHandler()
        {
            m_UIWidget_Task.Initialize();
        }

        private void StopGameHandler(bool result)
        {
            ResetEvents();
            HideCards();

            m_CardsBuffer.Clear();
        }

        private void DisplayModeIterationHandler(EntityData_Person correctPerson, List<EntityData_Person> wrongPersons, EntityData_Task task)
        {
            m_RandomPersonDataBuffer.Add(correctPerson);
            m_RandomPersonDataBuffer.AddRange(wrongPersons);

            for (int i = 0; i < m_RandomPersonDataBuffer.Count; i++)
            {
                int rndIndex = Random.Range(0, m_RandomPersonDataBuffer.Count);

                UIWidget_PersonCard card = CreateCard(m_RandomPersonDataBuffer[rndIndex]);

                m_CardsBuffer.Add(card);
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

        #endregion

        #region Card Sequence
        
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
            HideCards();
            m_UIWidget_InputResult.ShowResult(m_GameplayModel.OnValidateResult(selectedPersonCard.PersonData));
        }

        private void PersonCardShowAnimationFinished(UIWidget sender)
        {
            m_AnimationsFinishedAmount++;

            if (m_AnimationsFinishedAmount != m_CardsBuffer.Count)
                return;

            m_AnimationsFinishedAmount = 0;

            LockCardsInput(false);
        }

        private void PersonCardHideAnimationFinished(UIWidget sender)
        {
            m_AnimationsFinishedAmount++;

            if (m_AnimationsFinishedAmount != m_CardsBuffer.Count)
                return;

            m_AnimationsFinishedAmount = 0;

            for (int i = 0; i < m_CardsBuffer.Count; i++)
                Destroy(m_CardsBuffer[i].gameObject);

            m_CardsBuffer.Clear();

            m_GameplayModel.OnPrepareIteration?.Invoke();
        }

        private void PersonCardHideAnimationFinishedDispose(UIWidget sender)
        {
            Destroy(sender.gameObject);
        }

        #endregion

        #region Tools

        private void HideCards(bool clearBuffer = false)
        {
            foreach (UIWidget_PersonCard card in m_CardsBuffer)
            {
                card.SetWidgetActive(false, true);
                card.LockInput(true);
            }
        }

        private void ResetEvents()
        {
            foreach (UIWidget_PersonCard card in m_CardsBuffer)
            {
                card.ResetEvents();
                card.OnHideSequenceFinished += PersonCardHideAnimationFinishedDispose;
            }
        }

        private void LockCardsInput(bool isInputLocked)
        {
            foreach (UIWidget_PersonCard card in m_CardsBuffer)
            {
                card.LockInput(isInputLocked);
            }
        }

        #endregion
    }
}
