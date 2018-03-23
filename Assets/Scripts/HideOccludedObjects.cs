using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class HideOccludedObjects : MonoBehaviour
{
    [ContextMenu("Hide")]
    public void HideNow()
    {
        UnhideAll();

        Camera camera = GetComponent<Camera>();
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        foreach( Renderer rdr in Object.FindObjectsOfType<Renderer>() )
        {
            Bounds bds = rdr.bounds;
            if( !GeometryUtility.TestPlanesAABB(planes, bds) )
            {
                occluded.Add(rdr);
                rdr.enabled = false;
            }
        }
    }

    [ContextMenu("Unhide")]
    public void UnhideAll()
    {
        while (occluded.Count > 0)
        {
            occluded[0].enabled = true;
            occluded.RemoveAt(0);
        }
    }

    public List<Renderer> occluded = new List<Renderer>();
}
