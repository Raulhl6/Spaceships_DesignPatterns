using UnityEngine;

public class InputIAAdapter : IInput
{
    private readonly Transform _ship;
    private int _currentDirectionX;
    private readonly Camera _camera;
    private float _maxDistance;

    public InputIAAdapter(Transform ship, float maxDistance)
    {
        _ship = ship;
        _currentDirectionX = 1;
        _camera = Camera.main;
        _maxDistance = maxDistance;
    }
    
    public Vector2 GetMovementVector()
    {
        if (Mathf.Abs(_ship.transform.position.x) >= _maxDistance) _currentDirectionX *= -1;
        
        /*var viewPortPoint = _camera.WorldToViewportPoint(_ship.position);
        if (viewPortPoint.x is < 0.1f or > 0.9f) _currentDirectionX *= -1;*/
        return new Vector2(_currentDirectionX, 0);
    }

    public bool IsFireActionPressed()
    {
        return Random.Range(0f, 100) <= 20;
    }
}
