using A14Farming.Buildings;
using A14Farming.Buildings.Data;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace A14Farming.UI.HUD
{
    /// <summary>
	/// A button controller for spawning towers
	/// </summary>
	[RequireComponent(typeof(RectTransform))]
    public class TowerSpawnButton : MonoBehaviour
    {
		public Image towerIcon;

		public Button buyButton;

		/// <summary>
		/// Fires when the button is tapped
		/// </summary>
		public event Action<Building> buttonTapped;

		/// <summary>
		/// The tower controller that defines the button
		/// </summary>
		Building m_Tower;

		/// <summary>
		/// The attached rect transform
		/// </summary>
		RectTransform m_RectTransform;

		/// <summary>
		/// Cache the rect transform
		/// </summary>
		protected virtual void Awake()
		{
			m_RectTransform = (RectTransform) transform;
		}

		/// <summary>
		/// Define the button information for the tower
		/// </summary>
		/// <param name="towerData">
		/// The tower to initialize the button with
		/// </param>
		public void InitializeButton(BuildingLevelData towerData)
		{
			//m_Tower = towerData.building;
		}

		/// <summary>
		/// The click for when the button is tapped
		/// </summary>
		public void OnClick()
		{
			if (buttonTapped != null)
			{
				buttonTapped(m_Tower);
			}
		}
	}
}
