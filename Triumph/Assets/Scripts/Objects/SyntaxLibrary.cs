using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SyntaxLibrary
{
    private Dictionary<string, string> validHoldings = new Dictionary<string, string>();
    private Dictionary<string, string> validUnits = new Dictionary<string, string>();
    private List<Tuple<Regex, string[]>> validSyntax = new List<Tuple<Regex, string[]>>();

    public SyntaxLibrary(Dictionary<string, string> validHolding, Dictionary<string, string> validUnit, List<Tuple<Regex, string[]>> validSyntax)
    {
        this.validHoldings = validHolding;
        this.validUnits = validUnit;
        this.validSyntax = validSyntax;
    }

    public void AddUnit(Unit unit)
    {
        this.validUnits.Add(unit.Name.ToUpper(), unit.GUID.ToUpper());
    }

    public bool IsHoldingMatch(string text)
    {
        if (this.validHoldings.ContainsKey(text.ToUpper()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsUnitMatch(string text)
    {
        if (this.validUnits.ContainsKey(text.ToUpper()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Tuple<bool, string[]> IsCommandMatch(string text)
    {
        foreach (Tuple<Regex, string[]> tv in this.validSyntax)
        {
            if (tv.Item1.IsMatch(text.ToUpper()))
            {
                return new Tuple<bool, string[]> (true,tv.Item2);
            }
        }

        return new Tuple<bool, string[]>(false, null);
    }
}
