using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ApplyMaterialVariation : MonoBehaviour
{
    private Toggle m_toggle;
    private MaterialInfo m_info;

    public MaterialVariation variation;

    private void Awake()
    {
        m_info = FindObjectOfType<MaterialInfo>();

        m_toggle = GetComponent<Toggle>();
        m_toggle.group = GetComponentInParent<ToggleGroup>();
        m_toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private IEnumerator Start()
    {
        m_toggle.isOn = false;
        m_toggle.isOn = LoadVariation();

        yield return null;

        m_toggle.isOn = !m_toggle.group.AnyTogglesOn();
    }

    private void OnValueChanged(bool newValue)
    {
        if (!newValue || variation == null )
            return;

        m_info.SetVariation(variation);
        variation.Apply();

        SaveVariation();
    }

    private bool LoadVariation()
    {
        return variation != null && PlayerPrefs.GetInt("Variation:" + m_toggle.group.GetInstanceID(), 0) == variation.GetInstanceID();
    }

    private void SaveVariation()
    {
        PlayerPrefs.SetInt("Variation:" + m_toggle.group.GetInstanceID(), variation.GetInstanceID());
        PlayerPrefs.Save();
    }
}
