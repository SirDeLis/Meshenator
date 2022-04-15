using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMap : MonoBehaviour
{
    public float[,] heights;

    public float[,] GenerateHeightmap(int xLen, int zLen)
    {
        float[,] heightmap = new float[xLen+1, zLen+1];

        for (int z = 0; z <= zLen; z++)
        {
            for (int x = 0; x <= xLen; x++)
            {
                heightmap[z, x] = Mathf.PerlinNoise(x * .05f, z * .05f);
            }
        }
        return heightmap;
    }
}
