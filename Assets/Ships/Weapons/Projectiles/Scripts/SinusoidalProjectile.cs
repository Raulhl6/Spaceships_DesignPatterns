using System;
using UnityEngine;

public class SinusoidalProjectile : ProjectileBase
{
    [SerializeField] private float _amplitude = 1;
    [SerializeField] private float _frequency = 1;
    
    private Vector3 _currentPosition;
    private float _currentTime;
    

    protected override void DoStart()
    {
        _currentTime = 0;
        _currentPosition = transform.position;
    }

    protected override void DoMove()
    {
        _currentPosition += _transform.up * (_speed * Time.deltaTime);
        var horizontalPosition = _transform.right * (_amplitude * Mathf.Sin(_currentTime * _frequency));
        _rb.MovePosition(_currentPosition + horizontalPosition);
        
        _currentTime += Time.deltaTime;
    }

    protected override void DoDestroy()
    {
        
    }
}
