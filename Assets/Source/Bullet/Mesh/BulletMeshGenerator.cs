using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BulletMeshGenerator : MonoBehaviour
{
    [Header("Bullet Mesh components")]
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _projectileMaterial;

    [Header("Bullet create parametres")]
    [SerializeField] private float _bulletSize = 1f;
    [SerializeField] private float _perturbationAmount = 0.1f;

    private Mesh mesh;

    private void Start()
    {
        // Create a new mesh and customize
        mesh = new Mesh();
        _meshFilter.mesh = mesh;

        GenerateMesh();
    }

    public void GenerateMesh() // Method of generating a mesh with irregularities
    {
        Vector3[] vertices = new Vector3[24]; // Array for storing vertices
        int[] triangles = new int[36]; // Array for triangle indices

        // Base vertices of the cube
        Vector3[] baseVertices = new Vector3[]
        {
            new Vector3(-1f, -1f, -1f), // Left lower back vertex
            new Vector3(1f, -1f, -1f), // Lower right back vertex
            new Vector3(1f, 1f, -1f), // Upper right back vertex
            new Vector3(-1f, 1f, -1f), // Left upper back vertex
            new Vector3(-1f, -1f, 1f), // Left lower front vertex
            new Vector3(1f, -1f, 1f), // Lower right front vertex
            new Vector3(1f, 1f, 1f), // Upper right front vertex
            new Vector3(-1f, 1f, 1f) // Left upper front apex
        };

        // Create vertices of the cube with irregularities by adding an offset
        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i] * _bulletSize;
            vertex += Random.insideUnitSphere * _perturbationAmount; // Добавляем смещение
            vertices[i] = vertex;
        }

        // Basic triangles defining 6 faces of the cube (2 triangles per face)
        int[] baseTriangles = new int[]
        {
            0, 2, 1, 0, 3, 2, // Back edge
            4, 5, 6, 4, 6, 7, // Front edge
            0, 1, 5, 0, 5, 4, // Bottom edge
            2, 3, 7, 2, 7, 6, // Upper edge
            0, 4, 7, 0, 7, 3, // Left edge
            1, 2, 6, 1, 6, 5  // Right edge
        };

        // Fill the triangle array
        for (int i = 0; i < baseTriangles.Length; i++)
        {
            triangles[i] = baseTriangles[i];
        }

        // Assign vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Recalculate normals for correct lighting of the mesh
        mesh.RecalculateNormals();
    }
}