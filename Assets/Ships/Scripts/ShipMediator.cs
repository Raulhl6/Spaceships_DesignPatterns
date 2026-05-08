using System;
using UnityEngine;

public enum ETeams { Ally, Enemy }

public class ShipMediator : MonoBehaviour, IShip, IEventObsever
{
    [SerializeField] private MovementController _movementController;
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private HealthController _healthController;
    [SerializeField] private ShipId _shipId;

    #region Unity Methods

    private void Start()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EEventIds.GameOver, this);
        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EEventIds.Victory, this);
    }

    private void Update()
    {
        _weaponController.HandleShoot(_input.IsFireActionPressed());
        CheckDestroyLimits();
    }
    
    private void FixedUpdate()
    {
        _movementController.Move(_input.GetMovementVector());
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().UnSubscribe(EEventIds.GameOver, this);
        ServiceLocator.Instance.GetService<IEventQueue>().UnSubscribe(EEventIds.Victory, this);
    }

    #endregion


    #region Configure

    private int _score;
    private IInput _input;
    private ICheckDestroyLimits _checkDestroyLimits;

    public void Configure(ShipData data)
    {
        _input = data.input;
        _score = data.score;
        _checkDestroyLimits = data.checkDestroyLimits;
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
        if (isDead)
        {
            Destroy(gameObject);
            
            ServiceLocator.Instance.GetService<IEventQueue>().
                EnqueueEvent(new ShipDestroyedEventData(_score, _healthController.Team, GetInstanceID()));
        }
    }

    #endregion
    
    private void CheckDestroyLimits()
    {
        if (_checkDestroyLimits.IsInsideTheLimits(transform.position)) return;
        Destroy(gameObject);
        ServiceLocator.Instance.GetService<IEventQueue>().
            EnqueueEvent(new ShipDestroyedEventData(0, _healthController.Team, GetInstanceID()));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable damageable)) return;
        if (damageable.Team == _healthController.Team) return;
        damageable.AddDamage(100);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId != EEventIds.GameOver && eventData.EventId != EEventIds.Victory) return;
        
        Destroy(gameObject);
    }
}