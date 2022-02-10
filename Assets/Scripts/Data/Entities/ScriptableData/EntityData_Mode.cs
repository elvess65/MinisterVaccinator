using CoreFramework;
using FrameworkPackages.MinMax;
using System.Text;
using UnityEngine;

namespace MinisterVaccinator.Data.Entities
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Mode Data", menuName = "Entities/ModeData", order = 101)]
    public class EntityData_Mode : ScriptableObject
    {
        [Header("Rules of the game mode")]
        public EntityData_Task[] Tasks;
        public EntityData_Virus Virus;
        public EntityData_Population Population;

        [Space]
        [Header("Display")]
        public int PersonsToShow = 2;
        public EnumsCollection.RolesFlags RolesToShow;
        [MinMaxSlider(1, 100)]
        public MinMax AgeToShow;

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine("***MODE***");
            strBuilder.AppendLine($"Tasks ({Tasks.Length}]):");
            for (int i = 0; i < Tasks.Length; i++)
            {
                strBuilder.AppendLine($"{Tasks[i].ToString()}");
            }
            strBuilder.AppendLine($"PersonsToShow: {PersonsToShow}");
            strBuilder.AppendLine($"RolesToShow: {RolesToShow}");
            strBuilder.AppendLine($"AgeGroupsToShow: {AgeToShow}");

            return strBuilder.ToString();
        }
    }
}
