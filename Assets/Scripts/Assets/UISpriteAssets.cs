using UnityEngine;

namespace MinisterVaccinator.Assets.Battle
{
    [CreateAssetMenu(fileName = "New UISprite Assets", menuName = "Assets/UISprite Assets", order = 101)]
    public class UISpriteAssets : ScriptableObject
    {
        public Sprite[] Body;
        public Sprite[] Face;
        public Sprite[] Hair;
        public Sprite[] Kit;
        public Color[] Background;

        public T GetRandomElementFrom<T>(T[] arr)
        {
            return arr[Random.Range(0, arr.Length)];
        }

        public void Initialize()
        {
        
        }
    }
}