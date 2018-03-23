using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MaterialSelection : MonoBehaviour
{
    [SerializeField]
    private List<MaterialVariation> m_variations = new List<MaterialVariation>();

    [SerializeField]
    private RectTransform m_item;

    private void Awake()
    {
        m_item.GetComponent<Toggle>().isOn = false;

        foreach (MaterialVariation variation in m_variations)
        {
            RectTransform item = Instantiate(m_item.gameObject).GetComponent<RectTransform>();
            item.gameObject.SetActive(true);
            item.name = item.name.Replace("Clone", variation.name);
            item.SetParent(m_item.parent, false);
            item.SetSiblingIndex(m_item.GetSiblingIndex() + 1);

            ApplyMaterialVariation apm = item.gameObject.AddComponent<ApplyMaterialVariation>();
            apm.variation = variation;

            Toggle toggle = item.gameObject.GetComponent<Toggle>();
            (toggle.targetGraphic as Image).sprite = variation.thumbnail.sprite;
        }

        m_item.gameObject.SetActive(false);
    }
}
