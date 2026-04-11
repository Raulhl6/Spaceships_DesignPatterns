using UnityEngine;

public class InputJoystickAdapter : IInput
{
    private readonly Joystick _joystick;
    private readonly JoyButton _fireButton;
    
    public InputJoystickAdapter(Joystick joystick, JoyButton fireButton)
    {
        _joystick = joystick;
        _fireButton = fireButton;
        _joystick.gameObject.SetActive(true);
        _fireButton.gameObject.SetActive(true);
    }
    
    public Vector2 GetMovementVector()
    {
        return new Vector2(_joystick.Horizontal, _joystick.Vertical);
    }

    public bool IsFireActionPressed()
    {
        return _fireButton.IsPressed;
    }
}
