using UnityEngine;

namespace MinisterVaccinator.Data.Entities
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Population Data", menuName = "Entities/PopulationData", order = 101)]
    public class EntityData_Population : ScriptableObject
    {
        public int Population = 100;
        [Range(0, 90)]
        public int VaccinatedPercent = 50;

        [Range(0, 90)]
        public int InfectedPercent = 50;

        public int GetInfectedAmount() => GetAmount(InfectedPercent);

        public int GetVaccinatedAmount() => GetAmount(VaccinatedPercent);

        private int GetAmount(int percent)
        {
            float result = Population * (percent / 100f);
            return (int)result;
        }
    }
}
