namespace CoreFramework.Network
{
    public partial class ConnectionSuccessResult
    {
        public string SerializedGameData { get; }

        public ConnectionSuccessResult(string serializedGameData)
        {
            SerializedGameData = serializedGameData;
        }
    }
}
