using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "asset", menuName = "Hall 88/Material Variation", order = 0)]
public class MaterialVariation : ScriptableObject
{
    static public readonly string DEFAULT_TEXTURE_FOLDER = "textures/";

    static private Dictionary<Material, List<Renderer>> m_rendererMap = new Dictionary<Material, List<Renderer>>();

    static public List<Renderer> GetRenderers(Material material)
    {
        if(!m_rendererMap.ContainsKey(material))
        {
            m_rendererMap.Add(material, new List<Renderer>(FindObjectsOfType<Renderer>().Where(r=>r.sharedMaterials.Contains(material))));
        }

        return m_rendererMap[material];
    }

    [System.Serializable]
    public class Item
    {
        public Material material = null;
        public string textureName = "";
        public Vector2 textureScale = Vector2.one;

        public void Apply()
        {
            if (material == null)
            {
                Debug.LogWarning("No material found for item " + textureName);
                return;
            }

            RevertMaterialOnDestroyed.Save(material);

            if( !string.IsNullOrEmpty(textureName) )
            {
                ToggleRenderer(true);
                material.mainTexture = Resources.Load<Texture>(DEFAULT_TEXTURE_FOLDER + textureName);
                material.mainTextureScale = textureScale;
            }
            else
            {
                ToggleRenderer(false);
            }
        }

        public void ToggleRenderer(bool value)
        {
            if (material == null)
                return;

            foreach (Renderer r in GetRenderers(material)) r.enabled = value;
        }
    }

    [Multiline]
    public string label = "";

    public int thumbnailIndex = 0;

    public List<Item> items = new List<Item>();

    private Thumbnail m_thumbnail;
    public Thumbnail thumbnail
    {
        get
        {
            if (m_thumbnail == null)
            {
                List<string> textureList = new List<string>();
                foreach( Item item in items )
                {
                    if (    !string.IsNullOrEmpty(item.textureName)
                        &&  !textureList.Contains(DEFAULT_TEXTURE_FOLDER + item.textureName))
                        textureList.Add(DEFAULT_TEXTURE_FOLDER + item.textureName);
                }

                m_thumbnail = new Thumbnail(256, 80, textureList.ToArray());
            }

            return m_thumbnail;
        }
    }

    public Texture2D LoadTexture()
    {
        return Resources.Load<Texture2D>(DEFAULT_TEXTURE_FOLDER + items[thumbnailIndex].textureName);
    }

    public void Apply()
    {
        foreach (Item item in items)
        {
            item.Apply();

/*#if UNITY_EDITOR
            if (items.IndexOf(item) == thumbnailIndex) UnityEditor.Selection.activeObject = item.material;
#endif*/
        }
    }
}
