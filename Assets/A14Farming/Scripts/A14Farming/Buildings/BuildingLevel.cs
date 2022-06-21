using A14Farming.Buildings.Data;
using A14Farming.Buildings.Placement;
using UnityEngine;

namespace A14Farming.Buildings
{
    /// <summary>
	/// An individual level of a building
	/// </summary> 
	[DisallowMultipleComponent]
    public class BuildingLevel : MonoBehaviour
    {
		/// <summary>
		/// The prefab for communicating placement in the scene
		/// </summary>
		public BuildingPlacementGhost buildingGhostPrefab;

		/// <summary>
		/// Reference to scriptable object with level data on it
		/// </summary>
		public BuildingLevelData levelData;

		/// <summary>
		/// The parent tower controller of this tower
		/// </summary>
		public Building m_ParentTower;

		/// <summary>
		/// The physics layer mask that the tower searches on
		/// </summary>
		public LayerMask mask { get; protected set; }

		/// <summary>
		/// Gets the cost value
		/// </summary>
		public int cost
		{
			get { return levelData.cost; }
		}

		/// <summary>
		/// Gets the sell value
		/// </summary>
		public int sell
		{
			get { return levelData.sell; }
		}

		/// <summary>
		/// Gets the tower description
		/// </summary>
		public string description
		{
			get { return levelData.description; }
		}

		/// <summary>
		/// Gets the tower description
		/// </summary>
		public string upgradeDescription
		{
			get { return levelData.upgradeDescription; }
		}

		/// <summary>
		/// Initialises the Effects attached to this object
		/// </summary>
		public virtual void Initialize(Building tower)
		{
			m_ParentTower = tower; 
		}
	}
}

