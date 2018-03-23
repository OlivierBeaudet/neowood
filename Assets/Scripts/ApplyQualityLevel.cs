using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class ApplyQualityLevel : MonoBehaviour
{
    [SerializeField]
    private int m_qualityLevel;

    [SerializeField]
    private PostProcessingProfile m_postProcessingProfile;

    private Toggle m_toggle;
    private PostProcessingBehaviour m_postProcess;

    private void Awake()
    {
        m_toggle = GetComponent<Toggle>();
        m_toggle.onValueChanged.AddListener(OnValueChanged);
        m_toggle.isOn = m_qualityLevel == PlayerPrefs.GetInt("QualityLevel", QualitySettings.GetQualityLevel());

        m_postProcess = FindObjectOfType<PostProcessingBehaviour>();
        if(m_postProcess == null)
            Debug.LogWarning("No Post Processing Behaviour found");
    }

    private void OnValueChanged(bool newValue)
    {
        if(newValue)
        {
            QualitySettings.SetQualityLevel(m_qualityLevel);

            if( m_postProcessingProfile != null && m_postProcess != null )
            {
                m_postProcess.profile = m_postProcessingProfile;
            }

            PlayerPrefs.SetInt("QualityLevel", m_qualityLevel);
            PlayerPrefs.Save();
        }
    }
}
