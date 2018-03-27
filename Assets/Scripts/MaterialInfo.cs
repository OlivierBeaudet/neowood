﻿using UnityEngine;
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

        m_image.sprite = variation.thumbnail.sprite;

        Resources.UnloadUnusedAssets();
    }
}
