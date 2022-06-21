using A14Farming.Buildings;
using A14Farming.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace A14Farming.UI.HUD
{
    // command types
    public enum CommandType
    {
        None,
        Create,
        CreateCancel,
        Info,
        Upgrade,
        UpgradeCancel,
        UpgradeFinish,
        Boost,
        BoostAll,
        Training,
        Research,
    }

    public class UICommand : MonoBehaviour
    {
        private static UICommand instance;

        public Sprite infoSprite;
        private Building building = null;
        public GameObject prefButton;
        public Text Info;
        private List<CommandButton> Buttons = new List<CommandButton>();

        public static bool Visible = false;

        public static void Show(Building script) { instance._Show(script); }
        public static void Hide() { instance._Hide(); }

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            Visible = false;
        }

        void Update()
        {
            if ((GameHandler.instance.buildingSelected != null) && !UICommand.Visible)
            {
                UICommand.Show(GameHandler.instance.buildingSelected);
            }
            else if ((GameHandler.instance.buildingSelected == null) && UICommand.Visible)
            {
                UICommand.Hide();
            }

            if (Visible)
            {
                for (int i = 0; i < Buttons.Count; ++i)
                    Buttons[i].Update();
            }
        }

        public void Reset()
        {
            for (int i = 0; i < Buttons.Count; ++i)
            {
                Destroy(Buttons[i].go);
            }
            Buttons.Clear();
        }

        // add button to command dialog
        public CommandButton AddButton(Sprite symbol, string text, int buyPrice, CommandType ct)
        {
            GameObject go = (GameObject)Instantiate(prefButton, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(transform);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = new Vector3(0, 50, 0);

            CommandButton newButton = go.GetComponent<CommandButton>();
            Buttons.Add(newButton);

            //CommandType tempCT = ct;
            //Button button = go.GetComponent<Button>();
            //button.onClick.RemoveAllListeners();
            //button.onClick.AddListener(() => { ButtonClicked(tempCT); });

            //return newButton;
            return null;
        }

        public void _Hide()
        {
            //BETween.anchoredPosition3D(gameObject, 0.2f, new Vector3(0,0,0), new Vector3(0,-250,0));//.method = BETweenMethod.easeOut;
            //StartCoroutine(TweenMov(GetComponent<RectTransform>(), new Vector2(0, 0), new Vector2(0, -250), 0.2f, 0));
            //Visible = false;
        }

        public void _Show(Building script)
        {
            building = script;
            Reset();
            Visible = true;

            Info.text = building.buildingName.ToString();

            // if building is not in creation, show 'info' button
            AddButton(infoSprite, "Info", 0, CommandType.Info);

            // if in upgrading
            if (building.InUpgrade)
            {
                if (!building.UpgradeCompleted)
                {
                    // shows 'Cancel','Finish' buttons
                    //AddButton(Resources.Load<Sprite>("Icons/Multiplication"), "Cancel", 0, CommandType.UpgradeCancel);
                }
            }
            else
            {
                // add 'upgrade' button
                //AddButton(Resources.Load<Sprite>("Icons/UpArrow"), "Upgrade", 0, CommandType.Upgrade);
            }

            StartCoroutine(TweenMov(GetComponent<RectTransform>(), new Vector2(0, -250), new Vector2(0, 0), 0.2f, 0));

        }

        public IEnumerator TweenMov(RectTransform tr, Vector2 Start, Vector2 End, float time, float fDelay)
        {

            if (fDelay > 0.01f)
                yield return new WaitForSeconds(fDelay);

            float fAge = 0.0f;
            Vector2 vPos = Start;
            bool Completed = false;
            while (true)
            {
                tr.anchoredPosition = vPos;
                if (Completed)
                {

                    break;
                }

                yield return new WaitForSeconds(0.03f);

                fAge += 0.03f;
                float fRatio = fAge / time;
                vPos = Vector2.Lerp(Start, End, fRatio);
                if (fRatio > 1.0f)
                {
                    Completed = true;
                    vPos = End;
                }
            }
        }
    }
}
