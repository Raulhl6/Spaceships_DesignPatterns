

using System;
using UnityEngine;

public class ShipMediator : MonoBehaviour, IShip
{
    [SerializeField] private MovementController _movementController;
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private ShipId _shipId;

    #region Unity Methods
    private void Update()
    {
        _movementController.Move(_input.GetMovementVector());
        _weaponController.HandleShoot(_input.IsFireActionPressed());
    }

    #endregion


    #region Configure
    private IInput _input;
    
    public void Configure(IInput input, ICheckLimits limits, Vector2 speed, float fireRate, ProjectileId projectileId)
    {
        _input = input;
        _movementController.Configure(this, limits, speed);
        _weaponController.Configure(this, fireRate, projectileId);
    }

    #endregion


    #region IShip Implementation

    public string Id => _shipId.Value;
    public Transform GetTransform() => transform;

    #endregion

    
}