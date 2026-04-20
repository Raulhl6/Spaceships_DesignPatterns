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

    public ProjectileBase Create(string id, Vector3 position, Quaternion rotation)
    {
        return Object.Instantiate(_projectilesWarehouse.GetProjectileById(id), position, rotation);
    }
}
