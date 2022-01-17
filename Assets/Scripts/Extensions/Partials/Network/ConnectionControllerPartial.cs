using MinisterVaccinator.Setup;

namespace CoreFramework.Network
{
    public partial class ConnectionController
    {
        partial void Setup(ConnectionSuccessResult connectionResult)
        {
            IGameSetup gameSetup = new GameSetup(new DataSetup(connectionResult.SerializedGameData), new BehaviourSetup());
            gameSetup.Setup();
        }
    }
}
