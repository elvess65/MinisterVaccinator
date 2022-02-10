using UnityEngine;
using static CoreFramework.EnumsCollection;

namespace MinisterVaccinator.Assets.Battle
{
    [CreateAssetMenu(fileName = "New UISprite Assets", menuName = "Assets/UISprite Assets", order = 101)]
    public class UISpriteAssets : ScriptableObject
    {
        public UIPersonAssets[] PersonAssets;
        public Color[] Background;
        public Sprite Victory;
        public Sprite GameOver;

        public T GetRandomElementFrom<T>(T[] arr)
        {
            return arr[Random.Range(0, arr.Length)];
        }

        internal void Initialize()
        {
        }

        public UIPersonAssets GetUIPersonAssets(Roles role, int age)
        {
            for (int i = 0; i < PersonAssets.Length; ++i)
            {
                if (PersonAssets[i].Role == role && age >= PersonAssets[i].MinAge && age <= PersonAssets[i].MaxAge)
                {
                    return PersonAssets[i];
                }
            }

            throw new UnityException($"Can't find a person with parameters: {role} {age}");
        }
    }
}