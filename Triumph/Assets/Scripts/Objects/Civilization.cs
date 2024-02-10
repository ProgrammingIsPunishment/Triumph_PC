using System;
using System.Collections.Generic;

[Serializable]
public class Civilization
{
    public string GUID { get; set; }
    public string Name { get; set; }
    public List<InfluentialPerson> InfluentialPeople { get; set; }
    public List<Unit> Units { get; set; }
    public List<Holding> ExploredHoldings { get; set; }
    public List<Holding> TerritorialHoldings { get; set; }
    public InfluentialPerson Leader { get; set; }
    public List<Supply> Supplies { get; set; }
    public List<GoodsTemplate> GoodsTemplates { get; set; }
    public int PoliticalPower { get; set; }

    public Civilization(string guid, string name)
    {
        this.GUID = guid;
        this.Name = name;
        this.InfluentialPeople = new List<InfluentialPerson>();
        this.Units = new List<Unit>();
        this.ExploredHoldings = new List<Holding>();
        this.TerritorialHoldings = new List<Holding>();
        this.Leader = null;
        this.Supplies = new List<Supply>();
        this.GoodsTemplates = new List<GoodsTemplate>();
        this.PoliticalPower = 5;
    }

    public void ReplenishPoliticalPower()
    {
        int defaultValue = this.PerTurnPoliticalPowerGain();
        int ongoingCost = this.PerTurnPolticalPowerLoss();

        defaultValue -= ongoingCost;

        this.PoliticalPower = defaultValue;
    }

    public void UsePoliticalPower(int cost)
    {
        this.PoliticalPower -= cost;
    }

    public bool OwnsHolding(Holding holding)
    {
        return this.TerritorialHoldings.Contains(holding);
    }

    public bool HasPoliticalPower()
    {
        return this.PoliticalPower >= 1;
    }

    public int PerTurnPolticalPowerLoss()
    {
        int result = 0;

        result += this.TerritorialHoldings.Count;

        return result;
    }

    public int PerTurnPoliticalPowerGain()
    {
        int result = 0;

        result = 5;

        return result;
    }
}
