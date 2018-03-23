using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FreeLookCamera : MonoBehaviour
{
    [SerializeField]
    private float m_range = 1f;

    [SerializeField]
    private Ease m_easeType = Ease.InOutCirc;

    [SerializeField]
    private float m_tweenDuration = 2f;

    private Quaternion m_baseRotation;
    private Vector3 m_offset, m_lookAt;
    private Vector2 m_mousePos, m_mouseDelta;

    private bool m_hasFocus;
    public bool hasFocus
    {
        get { return m_hasFocus; }
        set { m_hasFocus = value; }
    }

    private Camera m_camera;
    private GameObject m_lastTarget;

    public void TweenTo(string targetName)
    {
        TweenTo(GameObject.Find(targetName));
    }

    public void TweenTo(GameObject newTarget)
    {
        if (m_lastTarget == newTarget)
            return;

        transform.DOKill();
        if (newTarget == null)
            return;

        transform.DOMove(newTarget.transform.position, m_tweenDuration).SetEase(m_easeType);
        transform.DORotate(newTarget.transform.eulerAngles, m_tweenDuration).SetEase(m_easeType);

        Camera otherCam = newTarget.GetComponent<Camera>();
        if( otherCam != null )
        {
            m_camera.DOFieldOfView(otherCam.fieldOfView, m_tweenDuration).SetEase(m_easeType);
        }

        m_baseRotation = newTarget.transform.rotation;
        m_lastTarget = newTarget;
    }

    private void Awake()
    {
        m_camera = GetComponent<Camera>();
    }

    private void Start()
    {
        m_baseRotation = transform.rotation;
    }

    private void Update()
    {
        if (m_hasFocus)
        {
            m_offset.x += (m_mousePos.x - Input.mousePosition.x) / Screen.width;
            m_offset.y += (m_mousePos.y - Input.mousePosition.y) / Screen.height;

            m_offset.z = 1f;
            m_offset.x = Mathf.Clamp(m_offset.x, -m_range, m_range);
            m_offset.y = Mathf.Clamp(m_offset.y, -m_range, m_range);

            m_lookAt = transform.position + m_baseRotation * m_offset;

            transform.LookAt(m_lookAt);
        }

        m_mousePos = Input.mousePosition;
    }
}
