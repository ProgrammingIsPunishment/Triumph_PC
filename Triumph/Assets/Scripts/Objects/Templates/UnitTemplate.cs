using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTemplate
{
    public string GUID { get; set; }
    public string Name { get; set; }
    public string Suffix { get; set; }
    public string Iteration { get; set; }
    public string ModelName { get; set; }

    public UnitTemplate(string guid, string name, string suffix, string iteration, string modelName)
    {
        this.GUID = guid;
        this.Name = name;
        this.Suffix = suffix;
        this.Iteration = iteration;
        this.ModelName = modelName;
    }
}
