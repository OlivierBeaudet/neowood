using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using DG.Tweening;

public class AnimateRectTransform : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private RectTransform[] m_targets = new RectTransform[0];

    [SerializeField]
    private Vector3 m_translation = Vector3.zero;

    [SerializeField]
    private float m_duration = 1f;

    [SerializeField]
    private Ease m_easeType = Ease.InOutQuad;

    private float m_sens = 1;

    public void Animate()
    {
        foreach( RectTransform target in m_targets )
        {
            target.DOKill(true);
            target.DOMove(target.position + m_translation * m_sens, m_duration).SetEase(m_easeType);
        }

        m_sens *= -1f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Animate();
    }
}
