using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MaterialInfo : MonoBehaviour
{
    [SerializeField]
    private Text m_label;

    [SerializeField]
    private Image m_image;

    private Texture2D m_texture;

    public void SetVariation( MaterialVariation variation )
    {
        m_label.text = variation.label;

        //m_texture = variation.LoadTexture();
        //m_image.sprite = Sprite.Create(m_texture, new Rect(0, 0, m_texture.width, m_texture.height), Vector2.zero);
        m_image.sprite = variation.thumbnail.sprite;

        Resources.UnloadUnusedAssets();
    }
}
