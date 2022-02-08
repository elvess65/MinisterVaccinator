using CoreFramework;
using System.Text;
using UnityEngine;

namespace MinisterVaccinator.Data.Entities
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Task Data", menuName = "Entities/TaskData", order = 101)]
    public class EntityData_Task : ScriptableObject
    {
        [Header("Which cure will be used and which roles should be vaccinated")]
        public EntityData_Cure Cure;
        public EnumsCollection.RolesFlags RolesToVaccinate;

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine("***TASK***");
            strBuilder.AppendLine(Cure.ToString());
            strBuilder.AppendLine($"Roles: {RolesToVaccinate}");
            
            return strBuilder.ToString();
        }
    }
}
