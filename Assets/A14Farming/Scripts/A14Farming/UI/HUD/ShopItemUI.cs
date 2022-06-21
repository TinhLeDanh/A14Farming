using A14Farming.Buildings;
using Core.BE;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace A14Farming.UI.HUD
{
    public class ShopItemUI : MonoBehaviour
    {
        Building building;

        public int Index = 0;
        public Image Border;
        public Image Background;
        public Text Name;
        public Text Info;
        public Image Icon;
        public GameObject goDisabled;
        public Text DisabledInfo;
        public GameObject goBuild;
        public Image PriceIcon;
        public Text Price;
        public Text BuildTimeInfo;
        public Text BuildTime;
        public Text BuildCountInfo;
        public Text BuildCount;

        /// <summary>
		/// Fires when the button is tapped
		/// </summary>
		public event Action<Building> buttonTapped;

        // initialize with building
        public void Init(int index, Building m_building)
        {
            building = m_building;
            Index = index;

            Name.text = building.buildingName;
            Info.text = building.levels[0].description;

            BuildTimeInfo.text = "Build time:";
            BuildTime.text = BENumber.SecToString((int)building.levels[0].levelData.buildTime);
            BuildCountInfo.text = "Built:";

            Icon.sprite = building.levels[0].levelData.icon;
        }

        public void Clicked()
        {
            if (buttonTapped != null)
            {
                buttonTapped(building);
                ShopUI.instance.Hide();
            }
        }
    }
}

