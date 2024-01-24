using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute
{
    public string GUID { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public string IconFileName { get; set; }
    public AttributeType AttributeType { get; set; }
    public float Value { get; set; }

    public Attribute(string guid, string displayname, string description, string iconFileName, AttributeType attributeType, float value)
    {
        this.GUID = guid;
        this.DisplayName = displayname;
        this.Description = description;
        this.IconFileName = iconFileName;
        this.AttributeType = attributeType;
        this.Value = value;
    }

    public Attribute CreateInstance()
    {
        return new Attribute(this.GUID, this.DisplayName, this.Description, this.IconFileName, this.AttributeType, this.Value);
    }
}
