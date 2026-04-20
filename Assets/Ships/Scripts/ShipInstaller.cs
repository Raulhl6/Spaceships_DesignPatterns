

using UnityEngine;
using UnityEngine.Serialization;

public class ShipInstaller : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private bool _useIA;
    [SerializeField] private bool _useJoystick;
    [SerializeField] private ShipMediator _ship;
    [SerializeField] private ShipToSpawnConfiguration _playerShipConfiguration;
    
    [Header("Android")]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private JoyButton _fireButton;

    private void Awake()
    {
        _ship.Configure(GetInput(),
            GetCheckLimits(),
            _playerShipConfiguration.Speed,
            _playerShipConfiguration.FireRate,
            _playerShipConfiguration.DefaultProjectileId);
    }

    private ICheckLimits GetCheckLimits()
    {
        if (_useIA)  return new InitialPositionCheckLimits(_ship.transform, 2f);
        
        return new ViewportCheckLimits(_ship.transform, Camera.main);
        
    }

    private IInput GetInput()
    {
        if (_useIA) return new InputAIAdapter(_ship.transform);
        if (_useJoystick) return new InputJoystickAdapter(_joystick, _fireButton);
        return InputReaderAdapter.Create();
    }
}