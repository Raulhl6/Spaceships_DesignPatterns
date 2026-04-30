using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private ProjectilesWarehouse _projectilesWarehouse;
    [SerializeField] private Transform _projectileSpawnPoint;
    
    private ProjectileId _defaultProjectileId;
    private float _fireRateInSeconds = 0.3f;
    private IShip _ship;
    private float _remainingSecondsToShoot;
    private ProjectileBase _currentProjectile;
    private ProjectilesFactory _projectilesFactory;
    private ETeams _team;

    public void Configure(IShip ship, float fireRate, ProjectileId projectileId, ETeams team)
    {
        _ship = ship;
        _defaultProjectileId = projectileId;
        _fireRateInSeconds = fireRate;
        _projectilesWarehouse = Instantiate(_projectilesWarehouse);
        _projectilesFactory = new ProjectilesFactory(_projectilesWarehouse);
        _currentProjectile = _projectilesWarehouse.GetProjectileById(_defaultProjectileId.Value);
        _team = team;
    }
    
    private void HandleShootCooldown()
    {
        if (_remainingSecondsToShoot <= 0) return;
        
        _remainingSecondsToShoot -= Time.deltaTime;
        
    }

    public void HandleShoot(bool canShoot)
    {
        HandleShootCooldown();
        
        if (canShoot && _remainingSecondsToShoot <= 0)
        {
            var projectile = _projectilesFactory.Create(
                _currentProjectile.Id,
                _projectileSpawnPoint.position,
                _projectileSpawnPoint.rotation,
                _team);

            _remainingSecondsToShoot = _fireRateInSeconds;
        }
    }

    
}
