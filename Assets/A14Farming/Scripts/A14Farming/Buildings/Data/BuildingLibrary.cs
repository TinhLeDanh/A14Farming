using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A14Farming.Buildings.Data
{
    /// <summary>
	/// The asset which holds the list of different towers
	/// </summary>
	[CreateAssetMenu(fileName = "BuildingLibrary.asset", menuName = "A14Farming/Building library", order = 1)]
    public class BuildingLibrary : ScriptableObject
    {
		/// <summary>
		/// The list of all the towers
		/// </summary>
		public List<Building> buildings;


	}
}

