using A14Farming.UI.HUD;
using Core.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace A14Farming.UI
{
    public class UIManager : Singleton<UIManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {

        }

        public void OnButtonShop()
        {
            ShopUI.instance.Show(ShopType.Normal);
        }
    }
}

