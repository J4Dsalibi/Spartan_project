using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour
{
    public int arenaWidth, arenaLength;
    public int roomWidthMin, roomLengthMin;
    public int maxIterations;
    public int corridorWidth;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        CreateArena();
    }

    private void CreateArena()
    {
        // Check if the arena width and length are valid
        if (arenaWidth <= 0 || arenaLength <= 0)
        {
            Debug.LogError("Arena width and length must be greater than zero. Please check your setup.");
            return; // Exit early if the dimensions are invalid
        }

        ArenaGenerator generator = new ArenaGenerator(arenaWidth, arenaLength);
        var listOfRooms = generator.CalculateRooms(maxIterations, roomWidthMin, roomLengthMin);
        for (int i = 0; i < listOfRooms.Count; i++)
        {
            CreateMesh(listOfRooms[i].BottomLeftAreaCorner, listOfRooms[i].TopRightAreaCorner);
        }
    }

    private void CreateMesh(Vector2 bottomLeftCorner, Vector2 topRightCorner)
    {
        Vector3 bottomLeftV = new Vector3(bottomLeftCorner.x, 0, bottomLeftCorner.y);
        Vector3 bottomRightV = new Vector3(topRightCorner.x, 0, bottomLeftCorner.y);
        Vector3 topLeftV = new Vector3(bottomLeftCorner.x, 0, topRightCorner.y);
        Vector3 topRightV = new Vector3(topRightCorner.x, 0, topRightCorner.y);

        Vector3[] vertices = new Vector3[]
        {
            topLeftV,
            topRightV,
            bottomLeftV,
            bottomRightV,
        };

        Vector2[] uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        int[] triangles = new int[]
        {
            0,
            1,
            2,
            2,
            1,
            3,
        };

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        GameObject arenaFloor = new GameObject("Mesh" + bottomLeftCorner, typeof(MeshFilter), typeof(MeshRenderer));

        arenaFloor.transform.position = Vector3.zero;
        arenaFloor.transform.localScale = Vector3.one;
        arenaFloor.GetComponent<MeshFilter>().mesh = mesh;
        arenaFloor.GetComponent<MeshRenderer>().material = material;

    }

}
