using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable
{
    private float _health;
    private IShip _ship;
    public ETeams Team { get; private set; }

    public void Configure(IShip ship, int health, ETeams team)
    {
        _ship = ship;
        _health = health;
        Team = team;
    }

    

    public void AddDamage(int amount)
    {
        _health = Mathf.Max(0, _health - amount);

        var isDead = _health <= 0;
        _ship.OnDamageReceived(isDead);

    }
}
