

using UnityEngine;
using UnityEngine.Serialization;

public class ShipInstaller : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private bool _useIA;
    [SerializeField] private bool _useJoystick;
    [FormerlySerializedAs("_ship")] [SerializeField] private ShipMediator shipMediator;
    
    [Header("Android")]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private JoyButton _fireButton;

    private void Awake()
    {
        shipMediator.Configure(GetInput(), GetCheckLimits());
    }

    private ICheckLimits GetCheckLimits()
    {
        if (_useIA)  return new InitialPositionCheckLimits(shipMediator.transform, 2f);
        
        return new ViewportCheckLimits(shipMediator.transform, Camera.main);
        
    }

    private IInput GetInput()
    {
        if (_useIA) return new InputIAAdapter(shipMediator.transform, 2f);
        if (_useJoystick) return new InputJoystickAdapter(_joystick, _fireButton);
        return InputReaderAdapter.Create();
    }
}