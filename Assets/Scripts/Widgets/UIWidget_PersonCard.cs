using CoreFramework.UI.Widget;
using MinisterVaccinator.Assets.Battle;
using MinisterVaccinator.Data.Entities;
using MinisterVaccinator.Models;
using UnityEngine;
using UnityEngine.UI;

namespace MinisterVaccinator.Widgets
{
    public class UIWidget_PersonCard : UIWidget_Button
    {
        [Header("Text")]
        [SerializeField] private Text Text_Age = null;
        [SerializeField] private Text Text_Role = null;

        [Header("Icon")]
        [SerializeField] private Image Image_Body = null;
        [SerializeField] private Image Image_Face = null;
        [SerializeField] private Image Image_Hair = null;
        [SerializeField] private Image Image_Kit = null;
        [SerializeField] private Image Image_Background = null;

        private UISpriteAssets m_SpriteAssets;

        public EntityData_Person PersonData { get; private set; }


        public void Initialize(EntityData_Person personData)
        {
            Initialize();

            SetWidgetActive(false, false);
            SetWidgetActive(true, true);
            LockInput(true);

            PersonData = personData;

            GameDataModel gameDataModel = Dispatcher.GetModel<GameDataModel>();
            m_SpriteAssets = gameDataModel.UISpriteAssets;

            GenerateRandomPersonIcon();
            ShowPersonData(personData);
        }

        private void GenerateRandomPersonIcon()
        {
            Image_Body.sprite = m_SpriteAssets.GetRandomElementFrom(m_SpriteAssets.Body);
            Image_Face.sprite = m_SpriteAssets.GetRandomElementFrom(m_SpriteAssets.Face);
            Image_Hair.sprite = m_SpriteAssets.GetRandomElementFrom(m_SpriteAssets.Hair);
            Image_Kit.sprite = m_SpriteAssets.GetRandomElementFrom(m_SpriteAssets.Kit);
            Image_Background.color = m_SpriteAssets.GetRandomElementFrom(m_SpriteAssets.Background);
        }

        private void ShowPersonData(EntityData_Person personData)
        {
            Text_Age.text = $"Age: {personData.Age}";
            Text_Role.text = personData.Role.ToString();
        }
    }
}
