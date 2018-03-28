using UnityEngine;
using System.Collections;

public class SelectGameObjectsWithTag : MonoBehaviour
{
    public string tag = "";

#if UNITY_EDITOR
    [ContextMenu("Select Now")]
    public void SelectNow()
    {
        UnityEditor.Selection.objects = GameObject.FindGameObjectsWithTag(tag);
    }
#endif
}
