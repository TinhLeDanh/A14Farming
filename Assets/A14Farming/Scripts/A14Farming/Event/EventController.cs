using A14Farming.Buildings;
using System;

namespace A14Farming.Event
{
    public static class EventController
    {
        public static event Action<Building> onSpawnedBuilding;

        public static void CallSpawnedBuilding(Building building)
        {
            onSpawnedBuilding?.Invoke(building);
        }
    }
}

