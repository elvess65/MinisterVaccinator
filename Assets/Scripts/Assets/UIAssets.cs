using MinisterVaccinator.Assets.Abstract;
using MinisterVaccinator.Widgets;
using UnityEngine;

namespace MinisterVaccinator.Assets.Battle
{
    [CreateAssetMenu(fileName = "New UI Assets", menuName = "Assets/UI Assets", order = 101)]
    public class UIAssets : PrefabAssets
    {
        public UIWidget_PersonCard PersonCardPrefab;

        public override void Initialize()
        {
        }
    }
}
