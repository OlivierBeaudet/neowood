using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class UIUtils
{
    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        Rect rect = new Rect(transform.position.x, Screen.height - transform.position.y, size.x, size.y);
        rect.x -= (transform.pivot.x * size.x);
        rect.y -= ((1.0f - transform.pivot.y) * size.y);
        return rect;
    }

    static private Dictionary<string, Vector2> m_scrollValue = new Dictionary<string, Vector2>();
    static public Vector2 GetScrollValue(string key)
    {
        if (!m_scrollValue.ContainsKey(key)) m_scrollValue.Add(key, Vector2.zero);
        return m_scrollValue[key];
    }

    static public void SetScrollValue(string key,Vector2 value)
    {
        if (!m_scrollValue.ContainsKey(key)) m_scrollValue.Add(key, value);
        else m_scrollValue[key] = value;
    }
}
