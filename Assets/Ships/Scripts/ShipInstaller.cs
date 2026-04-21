

using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipInstaller : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private bool _useIA;
    [SerializeField] private bool _useJoystick;
    [SerializeField] private ShipToSpawnConfiguration _playerShipConfiguration;
    [SerializeField] private ShipsWharehouse _shipsWharehouse;
    
    [Header("Android")]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private JoyButton _fireButton;

    private ShipBuilder _shipBuilder;
    private ShipMediator _userShip;

    private void Awake()
    {
        var shipFactory = new ShipsFactory(Instantiate(_shipsWharehouse));
        _shipBuilder = shipFactory.Create(_playerShipConfiguration.ShipId.Value)
            .WithConfiguration(_playerShipConfiguration);
        SetInput(_shipBuilder);
        SetCheckLimits(_shipBuilder);
    }

    private void SetCheckLimits(ShipBuilder shipBuilder)
    {
        if (_useIA)
        {
            shipBuilder.WithCheckLimitsType(ShipBuilder.ECheckLimitsTypes.InitalPosition);
            return;
        }
        shipBuilder.WithCheckLimitsType(ShipBuilder.ECheckLimitsTypes.Viewport);
        
    }

    private void SetInput(ShipBuilder shipBuilder)
    {
        if (_useIA)
        {
            shipBuilder.WithInputMode(ShipBuilder.EInputMode.AI);
            return;
        }
        if (_useJoystick)
        {
            shipBuilder
                .WithInputMode(ShipBuilder.EInputMode.Joystick)
                .WithJoystick(_joystick)
                .WithJoyButton(_fireButton);
            return;
        }
        
        shipBuilder.WithInputMode(ShipBuilder.EInputMode.Unity);
    }

    public void SpawnUserShip()
    {
        _userShip = _shipBuilder.Build();
    }

    public void DestroyUserShip()
    {
        if (!_userShip) return;
        Destroy(_userShip.gameObject);
    }
}