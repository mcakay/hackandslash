using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SpawnPointData
{
    public string PointId;
    public Transform Transform;
}

public class ProjectileController : MonoBehaviour
{
    [Header("Initial Spawn Points")]
    [SerializeField] private List<SpawnPointData> _initialSpawnPoints = new();

    private readonly Dictionary<string, Transform> _spawnPoints = new();

    private void Awake()
    {
        foreach (var point in _initialSpawnPoints)
        {
            RegisterSpawnPoint(point.PointId, point.Transform);
        }
    }

    public void RegisterSpawnPoint(string id, Transform pointTransform)
    {
        _spawnPoints[id] = pointTransform;
    }

    public void UnregisterSpawnPoint(string id)
    {
        _spawnPoints.Remove(id);
    }

    public Transform GetSpawnPoint(string id)
    {
        if (_spawnPoints.TryGetValue(id, out Transform point))
        {
            return point;
        }

        return transform;
    }
}
