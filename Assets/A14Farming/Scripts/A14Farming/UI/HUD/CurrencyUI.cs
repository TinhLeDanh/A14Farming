using A14Farming.System;
using Core.Economy;
using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace A14Farming.UI.HUD
{
	/// <summary>
	/// A class for controlling the displaying the currency
	/// </summary>
	public class CurrencyUI : Singleton<CurrencyUI>
	{
		/// <summary>
		/// The text element to display information on
		/// </summary>
		public Text txt_Exp;
		public Text txt_Gold;
		public Text txt_ExtraGold;
		public Text txt_Gem;

		public Currency m_Exp { get; protected set; }
		public Currency m_Gold { get; protected set; }
		public Currency m_ExtraGold { get; protected set; }
		public Currency m_Gem { get; protected set; }

        /// <summary>
        /// Assign the correct currency value
        /// </summary>
        protected virtual void Start()
		{
			if (GameHandler.instance != null)
			{
				m_Exp = new Currency(int.Parse(GameHandler.instance.userInfo.exp));
				m_Gold = new Currency(int.Parse(GameHandler.instance.userInfo.gold));
				m_ExtraGold = new Currency(int.Parse(GameHandler.instance.userInfo.extraGold));
				m_Gem = new Currency(int.Parse(GameHandler.instance.userInfo.gem));

				UpdateDisplay();
				m_Gold.currencyChanged += UpdateDisplay;
			}
			else
			{
				Debug.LogError("[UI] No level manager to get currency from");
			}
		}

		/// <summary>
		/// Unsubscribe from events
		/// </summary>
		protected override void OnDestroy()
		{
			if (m_Gold != null)
			{
				m_Gold.currencyChanged -= UpdateDisplay;
			}
		}

		/// <summary>
		/// A method for updating the display based on the current currency
		/// </summary>
		protected void UpdateDisplay()
		{
			txt_Exp.text = m_Exp.currentCurrency.ToString();
			txt_Gold.text = m_Gold.currentCurrency.ToString();
			txt_ExtraGold.text = m_ExtraGold.currentCurrency.ToString();
			txt_Gem.text = m_Gem.currentCurrency.ToString();
		}
	}
}

