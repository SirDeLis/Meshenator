using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    float[,] heightmap;
    int[] triangles;
    Color[] colors;

    public Gradient gradient;

    public int xSize = 20;
    public int zSize = 20;
    public float maxTerrainHeight =25f;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        heightmap = new float[xSize + 1, zSize + 1];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                heightmap[z, x] = Mathf.PerlinNoise(x * .05f, z * .05f);
            }
        }

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x, heightmap[z, x] * maxTerrainHeight, z);
                i++;
            }
        }
        triangles = new int[xSize*zSize*6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;

            }
            vert++;
        }
        colors = new Color[vertices.Length];
        for (int i = 0,z=0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                float height = Mathf.InverseLerp(0f,maxTerrainHeight, vertices[i].y);
                colors[i] = gradient.Evaluate(height);  

                i++;
            }
        }
        
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();
    }
}
