using UnityEngine;

public class ShipData
{
    public readonly IInput input;
    public readonly ICheckLimits checkLimits;

    public readonly Vector2 speed;
    public readonly float fireRate;
    public readonly int health;
    public readonly ProjectileId DefaultProjectileId;
    public readonly ETeams team;
    
    public ShipData(IInput input, ICheckLimits checkLimits,
        Vector2 speed, float fireRate, ProjectileId defaultProjectileId,
        int health, ETeams team)
    {
        this.input = input;
        this.checkLimits = checkLimits;
        this.speed = speed;
        this.fireRate = fireRate;
        DefaultProjectileId = defaultProjectileId;
        this.health = health;
        this.team = team;
    }

    
}
