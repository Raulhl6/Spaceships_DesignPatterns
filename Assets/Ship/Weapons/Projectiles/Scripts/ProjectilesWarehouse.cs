using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create ProjectilesConfiguration", fileName = "ProjectilesConfiguration", order = 0)]
public class ProjectilesWarehouse : ScriptableObject
{
    [SerializeField] private Projectile[] _projectilesPrefabs;

    private Dictionary<string, Projectile> _idToProjectilesPrefab;

    public void Configure()
    {
        _idToProjectilesPrefab = new Dictionary<string, Projectile>();
        foreach (var projectile in _projectilesPrefabs)
        {
            _idToProjectilesPrefab.Add(projectile.Id, projectile);
        }
    }
    
    public Projectile GetProjectileById(string id)
    {
        if (_idToProjectilesPrefab.TryGetValue(id, out var projectile))
        {
            return projectile;
        }
        throw new Exception($"Projectile with id {id} not found in configuration.");
    }
}
