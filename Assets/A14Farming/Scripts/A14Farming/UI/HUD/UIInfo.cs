using A14Farming.Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace A14Farming.UI.HUD
{
    public class UIInfo : MonoBehaviour
    {
		public CanvasGroup groupRoot;
		public CanvasGroup groupInfo;
		public Text Name;
		public Text Level;

		public CanvasGroup groupProgress;
		public Text TimeLeft;
		public Image Progress;
		public Image Icon;

		public CanvasGroup groupCollect;
		public Image CollectDialog;
		public Image CollectIcon;

		public Building building = null;
	}
}

