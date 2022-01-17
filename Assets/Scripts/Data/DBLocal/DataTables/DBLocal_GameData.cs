using MinisterVaccinator.Assets.Battle;
using MinisterVaccinator.Data.Entities;
using UnityEngine;

namespace MinisterVaccinator.Data.DataBaseLocal
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Local GameData", menuName = "DBLocal/GameData", order = 101)]
    public class DBLocal_GameData : ScriptableObject
    {
        public UIAssets UIAssets;
        public UISpriteAssets UISpriteAssets;
        public EntityData_Mode[] Modes;
    }
}
