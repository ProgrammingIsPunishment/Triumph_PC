using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Season
{
    public string Name { get; set; }
    public int Order { get; set; }
    public string IconFileName { get; set; }
    public int Length { get; set; }
    public int DaysLeft { get; set; }

    public Season(string name, int order, string iconFileName, int length)
    {
        this.Name = name;
        this.Order = order;
        this.IconFileName = iconFileName;
        this.Length = length;
        this.DaysLeft = length;
    }

    public void ResetDaysLeft()
    {
        this.DaysLeft = Length;
    }
}
