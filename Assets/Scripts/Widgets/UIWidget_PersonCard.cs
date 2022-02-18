using CoreFramework.UI.Widget;
using MinisterVaccinator.Assets.Battle;
using MinisterVaccinator.Data.Entities;
using MinisterVaccinator.Models;
using UnityEngine;
using UnityEngine.UI;
using static CoreFramework.EnumsCollection;

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

        public EntityData_Person PersonData { get; private set; }

        public void Initialize(EntityData_Person personData)
        {
            Initialize();

            SetWidgetActive(false, false);
            SetWidgetActive(true, true);
            LockInput(true);

            PersonData = personData;
            ShowPersonData(personData);
        }

        private void ShowPersonData(EntityData_Person personData)
        {
            GameDataModel gameDataModel = Dispatcher.GetModel<GameDataModel>();
            UIPersonAssets uiPersonAssets = gameDataModel.UISpriteAssets.GetUIPersonAssets(personData.Role, personData.Age);

            Image_Body.sprite = uiPersonAssets.GetRandomBody();
            Image_Face.sprite = uiPersonAssets.GetRandomFace();
            Image_Hair.sprite = uiPersonAssets.GetRandomHair();
            Image_Kit.sprite = uiPersonAssets.GetRandomKit();
           
            Image_Background.color = gameDataModel.UISpriteAssets.GetRandomElementFrom(gameDataModel.UISpriteAssets.Background);

            Text_Age.text = personData.Age > 0 ? $"Age: {personData.Age}" : string.Empty;
            Text_Role.text = personData.Role.ToString();
        }
    }
}
