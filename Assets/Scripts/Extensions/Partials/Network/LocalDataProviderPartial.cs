using UnityEngine;

namespace CoreFramework.Network
{
    partial class LocalDataProvider
    {
        partial void SimulateSuccessConnectionDelay()
        {
            OnConnectionSuccess?.Invoke(new ConnectionSuccessResult(JsonUtility.ToJson(m_DataObject.GameData)));
        }

        partial void SimulateErrorConnectionDelay(int errorCode)
        {
            OnConnectionError?.Invoke(errorCode);
        }
    }
}
