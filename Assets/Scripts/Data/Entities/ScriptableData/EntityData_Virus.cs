using UnityEngine;

namespace MinisterVaccinator.Data.Entities
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Virus Data", menuName = "Entities/VirusData", order = 101)]
    public class EntityData_Virus : ScriptableObject
    {
        [Tooltip("Количество людей, зараженных за секунду")]
        public float SpreadSpreadSec = 0.25f;        
    }
}

