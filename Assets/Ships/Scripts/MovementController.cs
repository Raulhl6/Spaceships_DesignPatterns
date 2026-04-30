using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    
    private Vector2 _speed;
    private IShip _ship;
    private Rigidbody2D _rb;
    private ICheckLimits _checkLimits;
    private Vector2 _currentPosition;
    

    public void Configure(IShip ship, ICheckLimits checkLimits, Vector2 speed)
    {
        _ship = ship;
        _checkLimits = checkLimits;
        _rb = ship.GetTransform().GetComponent<Rigidbody2D>();
        _speed = speed;
        _currentPosition = _rb.position;
    }
    
    
    public void Move(Vector2 direction)
    {
        _currentPosition += direction * (_speed * Time.deltaTime);
        _currentPosition = _checkLimits.ClampFinalPosition(_currentPosition);
        _rb.MovePosition(_currentPosition);
    }

}

