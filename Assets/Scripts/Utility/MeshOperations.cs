using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(MeshFilter))]
public class MeshOperations : MonoBehaviour
{
    [ContextMenu("Reverse Normals")]
    public void ReverseNormals()
    {
        Mesh baseMesh = GetComponent<MeshFilter>().sharedMesh;
        Mesh newMesh = Object.Instantiate(baseMesh) as Mesh;

        MeshHelper.Flip(newMesh);

        GetComponent<MeshFilter>().sharedMesh = newMesh;

#if UNITY_EDITOR
        if (Application.isPlaying)
            return;

        if( !AssetDatabase.Contains(newMesh) )
        {
            string assetPath = EditorUtility.SaveFilePanelInProject("Save mesh", newMesh.name, "asset", "");
            if( !string.IsNullOrEmpty(assetPath) )
            {
                AssetDatabase.CreateAsset(newMesh, assetPath);
            }
        }

        AssetDatabase.SaveAssets();
#endif
    }
}
