

using System;
using UnityEngine;

public class ShipMediator : MonoBehaviour, IShip
{
    [SerializeField] private MovementController _movementController;
    [SerializeField] private WeaponController _weaponController;


    #region Unity Methods
    private void Update()
    {
        _movementController.Move(_input.GetMovementVector());
        _weaponController.HandleShoot(_input.IsFireActionPressed());
    }

    #endregion


    #region Configure
    private IInput _input;
    
    public void Configure(IInput input, ICheckLimits limits)
    {
        _input = input;
        _movementController.Configure(this, limits);
        _weaponController.Configure(this);
    }

    #endregion


    public Transform GetTransform() => transform;
}