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

    public ShipBuilder Create(string id)
    {
        var prefab = _shipsWarehouse.GetShipById(id);
        
         return new ShipBuilder()
             .FromPrefab(prefab);
    }
    
}
