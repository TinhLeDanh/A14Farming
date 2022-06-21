using UnityEngine;

namespace A14Farming.Buildings.Data
{
    [CreateAssetMenu(fileName = "BuildingLibrary.asset", menuName = "A14Farming/Building data", order = 1)]
    public class BuildingLevelData : ScriptableObject
    {
        public string description;
        public string upgradeDescription;
        public int cost;
        public int sell;
        public double buildTime;
        public int buildCount;
        public Sprite icon;
    }
}
