using UnityEngine;

public class ShipBuilder
{
    public enum EInputMode
    {
        Unity, Joystick, AI
    }
    
    private Vector3 _position;
    private Quaternion _rotation;
    private IInput _input;
    private ICheckLimits _checkLimits;
    private ShipMediator _prefab;
    private ShipToSpawnConfiguration _shipConfiguration;
    private EInputMode _inputMode;
    private Joystick _joystick;
    private JoyButton _joyButton;

    public ShipBuilder WithPosition(Vector3 position)
    {
        _position = position;
        return this;
    }

    public ShipBuilder WithRotation(Quaternion rotation)
    {
        _rotation = rotation;
        return this;
    }

    public ShipBuilder WithInput(IInput input)
    {
        _input = input;
        return this;
    }

    public ShipBuilder WithCheckLimits(ICheckLimits checkLimits)
    {
        _checkLimits = checkLimits;
        return this;
    }
    
    public ShipBuilder FromPrefab(ShipMediator prefab)
    {
        _prefab = prefab;
        return this;
    }

    public ShipBuilder WithConfiguration(ShipToSpawnConfiguration shipConfiguration)
    {
        _shipConfiguration = shipConfiguration;
        return this;
    }

    public ShipBuilder WithJoystick(Joystick joystick)
    {
        _joystick = joystick;
        return this;
    }
    
    public ShipBuilder WithJoyButton(JoyButton joyButton)
    {
        _joyButton = joyButton;
        return this;
    }

    public ShipMediator Build()
    {
        ShipMediator ship = Object.Instantiate(_prefab, _position, _rotation);
        ship.Configure(GetInput(ship),
            _checkLimits,
            _shipConfiguration.Speed,
            _shipConfiguration.FireRate,
            _shipConfiguration.DefaultProjectileId);
        
        return ship;
    }

    private IInput GetInput(ShipMediator ship)
    {
        if (_input != null) return _input;
        
        switch (_inputMode)
        {
            case EInputMode.AI:
                return new InputAIAdapter(ship.transform);
            case EInputMode.Joystick:
                return new InputJoystickAdapter(_joystick, _joyButton);
            case EInputMode.Unity:
                return InputReaderAdapter.Create();
                break;
                
        }
        return null;
    }
}
