using CoreFramework;

namespace MinisterVaccinator.Models
{
    public class GameModel : BaseModel
    {
        /// <summary>
        /// Main Menu in waiting to be ready
        /// </summary>
        public System.Action OnMainMenuPending;

        /// <summary>
        /// Main Menu is ready
        /// </summary>
        public System.Action OnMainMenuReady;

        /// <summary>
        /// Play button pressed
        /// </summary>
        public System.Action OnStartGamePressed;

        /// <summary>
        /// Game has started
        /// </summary>
        public System.Action OnStartGame;

        /// <summary>
        /// Game has stopped
        /// </summary>
        public System.Action<bool> OnStopGame;

        public System.Action OnVictory;
        public System.Action OnGameOver;
        public System.Action OnTransitionFromGameFinishToNewGame;
        public System.Action OnTransitionFromGameFinishToMainMenu;
    }
}
