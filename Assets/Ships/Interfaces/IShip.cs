using UnityEngine;

public interface IShip
{
    string Id { get; }
    Transform GetTransform();

    void OnDamageReceived(bool isDead);
}
