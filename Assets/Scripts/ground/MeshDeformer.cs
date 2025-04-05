using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class MeshDeformer : MonoBehaviour
{
    [Header("Настройки деформации")] 
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private float _deformationStrength = 0.5f;
    [SerializeField] private float _smoothness = 2f;

    [SerializeField] private float _destructionDepth = -1f;

    private Mesh _mesh;
    private Vector3[] _originalVertices;
    private Vector3[] _modifiedVertices;
    private MeshCollider _meshCollider;
    private readonly HashSet<int> _removedVertices = new HashSet<int>();

    private Vector3 _clickPoint;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();

        _mesh = Instantiate(meshFilter.mesh);
        meshFilter.mesh = _mesh;

        _originalVertices = _mesh.vertices;
        _modifiedVertices = (Vector3[])_originalVertices.Clone();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
        {
            _clickPoint = hit.point;
            Vector3 localHitPoint = transform.InverseTransformPoint(hit.point);
            // Vector3 deformationDirection = transform.InverseTransformDirection(-hit.normal);
            DeformMesh(localHitPoint);
        }
    }

    // private void DeformMesh(Vector3 hitPoint, Vector3 direction)
    // {
    //     for (int i = 0; i < _modifiedVertices.Length; i++)
    //     {
    //         Vector3 vertex = _modifiedVertices[i];
    //         float distance = Vector3.Distance(vertex, hitPoint);
    //
    //         if (distance < _radius)
    //         {
    //             float falloff = 1 - Mathf.Pow(distance / _radius, _smoothness);
    //             _modifiedVertices[i] += direction * _deformationStrength * falloff;
    //         }
    //     }
    //
    //     ApplyMeshChanges();
    // }

    private void DeformMesh(Vector3 hitPoint)
    {
        // List<int> affectedVertices = new List<int>();

        for (int i = 0; i < _modifiedVertices.Length; i++)
        {
            if (_removedVertices.Contains(i)) continue;

            float distance = Vector3.Distance(_modifiedVertices[i], hitPoint);
            if (distance < _radius)
            {
                float falloff = 1 - Mathf.Pow(distance / _radius, _smoothness);
                _modifiedVertices[i] += Vector3.down * _deformationStrength * falloff;
                // _modifiedVertices[i] += direction * _deformationStrength * falloff;

                if (_modifiedVertices[i].y < _destructionDepth)
                {
                    _modifiedVertices[i].y = _destructionDepth;
                    // affectedVertices.Add(i);
                }
            }
        }

        // if (affectedVertices.Count > 0)
        // {
        //     RemoveVertices(affectedVertices);
        // }

        ApplyMeshChanges();
    }
    
    private void RemoveVertices(List<int> verticesToRemove)
    {
        // Помечаем вершины для удаления
        _removedVertices.UnionWith(verticesToRemove);

        // Создаем новые треугольники без удаленных вершин
        List<int> newTriangles = new List<int>();
        int[] triangles = _mesh.triangles;
        Vector3[] vertices = _mesh.vertices;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            bool toDestroy = true;
            for (int j = 0; j < 3; j++)
            {
                // if (_removedVertices.Contains(triangles[i + j]))
                // {
                //     keepTriangle = false;
                //     break;
                // }
                if (vertices[triangles[i + j]].y >= _destructionDepth)
                {
                    toDestroy = false;
                    break;
                }
            }

            if (!toDestroy)
            {
                newTriangles.Add(triangles[i]);
                newTriangles.Add(triangles[i + 1]);
                newTriangles.Add(triangles[i + 2]);
            }
        }

        _mesh.triangles = newTriangles.ToArray();
    }

    private void ApplyMeshChanges()
    {
        _mesh.vertices = _modifiedVertices;
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        _meshCollider.sharedMesh = _mesh;
    }
}