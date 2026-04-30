using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ShipBuilder
{
    public enum EInputMode
    {
        Unity, Joystick, AI
    }
    
    public enum ECheckLimitsTypes
    {
        InitalPosition, Viewport
    }
    
    private Vector3 _position;
    private Quaternion _rotation;
    private IInput _input;
    private ICheckLimits _checkLimits;
    private ECheckLimitsTypes _checkLimitsType = ECheckLimitsTypes.Viewport;
    private ShipMediator _prefab;
    private ShipToSpawnConfiguration _shipConfiguration;
    private EInputMode _inputMode;
    private Joystick _joystick;
    private JoyButton _joyButton;
    private ETeams _team;

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
    
    public ShipBuilder WithCheckLimitsType(ECheckLimitsTypes checkLimitsType)
    {
        _checkLimitsType = checkLimitsType;
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
    
    public ShipBuilder WithInputMode(EInputMode inputMode)
    {
        _inputMode = inputMode;
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
    public ShipBuilder WithTeam(ETeams team)
    {
        _team = team;
        return this;
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

    private ICheckLimits GetCheckLimits(ShipMediator ship)
    {
        if (_checkLimits != null) return _checkLimits;
        switch (_checkLimitsType)
        {
            case ECheckLimitsTypes.InitalPosition:
                return new InitialPositionCheckLimits(ship.transform, 10f);
                break;
            case ECheckLimitsTypes.Viewport:
                return new ViewportCheckLimits(Camera.main);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public ShipMediator Build()
    {
        ShipMediator ship = Object.Instantiate(_prefab, _position, _rotation);
        ShipData data = new ShipData(
            GetInput(ship),
            GetCheckLimits(ship),
            _shipConfiguration.Speed,
            _shipConfiguration.FireRate,
            _shipConfiguration.DefaultProjectileId, 
            _shipConfiguration.Health,
            _team);
        
        ship.Configure(data);
        
        return ship;
    }
}
