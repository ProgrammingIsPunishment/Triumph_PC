using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

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
}
