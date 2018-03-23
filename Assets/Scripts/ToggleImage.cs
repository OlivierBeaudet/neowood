using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleImage : MonoBehaviour
{
    private Toggle m_toggle;
    private Sprite m_defaultSprite;

    [SerializeField]
    private Sprite m_spriteOn;

    private void Awake()
    {
        m_toggle = GetComponent<Toggle>();

        if( m_toggle.isOn )
        {
            m_defaultSprite = m_spriteOn;
            m_spriteOn = (m_toggle.targetGraphic as Image).sprite;
        }
        else
        {
            m_defaultSprite = (m_toggle.targetGraphic as Image).sprite;
        }

        m_toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDestroy()
    {
        m_toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(bool newValue)
    {
        (m_toggle.targetGraphic as Image).sprite = newValue ? m_spriteOn : m_defaultSprite;
    }
}
