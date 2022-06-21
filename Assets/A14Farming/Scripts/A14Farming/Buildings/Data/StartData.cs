using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A14Farming.Buildings.Data
{
    [CreateAssetMenu(fileName = "BuildingLibrary.asset", menuName = "A14Farming/Building start data", order = 1)]
    public class StartData : ScriptableObject
    {
        public string gold;
        public string extraGold;
        public string gem;
        public string exp;
    }
}

