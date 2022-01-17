using UnityEngine;

namespace MinisterVaccinator.Extensions
{
    public class AnimationEventListener : MonoBehaviour
    {
        public System.Action OnAnimationEvent;

        public void AnimationHandler()
        {
            OnAnimationEvent?.Invoke();
        }
    }
}
