using CoreFramework;
using FrameworkPackages.MinMax;
using System.Text;
using UnityEngine;

namespace MinisterVaccinator.Data.Entities
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Cure Data", menuName = "Entities/CureData", order = 101)]
    public class EntityData_Cure : ScriptableObject
    {
        public EnumsCollection.Cures Cure;
        [MinMaxSlider(1, 100)]
        public MinMax[] AgesToApply;

        public bool HasNoAge => AgesToApply.Length == 0;

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine($"***CURE");
            strBuilder.AppendLine($"Type: {Cure}");
            strBuilder.AppendLine($"Ages to Apply:");
            for (int i = 0; i < AgesToApply.Length; i++)
                strBuilder.AppendLine($" - {AgesToApply}");

            return strBuilder.ToString();
        }
    }
}
