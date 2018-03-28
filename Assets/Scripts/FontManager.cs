using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FontManager : MonoBehaviour
{
    public bool applyOnStart;
    public FontElement[] elements = new FontElement[0];

    [ContextMenu("Apply Now")]
    public void ApplyNow()
    {
        foreach (FontElement fe in elements)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag(fe.name);
            foreach (GameObject go in gos)
            {
                Text text = go.GetComponent<Text>();
                if (text != null)
                {
                    Debug.Log("Apply font: " + text);
                    text.material = fe.material;
                    text.font = fe.font;
                    text.fontSize = 0;
                    text.fontStyle = FontStyle.Normal;
                    text.supportRichText = false;
                }
            }
        }

        if (!Application.isPlaying)
            applyOnStart = false;
    }

    private void Awake()
    {
        if (applyOnStart)
            ApplyNow();
    }
}

[System.Serializable]
public class FontElement
{
    public string name = "";
    public Material material;
    public Font font;
}