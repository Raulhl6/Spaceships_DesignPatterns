using UnityEngine;

public class ProjectilesFactory
{
    
    private readonly ProjectilesWarehouse _projectilesWarehouse;

    private ProjectilesFactory() {}
    
    public ProjectilesFactory(ProjectilesWarehouse projectilesWarehouse)
    {
        _projectilesWarehouse = projectilesWarehouse;
        _projectilesWarehouse.Configure();
    }

    public ProjectileBase Create(string id, Vector3 position, Quaternion rotation, ETeams team)
    {
        var prefab = _projectilesWarehouse.GetProjectileById(id);
        
        var projectile = Object.Instantiate(prefab, position, rotation);
        projectile.Configure(team);
        return projectile;
    }
}
