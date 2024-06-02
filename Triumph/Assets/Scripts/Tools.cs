using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System;
using System.Linq;

public static class Tools
{
    public static Color ColorFromHex(string hex)
    {
        var r = hex.Substring(0, 2);
        var g = hex.Substring(2, 2);
        var b = hex.Substring(4, 2);
        string alpha;
        if (hex.Length >= 8) { alpha = hex.Substring(6, 2); }
        else { alpha = "FF"; }

        return new Color((int.Parse(r, NumberStyles.HexNumber) / 255f),
                        (int.Parse(g, NumberStyles.HexNumber) / 255f),
                        (int.Parse(b, NumberStyles.HexNumber) / 255f),
                        (int.Parse(alpha, NumberStyles.HexNumber) / 255f));
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        System.Random rnd = new System.Random();
        return source.OrderBy<T, int>((item) => rnd.Next());
    }

    public static Color RandomColor()
    {
        System.Random rnd = new System.Random();
        Color randomColor = new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble(), .7f);

        UnityEngine.Debug.Log(randomColor.ToString());

        return randomColor;
    }
}
