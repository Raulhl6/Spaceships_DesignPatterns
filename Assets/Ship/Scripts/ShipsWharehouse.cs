using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Create ShipsWharehouse", fileName = "ShipsWharehouse", order = 0)]
public class ShipsWharehouse : ScriptableObject
{
    [FormerlySerializedAs("_projectilesPrefabs")] [SerializeField] private ShipMediator[] _shipsPrefabs;

    private Dictionary<string, ShipMediator> _idToShipsPrefab;

    public void Configure()
    {
        _idToShipsPrefab = new Dictionary<string, ShipMediator>();
        foreach (var ship in _shipsPrefabs)
        {
            _idToShipsPrefab.Add(ship.Id, ship);
        }
    }
    
    public ShipMediator GetShipById(string id)
    {
        if (_idToShipsPrefab.TryGetValue(id, out var ship))
        {
            return ship;
        }
        throw new Exception($"Ship with id {id} not found in configuration.");
    }
}
