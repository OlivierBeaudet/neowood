using UnityEngine;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

public static class StringUtils
{
    static public bool TryParsePercent( string strResult, out float result )
    {
        strResult = strResult.Trim('%');

        float floatResult = 0;
        if (float.TryParse(strResult, out floatResult))
        {
            result = floatResult / 100f;
            return true;
        }
        else
        {
            result = 0f;
            return false;
        }
    }

    static public string Vector3ToString(Vector3 v, int precision = 2)
    {
        return v.x.ToString("N" + precision) + ";" + v.y.ToString("N" + precision) + ";" + v.z.ToString("N" + precision);
    }

    static public Vector3 StringToVector3(string str)
    {
        Vector3 result = Vector3.zero;
        string[] split = str.Split(';');
        if (split.Length > 0) float.TryParse(split[0], out result.x);
        if (split.Length > 1) float.TryParse(split[1], out result.y);
        if (split.Length > 2) float.TryParse(split[2], out result.z);
        return result;
    }

    static public string Vector2ToString(Vector2 v, int precision = 2)
    {
        return v.x.ToString("N" + precision) + ";" + v.y.ToString("N" + precision);
    }

    static public Vector2 StringToVector2(string str)
    {
        Vector2 result = Vector2.zero;
        string[] split = str.Split(';');
        if (split.Length > 0) float.TryParse(split[0], out result.x);
        if (split.Length > 1) float.TryParse(split[1], out result.y);
        return result;
    }

    static public string ArrayToString( params object[] array )
    {
        string str = "";
        for (int i = 0; i < array.Length; i++) str += array[i] != null ? array[i].ToString() : "null";
        return str;
    }

    

    static public bool TryParseColor(string hex, out Color color)
    {
        byte r, g, b;
        if (!string.IsNullOrEmpty(hex)
        && byte.TryParse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, null, out r)
        && byte.TryParse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, null, out g)
        && byte.TryParse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber, null, out b))
        {
            color = new Color(r, g, b);
            return true;
        }
        else
        {
            color = Color.clear;
            return false;
        }
    }

    static public Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Length >= 2 ? hex.Substring(0, 2) : "00", System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Length >= 4 ? hex.Substring(2, 2) : "00", System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Length >= 6 ? hex.Substring(4, 2) : "00", System.Globalization.NumberStyles.HexNumber);
        byte a = byte.Parse(hex.Length >= 8 ? hex.Substring(6, 2) : "FF", System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, a);
    }

    static public string ColorToHex(Color color)
    {
        int r = (int)(color.r * 255);
        int g = (int)(color.g * 255);
        int b = (int)(color.b * 255);
        string hex = r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
        return hex;
    }

    static public string ColorToHex(Color32 color)
    {
        int r = (int)(color.r * 255);
        int g = (int)(color.g * 255);
        int b = (int)(color.b * 255);
        int a = (int)(color.a * 255);
        string hex = r.ToString("X2") + g.ToString("X2") + b.ToString("X2") + a.ToString("X2");
        return hex;
    }

    public static string CropText(string str, int maxChars, string suffix = " (...)")
    {
        return str.Length <= maxChars || maxChars < 0 ? str : str.Substring(0, maxChars - suffix.Length) + suffix;
    }

    public static string AddZeros(int number, int digitCount)
    {
        string str = number.ToString();
        for( int z = digitCount-1; z > 0; z-- )
        {
            if (number < z * 10)
                str = "0" + str;
        }
        return str;
    }

    public static string RemoveSpecialCharacters(this string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if (c >= 32 && c != 127)
                sb.Append(c);
        }
        return sb.ToString();
    }

    static public string RemoveAtEnd(string str, string end)
    {
        return str.EndsWith(end) ? str.Remove(str.Length - end.Length, end.Length) : str;
    }

    static public string RemoveAtStart(string str, string start)
    {
        return str.StartsWith(start) ? str.Remove(0, start.Length) : str;
    }

    static public string GetParentDirectory(string str, int count = 1, char splitChar = '/')
    {
        string[] split = str.Split(splitChar);

        str = "";
        for( int i = 0; i < Mathf.Clamp( split.Length - count, 0, split.Length - 1 ); i++ )
        {
            str += split[i] + splitChar;
        }

        return str.Trim('/');
    }
}
