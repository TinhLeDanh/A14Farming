using A14Farming.Buildings.Placement;
using A14Farming.System;
using Core.Utilities;
using System;
using UnityEngine;
using A14Farming.Event;
using A14Farming.Buildings.Data;
using A14Farming.UI.HUD;
using Core.BE;
using UnityEngine.UI;

namespace A14Farming.Buildings
{
    [Serializable]
    public class Building : MonoBehaviour
    {
        /// <summary>
        /// The tower levels associated with this tower
        /// </summary>
        public BuildingLevel[] levels;

        public string buildingName;

        /// <summary>
        /// Event that is fired when this instance is removed, such as when pooled or destroyed
        /// </summary>
        //public event Action<Building> removed;

        /// <summary>
        /// The current level of the tower
        /// </summary>
        public int currentLevel { get; protected set; }
        /// <summary>
        /// Reference to the data of the current level
        /// </summary>
        public BuildingLevel currentTowerLevel { get; protected set; }

        /// <summary>
        /// Gets whether the tower can level up anymore
        /// </summary>
        public bool isAtMaxLevel
        {
            get { return currentLevel == levels.Length - 1; }
        }

        /// <summary>
        /// Gets the first level tower ghost prefab
        /// </summary>
        public BuildingPlacementGhost buildingGhostPrefab
        {
            get { return levels[currentLevel].buildingGhostPrefab; }
        }

        public GameObject prefab;

        private IntVector2 tilePosOld = new IntVector2(0, 0);
        /// <summary>
        /// The size of the tower's footprint
        /// </summary>
        public IntVector2 dimensions;
        /// <summary>
        /// Gets the grid position for this tower on the <see cref="placementArea"/>
        /// </summary>
        public IntVector2 gridPosition { get; private set; }

        /// <summary>
        /// The placement area we've been built on
        /// </summary>
        public IPlacementArea placementArea { get; private set; }

        private UIInfo uiInfo = null;

        [HideInInspector]
        public bool InUpgrade = false;
        [HideInInspector]
        public bool UpgradeCompleted = false;
        private float UpgradeTimeTotal = 0.0f;
        private float UpgradeTimeLeft = 0.0f;

        private void Update()
        {
            //if (InUpgrade)
            //         {
            //	// if upgrading proceed
            //	if (!UpgradeCompleted)
            //	{
            //		// decrease left time
            //		UpgradeTimeLeft -= Time.deltaTime;

            //		// if upgrading done
            //		if (UpgradeTimeLeft < 0.0f)
            //		{
            //			UpgradeTimeLeft = 0.0f;
            //			UpgradeCompleted = true;

            //			// if building is selected, then update command dialog
            //			//if (UICommand.Visible && (SceneTown.buildingSelected == this))
            //			//	UICommand.Show(this);
            //		}
            //	}

            //             uiInfo.TimeLeft.text = UpgradeCompleted ? "Completed!" : BENumber.SecToString(Mathf.CeilToInt(UpgradeTimeLeft));
            //             uiInfo.Progress.fillAmount = (UpgradeTimeTotal - UpgradeTimeLeft) / UpgradeTimeTotal;
            //             uiInfo.groupProgress.alpha = 1;
            //             uiInfo.groupProgress.gameObject.SetActive(true);
            //         }
        }

        // initialize building
        public void InitializeButton(IPlacementArea targetArea, IntVector2 destination)
        {
            placementArea = targetArea;
            gridPosition = destination;

            if (targetArea != null)
            {
                transform.position = placementArea.GridToWorld(destination, dimensions);
                transform.rotation = placementArea.transform.rotation;
                targetArea.Occupy(destination, dimensions);
            }

            SetLevel(0);
        }

        /// <summary>
        /// Provides information on the cost to upgrade
        /// </summary>
        /// <returns>Returns -1 if the towers is already at max level, other returns the cost to upgrade</returns>
        public int GetCostForNextLevel()
        {
            if (isAtMaxLevel)
            {
                return -1;
            }
            return levels[currentLevel + 1].cost;
        }

        /// <summary>
        /// Provides the value recived for selling this tower
        /// </summary>
        /// <returns>A sell value of the tower</returns>
        public int GetSellLevel()
        {
            return GetSellLevel(currentLevel);
        }

        /// <summary>
        /// Provides the value recived for selling this tower of a particular level
        /// </summary>
        /// <param name="level">Level of tower</param>
        /// <returns>A sell value of the tower</returns>
        public int GetSellLevel(int level)
        {
            return levels[currentLevel].sell;
        }

        /// <summary>
		/// Used to (try to) upgrade the tower data
		/// </summary>
		public virtual bool UpgradeTower()
        {
            if (isAtMaxLevel)
            {
                return false;
            }
            SetLevel(currentLevel + 1);
            return true;
        }

        /// <summary>
		/// Used to set the tower to any valid level
		/// </summary>
		/// <param name="level">
		/// The level to upgrade the tower to
		/// </param>
		/// <returns>
		/// True if successful
		/// </returns>
		public virtual bool UpgradeTowerToLevel(int level)
        {
            if (level < 0 || isAtMaxLevel || level >= levels.Length)
            {
                return false;
            }
            SetLevel(level);
            return true;
        }

        public void Sell()
        {
            Remove();
        }

        /// <summary>
		/// Removes tower from placement area and destroys it
		/// </summary>
		public void Remove()
        {
            placementArea.Clear(gridPosition, dimensions);
            Destroy(gameObject);
        }

        protected void SetLevel(int level)
        {
            if (level < 0)
            {
                return;
            }

            currentLevel = level;
            if (currentTowerLevel != null)
            {
                Destroy(currentTowerLevel.gameObject);
            }

            // instantiate the visual representation
            currentTowerLevel = Instantiate(levels[currentLevel], transform);

            // initialize TowerLevel
            currentTowerLevel.Initialize(this);
        }

    }
}

