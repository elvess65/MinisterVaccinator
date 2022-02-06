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
        /// Any screen is waiting
        /// </summary>
        public System.Action OnPending;

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
    }
}
