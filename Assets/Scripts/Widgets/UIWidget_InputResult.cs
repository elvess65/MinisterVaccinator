using CoreFramework.UI.Widget;
using MinisterVaccinator.Models;
using UnityEngine;
using UnityEngine.UI;

namespace MinisterVaccinator.Widgets
{
    public class UIWidget_InputResult : UIWidget
    {
        [SerializeField] private Image m_ImageSuccessResult = null;
        [SerializeField] private Image m_ImageWrongResult = null;

        private GameDataModel m_GameDataModel;

        public void Initialize()
        {
            InternalInitialize();

            m_ImageSuccessResult.enabled = false;
            m_ImageWrongResult.enabled = false;
        }

        public void ShowResult(bool isSuccess)
        {
            transform.localScale = Vector3.zero;

            m_ImageSuccessResult.enabled = isSuccess;
            m_ImageWrongResult.enabled = !isSuccess;

            m_ShowSequence.PlaySequence(AnimationFinishedHandler);
        }


        private void AnimationFinishedHandler()
        {

        }
    }
}
