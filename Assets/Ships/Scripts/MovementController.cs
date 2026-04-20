using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    
    private Vector2 _speed;
    private IShip _ship;
    private Transform _transform;
    private ICheckLimits _checkLimits;

    
    public void Configure(IShip ship, ICheckLimits checkLimits, Vector2 speed)
    {
        _ship = ship;
        _checkLimits = checkLimits;
        _transform = ship.GetTransform();
        _speed = speed;
    }
    
    
    public void Move(Vector2 direction)
    {
        _transform.Translate(direction * (_speed * Time.deltaTime));
        _checkLimits.ClampFinalPosition();
    }

}

