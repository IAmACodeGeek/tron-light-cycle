using System.Collections.Generic;
using UnityEngine;

public class LightWallGenerator : MonoBehaviour{
    public Transform[] spawnVectors = new Transform[4];
    public float spawnInterval = 1f;
    private float currentTime = 0f;

    private MeshFilter meshFilter;

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private int spawnVertexIndex = 0;
    public int maxSpawnedWalls = 100;

    void Awake(){
        meshFilter = GetComponent<MeshFilter>();
    }

    void Start(){
        InitializeWall();
    }

    void Update(){
        currentTime += Time.smoothDeltaTime;
        if(currentTime >= spawnInterval){
            ExtendWall();
            currentTime = 0;
        }

    }

    void InitializeWall(){
        // Draw the first face
        for(int i = 0; i < spawnVectors.Length; i++){
            vertices.Add(spawnVectors[i].position);
        }

        spawnVertexIndex = 4;

        meshFilter.mesh.vertices = vertices.ToArray();
        meshFilter.mesh.triangles = triangles.ToArray();

    }

    void ExtendWall(){
        // Add vertices
        for(int i = 0; i < spawnVectors.Length; i++){
            vertices.Add(spawnVectors[i].position);
        }

        // Top face
        triangles.Add(spawnVertexIndex+1);
        triangles.Add(spawnVertexIndex+2);
        triangles.Add(spawnVertexIndex-2);
        
        triangles.Add(spawnVertexIndex+1);
        triangles.Add(spawnVertexIndex-2);
        triangles.Add(spawnVertexIndex-3);
        
        // Bottom face
        triangles.Add(spawnVertexIndex);
        triangles.Add(spawnVertexIndex-1);
        triangles.Add(spawnVertexIndex+3);
        
        triangles.Add(spawnVertexIndex);
        triangles.Add(spawnVertexIndex-4);
        triangles.Add(spawnVertexIndex-1);

        // Left face
        triangles.Add(spawnVertexIndex);
        triangles.Add(spawnVertexIndex+1);
        triangles.Add(spawnVertexIndex-3);

        triangles.Add(spawnVertexIndex);
        triangles.Add(spawnVertexIndex-3);
        triangles.Add(spawnVertexIndex-4);

        // Right face
        triangles.Add(spawnVertexIndex+3);
        triangles.Add(spawnVertexIndex-2);
        triangles.Add(spawnVertexIndex+2);

        triangles.Add(spawnVertexIndex+3);
        triangles.Add(spawnVertexIndex-1);
        triangles.Add(spawnVertexIndex-2);

        spawnVertexIndex += 4;
        
        // Destroy excess walls
        meshFilter.mesh.Clear();
        if(vertices.Count > maxSpawnedWalls * 4){
            triangles.RemoveRange(0, 24);
            for(int i = 0; i < triangles.Count; i++){
                triangles[i] -= 4;
            }
            vertices.RemoveRange(0, 4);
            spawnVertexIndex -= 4;
        }
        
        meshFilter.mesh.vertices = vertices.ToArray();
        meshFilter.mesh.triangles = triangles.ToArray();
    }
}
