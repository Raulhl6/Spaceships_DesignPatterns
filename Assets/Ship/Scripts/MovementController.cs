using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    
    private IShip _ship;
    private Transform _transform;
    private ICheckLimits _checkLimits;



    public void Configure(IShip ship, ICheckLimits checkLimits)
    {
        _ship = ship;
        _checkLimits = checkLimits;
        _transform = ship.GetTransform();
    }
    
    
    
    public void Move(Vector2 direction)
    {
        _transform.Translate(direction * (_speed * Time.deltaTime));
        _checkLimits.ClampFinalPosition();
    }

}

