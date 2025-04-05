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
    [SerializeField] private float _maxDistance = 5f;

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

    private void Update()
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
            if (Vector3.Distance(_camera.transform.position, hit.point) > _maxDistance) return;
            _clickPoint = hit.point;
            Vector3 localHitPoint = transform.InverseTransformPoint(hit.point);
            DeformMesh(localHitPoint);
        }
    }

    private void DeformMesh(Vector3 hitPoint)
    {

        for (int i = 0; i < _modifiedVertices.Length; i++)
        {
            if (_removedVertices.Contains(i)) continue;

            float distance = Vector3.Distance(_modifiedVertices[i], hitPoint);
            if (distance < _radius)
            {
                float falloff = 1 - Mathf.Pow(distance / _radius, _smoothness);
                _modifiedVertices[i] += Vector3.down * _deformationStrength * falloff;

                if (_modifiedVertices[i].y < _destructionDepth)
                {
                    _modifiedVertices[i].y = _destructionDepth;
                }
            }
        }

        ApplyMeshChanges();
    }

    private void ApplyMeshChanges()
    {
        _mesh.vertices = _modifiedVertices;
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();
        _meshCollider.sharedMesh = _mesh;
    }
}