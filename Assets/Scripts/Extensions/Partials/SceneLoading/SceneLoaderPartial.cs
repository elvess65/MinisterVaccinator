using MinisterVaccinator;
using MinisterVaccinator.Core;
using MinisterVaccinator.Models;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CoreFramework.SceneLoading
{
    public partial class SceneLoader
    {
        public const string MAIN_SCENE_NAME = "MainScene";

        private const string m_TRANSITION_SCENE_NAME = "TransitionScene";
        private const string m_BOOT_SCENE_NAME = "BootScene";

        private GameModel m_GameModel;

        partial void PartialConstructorCall()
        {
            LoadLevel(m_TRANSITION_SCENE_NAME);
        }

        partial void SceneLoadCompleteHandler()
        {
            switch (m_CurrentLoadingLevel)
            {
                case MAIN_SCENE_NAME:
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(MAIN_SCENE_NAME));
                    SceneLoadingManager.Instance.FadeOut();

                    SceneLoadingManager.Instance.StartCoroutine(InitializationCoroutine());

                    break;

                case m_TRANSITION_SCENE_NAME:
                    GameManager.Instance.InitializeConnection();
                    break;
            }
        }

        IEnumerator InitializationCoroutine()
        {
            yield return null;

            if (m_GameModel == null)
                m_GameModel = Dispatcher.GetModel<GameModel>();

            m_GameModel.OnMainMenuPending?.Invoke();

            yield return new WaitForSeconds(ConstsCollection.INITIALIZATION_DELAY);

            m_GameModel.OnMainMenuReady?.Invoke();
        }
    }
}
