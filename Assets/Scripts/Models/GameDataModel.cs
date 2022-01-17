using CoreFramework;
using MinisterVaccinator.Assets.Battle;
using MinisterVaccinator.Data.Entities;

namespace MinisterVaccinator.Models
{
    public class GameDataModel : BaseModel
    {
        public UIAssets UIAssets;
        public UISpriteAssets UISpriteAssets;
        public EntityData_Mode[] Modes;

        public override void Initialize()
        {
            base.Initialize();

            UIAssets.Initialize();
            UISpriteAssets.Initialize();
        }
    }
}
