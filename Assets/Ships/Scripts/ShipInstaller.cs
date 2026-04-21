

using UnityEngine;
using UnityEngine.Serialization;

public class ShipInstaller : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private bool _useIA;
    [SerializeField] private bool _useJoystick;
    [SerializeField] private ShipMediator _ship;
    [SerializeField] private ShipToSpawnConfiguration _playerShipConfiguration;
    [SerializeField] private ShipsWharehouse _shipsWharehouse;
    
    [Header("Android")]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private JoyButton _fireButton;

    private void Awake()
    {
        var shipFactory = new ShipsFactory(Instantiate(_shipsWharehouse));
        var shipBuilder = shipFactory.Create(_playerShipConfiguration.ShipId.Value)
            .WithConfiguration(_playerShipConfiguration);
        SetInput(shipBuilder);
        SetCheckLimits(shipBuilder);
        _ship = shipBuilder.Build();
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
}