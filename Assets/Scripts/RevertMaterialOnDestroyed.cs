using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RevertMaterialOnDestroyed : MonoBehaviour
{
    static private List<Material> m_savedMaterials = new List<Material>();

    static public void Save( Material material )
    {
        if (m_savedMaterials.Contains(material))
            return;

        m_savedMaterials.Add(material);
        
        RevertMaterialOnDestroyed revertMaterial = new GameObject("~"+material.name).AddComponent<RevertMaterialOnDestroyed>();
        revertMaterial.m_material = material;
        revertMaterial.m_savedTexture = material.mainTexture;
        revertMaterial.m_savedScale = material.mainTextureScale;
        revertMaterial.m_savedOffset = material.mainTextureOffset;
        revertMaterial.m_savedColor = material.color;
    }

    private Material m_material;
    private Texture m_savedTexture;
    private Color m_savedColor;
    private Vector2 m_savedScale, m_savedOffset;

    private void OnDestroy()
    {
        m_material.mainTexture = m_savedTexture;
        m_material.mainTextureScale = m_savedScale;
        m_material.mainTextureOffset = m_savedOffset;
        m_material.color = m_savedColor;
    }
}
