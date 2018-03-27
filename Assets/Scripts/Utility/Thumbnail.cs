using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Thumbnail
{
    static private int m_maxHeight = 0;
    static private Rect m_lastRect = new Rect();

    static private Texture2D m_atlas;
    static public Texture2D atlas
    {
        get
        {
            if (m_atlas == null)
            {
                m_atlas = new Texture2D(1024, 1024, TextureFormat.ARGB32, false);
                m_atlas.filterMode = FilterMode.Point;
                m_atlas.Apply();

#if UNITY_EDITOR
                new GameObject("~ATLAS").AddComponent<UnityEngine.UI.RawImage>().texture = m_atlas;
#endif
            }

            return m_atlas;
        }
    }

    static private Dictionary<string, Sprite> m_spriteMap = new Dictionary<string, Sprite>();

    static public void Combine(Texture2D other, Texture2D dest )
    {
        for( int x = 0; x < dest.width; x++ )
        {
            int yMax = Mathf.RoundToInt((float)dest.height * (dest.width - x) / (float)dest.width);
            for( int y = 0; y < yMax; y++ )
            {
                dest.SetPixel(x, y, other.GetPixel(x, y));
            }
        }
    }
    
    private string m_textureName;
    public string textureName
    {
        get { return m_textureName; }
        set
        {
            if(m_textureName != value)
            {
                if (m_sprite != null)
                    Object.Destroy(m_sprite);

                m_textureName = value;
            }
        }
    }

    private Sprite m_sprite;
    public Sprite sprite
    {
        get
        {
            if(m_sprite == null)
            {
                if (m_spriteMap.ContainsKey(m_textureName))
                    m_sprite = m_spriteMap[m_textureName];
                else
                {
                    m_lastRect.x += m_lastRect.width;

                    if (m_lastRect.xMax > atlas.width)
                    {
                        m_lastRect.x = 0;
                        m_lastRect.y += m_maxHeight;
                        m_maxHeight = 0;
                    }

                    m_lastRect.width = m_width;
                    m_lastRect.height = m_height;

                    if (m_height > m_maxHeight)
                        m_maxHeight = m_height;

                    Texture2D original = null;
                    string[] split = m_textureName.Split(',');
                    Texture2D[] clones = new Texture2D[split.Length];
                    for( int i = 0; i < split.Length; i++)
                    {
                        original = Resources.Load<Texture2D>(split[i]);
                        if (original == null)
                        {
                            Debug.LogError("Cannot load texture " + split[i]);
                            return null;
                        }

                        //clones[i] = Object.Instantiate(original) as Texture2D;
                        //TextureScale.Bilinear(clones[i], m_width, m_height);
                        clones[i] = new Texture2D(m_width, m_height, TextureFormat.ARGB32, false);
                        clones[i].SetPixels(0, 0, m_width, m_height, original.GetPixels(0, 0, m_width, m_height));

                        if ( i >= 1 )
                        {
                            Combine(clones[i-1], clones[i]);
                        }
                    }

                    atlas.SetPixels(
                        Mathf.RoundToInt(m_lastRect.x),
                        Mathf.RoundToInt(m_lastRect.y),
                        Mathf.RoundToInt(m_lastRect.width),
                        Mathf.RoundToInt(m_lastRect.height),
                        clones[clones.Length-1].GetPixels()
                    );

                    m_atlas.Apply();

                    m_sprite = Sprite.Create(m_atlas, m_lastRect, Vector2.zero);
                    m_sprite.name = m_textureName + " (thumb)";

                    m_spriteMap.Add(m_textureName, m_sprite);

                    // Debug.Log("Generated sprite: " + m_sprite + " (" + m_lastRect + ")");

                    for (int i = 0; i < clones.Length; i++)
                    {
                        Object.Destroy(clones[i]);
                        clones[i] = null;
                    }

                    Resources.UnloadUnusedAssets();
                }
            }

            return m_sprite;
        }
    }

    private int m_width, m_height;
    public int width { get { return m_width; } }
    public int height { get { return m_height; } }

    public Thumbnail( int w, int h, params string[] textureFiles)
    {
        m_width = w;
        m_height = h;

        m_textureName = "";
        foreach (string file in textureFiles) m_textureName += file + ",";
        m_textureName = m_textureName.TrimEnd(',');
    }
}
