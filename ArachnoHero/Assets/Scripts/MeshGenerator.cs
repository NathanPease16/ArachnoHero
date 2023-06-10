using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [Header("References")]
    private Mesh mesh;

    [Header("Attributes")]
    [SerializeField] private bool discreteMesh;
    [SerializeField] private int xSize;
    [SerializeField] private int zSize;
    [SerializeField] private float intensity;
    [SerializeField] private float zoom;
    [SerializeField] private float xPosition;
    [SerializeField] private float zPosition;

    [Header("Terrain Data")]
    [SerializeField] private TerrainData[] terrainData;

    [Header("Data")]
    private Vector3[] vertices;
    //private int[] triangles;
    private List<int>[] triangles;

    [Header("References")]
    private new MeshRenderer renderer;

    Vector3[] points;
    List<int>[] triangles2;

    void Start()
    {
        mesh = new Mesh();
        renderer = GetComponent<MeshRenderer>();
        GetComponent<MeshFilter>().mesh = mesh;
        triangles = new List<int>[terrainData.Length];

        for (int i = 0; i < triangles.Length; i++)
            triangles[i] = new List<int>();

        SortData(terrainData);
        
        Material[] materials = new Material[terrainData.Length];
        for (int i = 0; i < terrainData.Length; i++)
            materials[i] = terrainData[i].material;

        renderer.materials = materials;

        CreateShape();
        if (discreteMesh)
            //MakeMeshDiscrete(vertices, triangles);
        UpdateMesh();







        // skorlkee;
        float radius = 10;
        float spacing = 1;

        float circumference = 2f * Mathf.PI * radius;
        int numPoints = Mathf.CeilToInt((circumference / spacing) * (radius - 1));

        float circumferencePoints = numPoints/(radius - 1);

        List<Vector3> ps = new List<Vector3>();

        for (int i = 0; i < circumferencePoints - 1; i++)
        {
            float angle = Mathf.PI * 2f * i / (circumferencePoints - 1);
            float x = Mathf.Sin(angle) * radius;
            float z = Mathf.Cos(angle) * radius;
            float y = Mathf.PerlinNoise(x * zoom + xPosition, z * zoom + zPosition) * intensity;
            ps.Add(new Vector3(x, y, z));
        }

        for (int i = 1; i < radius; i++)
        {
            for (int j = 0; j < circumferencePoints; j++)
            {
                int index = j + (int)circumferencePoints * i;
                Vector3 dir = -ps[j].normalized;
                dir.y = 0;
                Vector3 point = ps[j] + (dir * i);
                point.y = Mathf.PerlinNoise(point.x * zoom + xPosition, point.z * zoom + zPosition) * intensity;
                ps.Add(point);
            }
        }

        points = ps.ToArray();

        triangles2 = new List<int>[points.Length];

        for (int i = 0; i < triangles2.Length; i++)
            triangles2[i] = new List<int>();

        /*
        vert = 0;
        triangles2[0].Add(vert);
        triangles2[0].Add(vert + 1);
        triangles2[0].Add((int)circumferencePoints + vert);
        triangles2[0].Add((int)circumferencePoints + vert);
        triangles2[0].Add(vert + 1);
        triangles2[0].Add((int)circumferencePoints + vert + 1);
        */

        Debug.Log(circumferencePoints);
        
        int vert = 0;
        for (int j = 0; j < points.Length - 1; j++)
        {
            if ((int)circumferencePoints + vert + 1 < points.Length)
            {
                triangles2[0].Add(vert);
                triangles2[0].Add(vert + 1);
                triangles2[0].Add((int)circumferencePoints + vert);
                triangles2[0].Add((int)circumferencePoints + vert);
                triangles2[0].Add(vert + 1);
                triangles2[0].Add((int)circumferencePoints + vert + 1);
            }
            else
            {
                triangles2[0].Add(vert);
                triangles2[0].Add(vert + 1);
                //triangles2[0].Add((int)circumferencePoints + vert);
                //triangles2[0].Add((int)circumferencePoints + vert);
                triangles2[0].Add(points.Length - 1);
                triangles2[0].Add(points.Length - 1);
                triangles2[0].Add(vert + 1);
                //triangles2[0].Add((int)circumferencePoints + vert + 1);
                triangles2[0].Add(points.Length - 1);
            }

            vert++;
        }

        triangles2[0].Add(0);
        triangles2[0].Add((int)circumferencePoints);
        triangles2[0].Add((int)circumferencePoints - 1);

        UpdateMesh2();
}

    private void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * zoom + xPosition, z * zoom + zPosition) * intensity;

                float xOffset = 0;
                float zOffset = 0;
                
                if (x == 0)
                    xOffset = -Mathf.PerlinNoise(x * zoom + xPosition, z * zoom + zPosition) * intensity * 2;
                else if (x == xSize)
                    xOffset = Mathf.PerlinNoise(x * zoom + xPosition, z * zoom + zPosition) * intensity * 2;

                else if (z == 0)
                    zOffset = -Mathf.PerlinNoise(x * zoom + xPosition, z * zoom + zPosition) * intensity * 2;
                else if (z == zSize)
                    zOffset = Mathf.PerlinNoise(x * zoom + xPosition, z * zoom + zPosition) * intensity * 2;

                vertices[i] = new Vector3(x + xOffset, y, z + zOffset);
                i++;
            }
        }

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int i = 0; i < terrainData.Length; i++)
                {
                    if (vertices[vert].y <= terrainData[i].height)
                    {
                        triangles[i].Add(vert);
                        triangles[i].Add(vert + xSize + 1);
                        triangles[i].Add(vert + 1);
                        triangles[i].Add(vert + 1);
                        triangles[i].Add(vert + xSize + 1);
                        triangles[i].Add(vert + xSize + 2);
                    }
                }

                vert++;
                tris += 6;
            }
        }
    }

    private void SortData(TerrainData[] data)
    {
        for (int index = 0; index < data.Length; index++)
        {
            int smallest = index;

            for (int i = index; i < data.Length; i++)
                if (data[smallest].height > data[i].height)
                    smallest = i;

            TerrainData temp = data[index];
            data[index] = data[smallest];
            data[smallest] = temp;
        }
    }

    private void MakeMeshDiscrete(Vector3[] vert, int[] tri) 
    {
        Vector3[] vertDiscrete = new Vector3[tri.Length];
        int[] triDiscrete = new int[tri.Length];
        for (int i = 0; i < tri.Length; i++)
        {
            //vertDiscrete[i] = vert[triangles[i]];
            triDiscrete[i] = i;
        }

        vertices = vertDiscrete;
        //triangles = triDiscrete;
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;

        mesh.subMeshCount = triangles.Length;
        
        for (int i = 0; i < triangles.Length; i++)
        {
            mesh.SetTriangles(triangles[i].ToArray(), i);
        }

        mesh.RecalculateNormals();
    }

    private void UpdateMesh2()
    {
        mesh.Clear();

        mesh.Clear();
        mesh.vertices = points;

        mesh.subMeshCount = triangles2.Length;
        
        for (int i = 0; i < triangles2.Length; i++)
        {
            if (triangles2[i].ToArray().Length == 0)
                continue;

            mesh.SetTriangles(triangles2[i].ToArray(), i);
        }

        mesh.RecalculateNormals();
    }

    void OnDrawGizmos()
    {
        if (vertices == null || points == null) { return; }

        for (int i = 0; i < vertices.Length; i++)
        {
            //Gizmos.DrawSphere(vertices[i], .1f);
        }

        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(points[i], .1f);
        }
    }
}
