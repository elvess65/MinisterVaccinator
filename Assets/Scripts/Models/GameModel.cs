using CoreFramework;

namespace MinisterVaccinator.Models
{
    public class GameModel : BaseModel
    {
        /// <summary>
        /// Menu in waiting to be ready
        /// </summary>
        public System.Action OnMenuPending;

        /// <summary>
        /// Menu is ready
        /// </summary>
        public System.Action OnMenuReady;

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
        public System.Action OnStopGame;
    }
}
