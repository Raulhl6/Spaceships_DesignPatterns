using UnityEngine;

public class InputAIAdapter : IInput
{
    private readonly Transform _ship;
    private float _currentDirectionX;
    private readonly Camera _camera;

    public InputAIAdapter(Transform ship)
    {
        _ship = ship;
        _currentDirectionX = _ship.right.x;
        _camera = Camera.main;
    }
    
    public Vector2 GetMovementVector()
    {
        //if (Mathf.Abs(_ship.transform.position.x) >= _maxDistance) _currentDirectionX *= -1;
        
        var viewPortPoint = _camera.WorldToViewportPoint(_ship.position);
        if (viewPortPoint.x is < 0.1f or > 0.9f) _currentDirectionX *= -1;
        return new Vector2(_currentDirectionX, 1);
    }

    public bool IsFireActionPressed()
    {
        return Random.Range(0f, 100) <= 20;
    }
}
