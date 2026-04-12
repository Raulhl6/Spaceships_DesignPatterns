
public class LinearProjectile : ProjectileBase
{
    
    protected override void DoStart()
    {
        _rb.linearVelocity = _transform.up * _speed;
    }

    protected override void DoMove() {}

    protected override void DoDestroy()
    {
        
    }
}
