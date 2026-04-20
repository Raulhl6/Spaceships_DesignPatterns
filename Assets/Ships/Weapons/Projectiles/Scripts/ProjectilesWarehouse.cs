using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create ProjectilesWarehouse", fileName = "ProjectilesWarehouse", order = 0)]
public class ProjectilesWarehouse : ScriptableObject
{
    [SerializeField] private ProjectileBase[] _projectilesPrefabs;

    private Dictionary<string, ProjectileBase> _idToProjectilesPrefab;

    public void Configure()
    {
        _idToProjectilesPrefab = new Dictionary<string, ProjectileBase>();
        foreach (var projectile in _projectilesPrefabs)
        {
            _idToProjectilesPrefab.Add(projectile.Id, projectile);
        }
    }
    
    public ProjectileBase GetProjectileById(string id)
    {
        if (_idToProjectilesPrefab.TryGetValue(id, out var projectile))
        {
            return projectile;
        }
        throw new Exception($"Projectile with id {id} not found in configuration.");
    }
}
