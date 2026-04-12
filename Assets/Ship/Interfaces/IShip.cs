using UnityEngine;

public interface IShip
{
    string Id { get; }
    Transform GetTransform();
    
}
