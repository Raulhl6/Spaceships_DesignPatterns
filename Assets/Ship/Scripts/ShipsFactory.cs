using UnityEngine;

public class ShipsFactory : MonoBehaviour
{
    
    private readonly ShipsWharehouse _shipsWarehouse;

    private ShipsFactory() {}
    
    public ShipsFactory(ShipsWharehouse shipsWarehouse)
    {
        _shipsWarehouse = shipsWarehouse;
        _shipsWarehouse.Configure();
    }

    public ShipMediator Create(string id, Vector3 position, Quaternion rotation)
    {
        return Object.Instantiate(_shipsWarehouse.GetShipById(id), position, rotation);
    }
    
}
