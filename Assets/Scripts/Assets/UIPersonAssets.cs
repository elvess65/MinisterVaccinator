using UnityEngine;
using static CoreFramework.EnumsCollection;

namespace MinisterVaccinator.Assets.Battle
{
    [CreateAssetMenu(fileName = "UIPersonAssets", menuName = "Assets/UIPersonAssets", order = 101)]
    public class UIPersonAssets : ScriptableObject
    {
        public RolesFlags Role;
        public int MinAge;
        public int MaxAge;
        public Sprite[] Body;
        public Sprite[] Face;
        public Sprite[] Hair;
        public Sprite[] Kit;

        public Sprite GetRandomBody()
        {
            return Body[Random.Range(0, Body.Length)];
        }

        public Sprite GetRandomFace()
        {
            return Face[Random.Range(0, Face.Length)];
        }

        public Sprite GetRandomHair()
        {
            return Hair[Random.Range(0, Hair.Length)];
        }

        public Sprite GetRandomKit()
        {
            return Kit[Random.Range(0, Kit.Length)];
        }
    }
}