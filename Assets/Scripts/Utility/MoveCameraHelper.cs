using UnityEngine;
using System.Collections;

public class MoveCameraHelper : MonoBehaviour
{
#if UNITY_EDITOR
    static private Camera m_sceneCamera;

    [ContextMenu("Create Transform at Position")]
    void CreateTransformAtPosition()
    {
        GameObject dummy = new GameObject("DUMMY");
        dummy.transform.position = transform.position;
        dummy.transform.rotation = transform.rotation;
    }
    
    [ContextMenu("Move Viewport to Camera")]
    public void MoveViewportToCamera()
    {
        UnityEditor.SceneView.lastActiveSceneView.pivot = transform.position;
        UnityEditor.SceneView.lastActiveSceneView.rotation = transform.rotation;

        Camera camera = GetComponent<Camera>();
        if( camera != null )
            UnityEditor.SceneView.lastActiveSceneView.orthographic = camera.orthographic;

        UnityEditor.SceneView.RepaintAll();
        Debug.Log("camera moved");
    }
    
    public bool moveToViewport = false;

    public Transform moveToTarget = null;

    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject != gameObject)
            moveToViewport = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (moveToTarget != null)
        {
            moveToViewport = false;

            transform.position = moveToTarget.position;
            transform.rotation = moveToTarget.rotation;

            moveToTarget = null;
        }

        if (moveToViewport)
        {
            m_sceneCamera = UnityEditor.SceneView.lastActiveSceneView.camera;
            if (m_sceneCamera == null)
                return;

            transform.position = m_sceneCamera.transform.position;
            transform.rotation = m_sceneCamera.transform.rotation;
        }
    }
#endif
}
