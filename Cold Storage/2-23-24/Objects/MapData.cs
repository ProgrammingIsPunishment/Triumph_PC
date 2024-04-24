using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData
{
   public List<ResourceItem> AllResourceItems { get; set; }
   public List<Holding> AllHoldings { get; set; }
   public List<Civilization> AllCivilizations { get; set; }
   public List<Unit> AllUnits { get; set; }
   public List<Building> AllBuildings { get; set; }
   public List<Effect> AllEffects { get; set; }
   public List<Attribute> AllAttributes { get; set; }
   public List<Season> AllSeasons { get; set; }
}
