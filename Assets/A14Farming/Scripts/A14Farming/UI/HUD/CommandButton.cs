using A14Farming.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace A14Farming.UI.HUD
{
    public class CommandButton : MonoBehaviour
    {
        //private static Color	colorShow = Color.black;
        //private static Color	colorNotShow = new Color(0,0,0,0);
        [HideInInspector]
        public GameObject go = null;
        public Image Background;
        public Text Title;
        public Image Icon;
        public Text Price;
        public Image PriceIcon;
        [HideInInspector]
        public Sprite Symbol;
        public UnityAction call = null;
        public bool Disabled = false;
        public bool Locked = false;

        //public PayType payType = PayType.Gold;
        public int BuyPrice = 0;

        public void Awake()
        {
            //Symbol = _symbol;
            //Title.text = _text;
            //call = _call;
            //Disabled = false;
            //Locked = false;

            ////payType = _payType;
            //BuyPrice = _buyPrice;

            //Icon.overrideSprite = Symbol;

            Price.text = (BuyPrice == 0) ? "" : BuyPrice.ToString("#,##0");
            //PriceIcon.overrideSprite = TBDatabase.GetPayTypeIcon(_payType);
            PriceIcon.gameObject.SetActive((BuyPrice == 0) ? false : true);
            //PriceIcon.gameObject.SetActive (false);
            //Icon.sprite = GameHandler.instance.buildingSelected.icon.sprite;

            State(false);
        }

        public void State(bool Selected)
        {
            if (!Selected)
            {
                //Background.color = Color.white;
                //Icon.color = colorShow;
                //Icon2.color = colorNotShow;
                //Text.color = clrText;
            }
            else
            {
                //Background.color = Color.green;
                //Icon.color = colorNotShow;
                //Icon2.color = colorShow;
                //Text.color = clrText;
            }
        }

        public void Update()
        {
            //if (payType == PayType.Gold) Price.color = ((int)SceneTown.Gold.Target() < BuyPrice) ? Color.red : Color.white;
            //else if (payType == PayType.Elixir) Price.color = ((int)SceneTown.Elixir.Target() < BuyPrice) ? Color.red : Color.white;
            //else if (payType == PayType.Gem) Price.color = ((int)SceneTown.Gem.Target() < BuyPrice) ? Color.red : Color.white;
            //else { }
        }
    }
}

