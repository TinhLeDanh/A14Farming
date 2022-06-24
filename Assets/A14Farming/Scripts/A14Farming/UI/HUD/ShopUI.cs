using A14Farming.Buildings;
using A14Farming.Buildings.Data;
using A14Farming.System;
using Core.BE;
using Core.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace A14Farming.UI.HUD
{
    public enum ShopType
    {
        Normal = 0, // incase general buildings
        InApp = 1,  // inapp item like gem
        House = 2,  // house only (worker)
    }
    public class ShopUI : Singleton<ShopUI>
    {
        public GameObject prefabShopItem;

        public RectTransform rtDialog;
        public GameObject[] contents;
        public Toggle[] toggleButtons;
        private ShopType shopType = ShopType.Normal;
        protected override void Awake()
        {
            base.Awake();

            gameObject.SetActive(false);
        }

        public void Hide()
        {
            BETween.anchoredPosition(rtDialog.gameObject, 0.3f, new Vector3(0, -500)).method = BETweenMethod.easeOut;
            BETween.alpha(gameObject, 0.3f, 0.5f, 0.0f).method = BETweenMethod.easeOut;
            BETween.enable(gameObject, 0.01f, false).delay = 0.4f;
        }

        public void Show(ShopType type)
        {
            // if shop dialog called while new building is in creation,
            // delete new building
            if (GameUI.instance.isBuilding)
            {
                GameUI.instance.CancelGhostPlacement();
            }

            gameObject.transform.localPosition = Vector3.zero;
            gameObject.SetActive(true);
            gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            shopType = type;

            // delete old items of each content
            for (int i = 0; i < contents.Length; ++i)
            {
                for (int j = contents[i].transform.childCount - 1; j >= 0; j--)
                {
                    Destroy(contents[i].transform.GetChild(j).gameObject);
                }
            }

            // create shop items of each contents
            if(shopType == ShopType.Normal)
            {
                FillContents(0, GameHandler.instance.buildingLibrary.buildings);
            }

            toggleButtons[0].isOn = true;
            rtDialog.anchoredPosition = new Vector3(0, 0);
            BETween.alpha(gameObject, 0.3f, 0, 0.5f).method = BETweenMethod.easeOut;
        }

        // incase shop item is building
        public void FillContents(int id, List<Building> list)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                ShopItemUI shopbutton = AddShopItem(prefabShopItem, id, i);
                shopbutton.Init(i, list[i]);
                shopbutton.buttonTapped += OnButtonTapped;
            }

            contents[id].GetComponent<RectTransform>().sizeDelta = new Vector3(320 * list.Count, 440);
        }

        // add shop item with position and animation
        public ShopItemUI AddShopItem(GameObject prefab, int id, int i)
        {
            GameObject go = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(contents[id].transform);
            go.transform.localScale = Vector3.one;
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(i * 320 + 150 + 10, -200);

            ShopItemUI script = go.GetComponent<ShopItemUI>();

            go.GetComponent<CanvasGroup>().alpha = 0;

            BETween bt1 = BETween.alpha(go, 0.1f, 0.0f, 1.0f);
            bt1.delay = 0.1f * (float)i + 0.2f;

            BETween bt2 = BETween.scale(go, 0.2f, Vector3.one, new Vector3(1.1f, 1.1f, 1.1f));
            bt2.delay = bt1.delay;
            bt2.loopStyle = BETweenLoop.pingpong;

            return script;
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
    }
}

