using CoreFramework;
using MinisterVaccinator.Data.Entities;
using System.Collections.Generic;

namespace MinisterVaccinator.Models
{
    public class GameplayModel : BaseModel
    {
        public System.Action OnPrepareIteration;
        public System.Func<EntityData_Person, bool> OnValidateResult;
        public System.Action<EntityData_Person, List<EntityData_Person>, EntityData_Task> OnDisplayModeIteration;

        public EntityData_Mode CurrentMode;
        public EntityData_Task CurrentTask;
        public int CurrentLevel = 0;
    }
}
