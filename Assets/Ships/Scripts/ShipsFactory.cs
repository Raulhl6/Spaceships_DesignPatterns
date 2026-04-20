using UnityEngine;

public class ShipsFactory
{
    
    private readonly ShipsWharehouse _shipsWarehouse;

    private ShipsFactory() {}
    
    public ShipsFactory(ShipsWharehouse shipsWarehouse)
    {
        _shipsWarehouse = shipsWarehouse;
        _shipsWarehouse.Configure();
    }

    public ShipBuilder Create(string id, Vector3 position, Quaternion rotation)
    {
        var prefab = Object.Instantiate(_shipsWarehouse.GetShipById(id), position, rotation);
        
         return new ShipBuilder()
             .FromPrefab(prefab)
             .WithPosition(position);
    }
    
}
