using UnityEngine;
using UnityEngine.UI;

namespace CoreFramework.UI.Widget
{
    /// <summary>
    /// Базовый класс виджета, на который можно нажимать
    /// </summary>
    public abstract class UIWidget_Clickable : UIWidget
    {
        public System.Action OnWidgetPress;

        [SerializeField] public Button WidgetButton;


        public override void LockInput(bool isLocked)
        {
            WidgetButton.enabled = !isLocked;
        }

        public override void ResetEvents()
        {
            base.ResetEvents();

            OnWidgetPress = null;
        }

        protected override void InternalInitialize()
        {
            base.InternalInitialize();

            WidgetButton.onClick.AddListener(WidgetPressHandler);
        }

        protected virtual void WidgetPressHandler()
        {
            OnWidgetPress?.Invoke();
        }
    }
}
