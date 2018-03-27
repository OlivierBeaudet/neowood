using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleTextColor : MonoBehaviour
{
    private Toggle m_toggle;

    [SerializeField]
    private Text m_label;

    [SerializeField]
    private Color m_colorOn = Color.white;

    [SerializeField]
    private Color m_colorOff = Color.black;

    private void Awake()
    {
        m_toggle = GetComponent<Toggle>();
        OnValueChanged(m_toggle.isOn);

        m_toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDestroy()
    {
        m_toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(bool newValue)
    {
        m_label.color = newValue ? m_colorOn : m_colorOff;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(!Application.isPlaying )
            OnValueChanged(GetComponent<Toggle>().isOn);
    }
#endif
}
