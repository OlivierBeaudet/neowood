using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraViewport : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private FreeLookCamera m_cameraControl;

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_cameraControl.hasFocus = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_cameraControl.hasFocus = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_cameraControl.hasFocus = false;
    }

    private void Awake()
    {
        if (m_cameraControl == null)
            m_cameraControl = FindObjectOfType<FreeLookCamera>();
    }
}
