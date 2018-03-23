using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshHelper
{
    static public MeshFilter CreateMeshObject(Mesh mesh, Transform parent, int layer)
    {
        return CreateMeshObject(mesh, parent, layer, mesh.name, new Material[0], true);
    }

    static public MeshFilter CreateMeshObject(Mesh mesh, Transform parent, int layer, bool addCollider)
    {
        return CreateMeshObject(mesh, parent, layer, mesh.name, new Material[0], addCollider);
    }

    static public MeshFilter CreateMeshObject(Mesh mesh, Transform parent, int layer, Material[] materials, bool addCollider)
    {
        return CreateMeshObject(mesh, parent, layer, mesh.name, materials, addCollider);
    }

    static public MeshFilter CreateMeshObject( Mesh mesh, Transform parent, int layer, string name, Material[] materials, bool addCollider )
    {
        GameObject meshObject = new GameObject(name);
        meshObject.layer = layer;
        meshObject.transform.parent = parent;
        meshObject.transform.localPosition = Vector3.zero;
        meshObject.transform.localRotation = Quaternion.identity;

        MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = mesh;

        meshObject.AddComponent<MeshRenderer>().sharedMaterials = materials;

        meshObject.AddComponent<MeshCollider>();

        return meshFilter;
    }

    static public Mesh QuickMesh(Vector3[] vts, int[] tris, Vector3[] nms = null, Vector2[] uvs = null)
    {
        Mesh mesh = new Mesh();

        mesh.vertices = vts;
        mesh.triangles = tris;

        if (nms == null)
        {
            nms = new Vector3[vts.Length];
            for (int i = 0; i < nms.Length; i++)
            {
                nms[i] = Vector3.up;
            }
        }

        mesh.normals = nms;

        mesh.uv = uvs != null ? uvs : new Vector2[vts.Length];

        return mesh;
    }

    static public Mesh MakeTri(Vector3[] vts, Vector3[] nms, Vector2[] uvs)
    {
        Mesh mesh = new Mesh();

        if (vts.Length < 3)
        {
            Debug.LogWarning("Unable to make tri: need at least 3 vertices");
            return mesh;
        }

        mesh.vertices = vts;
        mesh.normals = nms;
        mesh.uv = uvs;

        mesh.triangles = new int[3] { 0, 1, 2 };

        return mesh;
    }

    static public Mesh MakeQuad(Vector3[] vts, Vector3[] nms, Vector2[] uvs)
    {
        Mesh mesh = new Mesh();

        if (vts.Length < 4)
        {
            Debug.LogWarning("Unable to make quad: need at least 4 vertices");
            return mesh;
        }

        mesh.vertices = vts;
        mesh.normals = nms;
        mesh.uv = uvs;

        mesh.triangles = new int[6] { 0, 1, 3, 1, 2, 3 };

        return mesh;
    }

    static public Vector2[] DistanceToUVX(Vector3[] input, float distance )
    {
        int c = input.Length;
        Vector2[] output = new Vector2[input.Length];
        for( int i = 0; i < c; i++ )
        {
            output[i].y = input[i].y;
            output[i].x = distance;
        }
        return output;
    }

    static public void Offset(Mesh mesh, Vector3 offset)
    {
        Vector3[] vts = mesh.vertices;
        for (int i = 0; i < vts.Length; i++)
            vts[i] += offset;
        mesh.vertices = vts;
    }

    static public void Flip(Mesh mesh)
    {
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
            normals[i] = -normals[i];
        mesh.normals = normals;

        for (int m = 0; m < mesh.subMeshCount; m++)
        {
            int[] triangles = mesh.GetTriangles(m);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                int temp = triangles[i + 0];
                triangles[i + 0] = triangles[i + 1];
                triangles[i + 1] = temp;
            }
            mesh.SetTriangles(triangles, m);
        }
    }

    static public void Scale(Mesh mesh, Vector3 scale)
    {
        Vector3[] vts = mesh.vertices;
        for (int i = 0; i < vts.Length; i++)
        {
            vts[i].x *= scale.x;
            vts[i].y *= scale.y;
            vts[i].z *= scale.z;
        }
        mesh.vertices = vts;
    }

    private List<Vector3> m_vertices = new List<Vector3>();
    private List<Vector3> m_normals = new List<Vector3>();
    private List<Vector2> m_uvs = new List<Vector2>();
    private List<int> m_triangles = new List<int>();

    public MeshHelper( MeshHelper other = null )
    {
       if( other != null )
        {
            Merge(other);
        }
    }

    public void Merge( MeshHelper other )
    {
        m_vertices.AddRange(other.m_vertices);
        m_normals.AddRange(other.m_normals);
        m_uvs.AddRange(other.m_uvs);
        m_triangles.AddRange(other.m_triangles);
    }

    public void AddTri(Vector3[] vts, Vector3[] nms, Vector2[] uvs)
    {
        if( vts.Length != 3 || nms.Length != vts.Length || uvs.Length != vts.Length )
        {
            Debug.LogWarning("Vertices, normals or uvs length != 3");
            return;
        }

        int baseIndex = m_vertices.Count;
        m_vertices.AddRange(vts);
        m_normals.AddRange(nms);
        m_uvs.AddRange(uvs);
        m_triangles.AddRange(new int[3] { baseIndex, baseIndex+1, baseIndex+2 });
    }

    public void AddQuad(Vector3[] vts, Vector3[] nms, Vector2[] uvs)
    {
        if (vts.Length != 4 || nms.Length != vts.Length || uvs.Length != vts.Length)
        {
            Debug.LogWarning("Vertices, normals or uvs length != 4");
            return;
        }

        int baseIndex = m_vertices.Count;
        m_vertices.AddRange(vts);
        m_normals.AddRange(nms);
        m_uvs.AddRange(uvs);

        m_triangles.AddRange(new int[6] { baseIndex, baseIndex + 1, baseIndex + 3, baseIndex + 1, baseIndex + 2, baseIndex + 3 });
    }

    public void Scale(float scale)
    {
        for (int i = 0; i < m_vertices.Count; i++)
        {
            m_vertices[i] *= scale;
        }
    }

    public void Offset(Vector3 offset)
    {
        for( int i =0; i < m_vertices.Count; i++ )
        {
            m_vertices[i] += offset;
        }
    }

    public void Flip()
    {
        for (int i = 0; i < m_normals.Count; i++)
        {
            m_normals[i] = -m_normals[i];
        }
    }

    public Mesh CreateMesh(string name = "", int subMesh = 0)
    {
        Mesh mesh = new Mesh();
        mesh.name = name;

        mesh.subMeshCount = subMesh + 1;
        mesh.SetVertices(m_vertices);
        mesh.SetNormals(m_normals);
        mesh.SetUVs(0, m_uvs);
        mesh.SetTriangles(m_triangles.ToArray(), subMesh);

        return mesh;
    }

    public void Clear()
    {
        m_vertices.Clear();
        m_normals.Clear();
        m_uvs.Clear();
        m_triangles.Clear();
    }
}