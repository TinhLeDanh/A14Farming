using A14Farming.Buildings;
using A14Farming.Buildings.Data;
using A14Farming.Buildings.Placement;
using A14Farming.Event;
using A14Farming.UI.HUD;
using Core.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using Core.BE;

namespace A14Farming.System
{
    public class UserInfo
    {
        public bool isNew = true;
        public string gold;
        public string extraGold;
        public string gem;
        public string exp;
    }

    [Serializable]
    public class BuildingList
    {
        public List<Detail> m_buildingList;
    }

    [Serializable]
    public class Detail
    {
        public float posX;
        public float posY;
        public float posZ;

        public int gridPosX;
        public int gridPosY;

        public int dimX;
        public int dimY;

        public string buildingName;
    }

    public class GameHandler : Singleton<GameHandler>
    {
        private GameUI gameUI;

        public UserInfo userInfo;
        private List<Building> listBuilding;
        BuildingList buildingList;
        [HideInInspector]
        public Building buildingSelected = null;

        public BuildingLibrary buildingLibrary;
        public StartData startData;

        public TowerPlacementGrid targetArea;

        private Vector3 mousePosOld = Vector3.zero;
        private Vector3 mousePosLast = Vector3.zero;
        [HideInInspector]
        public Plane xzPlane;

        public Transform buildingPool;

        /// <summary>
		/// Awake method to associate singleton with instance
		/// </summary>
		protected override void Awake()
        {
            base.Awake();

            //SaveSystem.Init();
            listBuilding = new List<Building>();
            buildingList = new BuildingList();
            buildingList.m_buildingList = new List<Detail>();

            //Load user information
            LoadUserInfo("1", "user_info");
            if (userInfo.isNew)
                InitUserInfo();
            SaveUserInfo("1", "user_info", userInfo);
        }

        void OnEnable()
        {
            EventController.onSpawnedBuilding += AddBuildingToList;
        }

        void OnDisable()
        {
            EventController.onSpawnedBuilding -= AddBuildingToList;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void Start()
        {
            
        }

        private void InitUserInfo()
        {
            userInfo = new UserInfo();
            userInfo.isNew = false;
            userInfo.gold = startData.gold;
            userInfo.extraGold = startData.extraGold;
            userInfo.gem = startData.gem;
            userInfo.exp = startData.exp;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    //Debug.Log("left-click over a GUI element!");
                    return;
                }
                Pick();
            }
        }

        public void Pick()
        {
            //Debug.Log ("Pick buildingSelected:"+((buildingSelected != null) ? buildingSelected.name : "none"));
            //GameObject goSelectNew = null;
            Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log ("Pick"+hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag == "Ground")
                {

                    BuildingSelect(null);
                    return;
                }
                else if (hit.collider.gameObject.tag == "Building")
                {
                    Building buildingNew = BuildingFromObject(hit.collider.gameObject);
                    if (buildingNew == buildingSelected) return;

                    BuildingSelect(buildingNew);
                    return;
                }
            }
        }

        public void BuildingSelect(Building buildingNew)
        {
            // if user select selected building again
            bool SelectSame = (buildingNew == buildingSelected) ? true : false;
            if (buildingSelected != null)
            {
                UICommand.Hide();
            }

            if (SelectSame)
                return;

            buildingSelected = buildingNew;

            if (buildingSelected != null)
            {
                // set scale animation to newly selected building
                BETween bt = BETween.scale(buildingSelected.gameObject, 0.1f, new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.4f, 1.4f, 1.4f));
                bt.loopStyle = BETweenLoop.pingpong;
            }
        }

        public void BuildingLandUnselect()
        {
            UICommand.Hide();
        }

        public void AddBuildingToList(Building building)
        {
            listBuilding.Add(building);
            Detail buildingDetail = new Detail();
            buildingDetail.posX = building.transform.position.x;
            buildingDetail.posY = building.transform.position.y;
            buildingDetail.posZ = building.transform.position.z;

            buildingDetail.gridPosX = (int)building.gridPosition.x;
            buildingDetail.gridPosY = (int)building.gridPosition.y;

            buildingDetail.dimX = (int)building.dimensions.x;
            buildingDetail.dimY = (int)building.dimensions.y;

            buildingDetail.buildingName = building.buildingName;

            buildingList.m_buildingList.Add(buildingDetail);
            //SaveBuildingList("1", "buildingList", buildingList);
        }

        private void SaveBuildingList(string Id, string file_name, BuildingList bL)
        {
            string json = JsonUtility.ToJson(bL);
            //SaveSystem.Save(Id, file_name, json);
        }

        private void SaveUserInfo(string Id, string file_name, UserInfo uInfo)
        {
            string json = JsonUtility.ToJson(uInfo);
            //SaveSystem.Save(Id, file_name, json);
        }

        private void LoadBuilding(string Id, string file_name)
        {
            string saveString = SaveSystem.Load(Id, file_name);

            if (saveString != null)
            {
                buildingList = JsonUtility.FromJson<BuildingList>(saveString);

                foreach (Detail details in buildingList.m_buildingList)
                {
                    Vector3 pos;
                    pos.x = details.posX;
                    pos.y = details.posY;
                    pos.z = details.posZ;
                    GameObject prefab = buildingLibrary.buildings.First(x => x.buildingName == details.buildingName).prefab;
                    GameObject createdBuilding = Instantiate(prefab, pos, Quaternion.identity);
                    Building b = createdBuilding.GetComponent<Building>();
                    createdBuilding.transform.parent = buildingPool;
                    b.prefab = prefab;
                    b.dimensions.x = details.dimX;
                    b.dimensions.y = details.dimY;

                    IntVector2 destination;
                    destination.x = details.gridPosX;
                    destination.y = details.gridPosY;
                    //targetArea.Occupy(destination, b.dimensions);
                }
            }
            else
            {
            }
        }

        private void LoadUserInfo(string Id, string file_name)
        {
            string saveString = SaveSystem.Load(Id, file_name);

            if (saveString != null)
            {
                userInfo = JsonUtility.FromJson<UserInfo>(saveString);
            }
            else
            {
                InitUserInfo();
            }
        }

        // get building script
        // if child object was hitted, check parent 
        public Building BuildingFromObject(GameObject go)
        {
            Building buildingNew = go.GetComponent<Building>();
            if (buildingNew == null) buildingNew = go.transform.parent.gameObject.GetComponent<Building>();

            return buildingNew;
        }
    }
}

