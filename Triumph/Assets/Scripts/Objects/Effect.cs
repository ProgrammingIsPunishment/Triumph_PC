using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public string IconFileName { get; set; }
    public EffectType EffectType { get; set; }
    public bool IsPositiveEffect { get; set; }
    public float Value { get; set; }
    public List<Tuple<EffectType, float>> Values { get; set; }

    public Effect(string guid, string displayname, string description, List<Tuple<EffectType,float>> values)
    {
        this.GUID = guid;
        this.DisplayName = displayname;
        this.Description = description;
        //this.IconFileName = iconFileName;
        //this.EffectType = effectType;
        //this.IsPositiveEffect = isPositiveEffect;
        //this.Value = value;
        this.Values = values;
    }

    public Effect CreateInstance()
    {
        //return new Effect(this.GUID,this.DisplayName,this.Description, this.IconFileName, this.EffectType,this.IsPositiveEffect,this.Value);
        return new Effect(this.GUID, this.DisplayName, this.Description, this.Values);
    }

    public float ProcessEffect(float valueToProcess, float value)
    {
        return valueToProcess += value;
    }
}
