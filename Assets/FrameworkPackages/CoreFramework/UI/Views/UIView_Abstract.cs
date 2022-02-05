using System.Collections.Generic;
using CoreFramework.UI.Widget;
using UnityEngine;

namespace CoreFramework.UI.View
{
    /// <summary>
    /// Базовый класс для UI View
    /// </summary>
    public abstract class UIView_Abstract : BaseView
    {
        public Transform Root;

        protected List<UIWidget> m_Widgets = new List<UIWidget>();

        private void Awake()
        {
            Initialize();
        }

        public abstract void Initialize();

        public void LockInput(bool isLocked)
        {
            for (int i = 0; i < m_Widgets.Count; i++)
            {
                m_Widgets[i].LockInput(isLocked);
            }
        }

        /// <summary>
        /// Включить/выключить визуальное отображение виджета
        /// </summary>
        /// <param name="isEnabled">Включен/выключен виджет</param>
        /// <param name="isAnimated">Анимация при выполнении</param>
        public void SetWidgetsActive(bool isEnabled, bool isAnimated, params UIWidget[] excludedFromActivatingWidgets)
        {
            for (int i = 0; i < m_Widgets.Count; i++)
            {
                if (!SkipWidget(m_Widgets[i], excludedFromActivatingWidgets))
                {
                    m_Widgets[i].SetWidgetActive(isEnabled, isAnimated);
                }
            }
        }


        /// <summary>
        /// Регистрирует виджет с списке управляемых (для включения/отключения)
        /// </summary>
        /// <param name="widget"></param>
        protected void RegisterWidget(UIWidget widget)
        {
            m_Widgets.Add(widget);
        }

        /// <summary>
        /// Попадает ли текущий виджет в список исключений
        /// </summary>
        /// <returns></returns>
        private bool SkipWidget(UIWidget currentWidget, UIWidget[] excludedFromActivatingWidgets)
        {
            if (excludedFromActivatingWidgets != null)
            {
                for (int j = 0; j < excludedFromActivatingWidgets.Length; j++)
                {
                    if (currentWidget == excludedFromActivatingWidgets[j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
