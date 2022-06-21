using A14Farming.Buildings;
using A14Farming.Buildings.Data;
using A14Farming.System;
using UnityEngine;

namespace A14Farming.UI.HUD
{
    public class BuildSidebar : MonoBehaviour
    {
        public TowerSpawnButton towerButton;

        public BuildingLibrary buildingLibrary;

        /// <summary>
        /// Initialize the tower spawn buttons
        /// </summary>
        protected virtual void Start()
        {
            //foreach (BuildingDetail buildingDetail in buildingLibrary.buildings)
            //{
            //    TowerSpawnButton button = Instantiate(towerButton, transform);
            //    button.InitializeButton(buildingDetail);
            //    button.buttonTapped += OnButtonTapped;
            //}
        }

        /// <summary>
        /// Sets the GameUI to build mode with the <see cref="towerData"/>
        /// </summary>
        /// <param name="towerData"></param>
        void OnButtonTapped(Building towerData)
        {
            var gameUI = GameUI.instance;
            if (gameUI.isBuilding)
            {
                gameUI.CancelGhostPlacement();
            }
            gameUI.SetToBuildMode(towerData);
        }

        void OnDestroy()
        {
            towerButton.buttonTapped -= OnButtonTapped;
        }
    }
}

