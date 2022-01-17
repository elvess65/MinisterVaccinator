using CoreFramework.Abstract;
using CoreFramework.Network;
using CoreFramework.SceneLoading;
using UnityEngine;

namespace MinisterVaccinator.Core
{
    public class GameManager : Singleton<GameManager>
    {
        public SceneLoader SceneLoader { get; private set; }

        public void InitializeConnection()
        {
            ConnectionController connectionController = new ConnectionController();
            connectionController.OnConnectionSuccess += ConnectionResultSuccess;
            connectionController.OnConnectionError += ConnectionResultError;
            connectionController.Connect();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            SceneLoader = new SceneLoader();
        }

        private void ConnectionResultSuccess()
        {
            SceneLoader.LoadLevel(SceneLoader.MAIN_SCENE_NAME);
        }

        private void ConnectionResultError(int errorCode)
        {
            Debug.Log($"Error connection. Code {errorCode}");
        }
    }
}
