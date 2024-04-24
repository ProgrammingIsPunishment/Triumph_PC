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

    public Effect(string guid, string displayname, string description, string iconFileName, EffectType effectType, bool isPositiveEffect, float value)
    {
        this.GUID = guid;
        this.DisplayName = displayname;
        this.Description = description;
        this.IconFileName = iconFileName;
        this.EffectType = effectType;
        this.IsPositiveEffect = isPositiveEffect;
        this.Value = value;
    }

    public Effect CreateInstance()
    {
        return new Effect(this.GUID,this.DisplayName,this.Description, this.IconFileName, this.EffectType,this.IsPositiveEffect,this.Value);
    }

    public float ProcessValue(float valueToProcess)
    {
        if (this.IsPositiveEffect)
        {
            valueToProcess += this.Value;
        }
        else
        {
            valueToProcess -= this.Value;
        }

        return valueToProcess;
    }
}
