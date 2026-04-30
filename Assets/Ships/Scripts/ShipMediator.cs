using UnityEngine;

public enum ETeams { Ally, Enemy }

public class ShipMediator : MonoBehaviour, IShip
{
    [SerializeField] private MovementController _movementController;
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private HealthController _healthController;
    [SerializeField] private ShipId _shipId;

    #region Unity Methods

    private void Update()
    {

        _weaponController.HandleShoot(_input.IsFireActionPressed());
    }

    private void FixedUpdate()
    {
        _movementController.Move(_input.GetMovementVector());
    }

    #endregion


    #region Configure

    private IInput _input;

    public void Configure(ShipData data)
    {
        _input = data.input;
        _movementController.Configure(this, data.checkLimits, data.speed);
        _healthController.Configure(this, data.health, data.team);
        _weaponController.Configure(this, data.fireRate, data.DefaultProjectileId, data.team);
    }

    #endregion


    #region IShip Implementation

    public string Id => _shipId.Value;
    public Transform GetTransform() => transform;

    public void OnDamageReceived(bool isDead)
    {
        if (isDead) Destroy(gameObject);
    }

    #endregion


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable damageable)) return;
        if (damageable.Team == _healthController.Team) return;
        damageable.AddDamage(100);
    }
}