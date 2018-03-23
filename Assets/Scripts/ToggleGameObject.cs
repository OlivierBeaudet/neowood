using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleGameObject : MonoBehaviour
{
    private Toggle m_toggle;
    private Sprite m_defaultSprite;

    [SerializeField]
    private GameObject m_target;

    private void Awake()
    {
        m_toggle = GetComponent<Toggle>();
        m_toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDestroy()
    {
        m_toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void Start()
    {
        m_target.SetActive(m_toggle.isOn);
    }

    private void OnValueChanged(bool newValue)
    {
        m_target.SetActive(newValue);
    }
}
