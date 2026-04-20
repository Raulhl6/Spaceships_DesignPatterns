using System;
using UnityEngine;

/** Template method **/
[RequireComponent(typeof(Rigidbody2D))]
public abstract class ProjectileBase : MonoBehaviour, IProjectile
{
    
    [SerializeField] protected ProjectileId _id;
    [SerializeField] protected float _speed = 5f;
    [SerializeField] protected float _lifeTime = 4f;

    protected Transform _transform;
    public string Id => _id.Value;
    
    private void DestroyProjectile()
    {
        DoDestroy();
        Destroy(gameObject);
    }

    #region Abstract Methods

    protected abstract void DoStart();
    protected abstract void DoMove();
    protected abstract void DoDestroy();

    #endregion


    #region Unity Methods

    private void Start()
    {
        _transform = transform;
        InitRigidbody();
        Invoke(nameof(DestroyProjectile), _lifeTime);
        DoStart();
    }

    private void FixedUpdate()
    {
        DoMove();
    }

    #endregion
    
    
    #region Rigidbody

    protected Rigidbody2D _rb;

    private void InitRigidbody()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    #endregion


    #region Collisions

    private void OnTriggerEnter2D(Collider2D other)
    {
        CancelInvoke(nameof(DestroyProjectile));
        DestroyProjectile();
    }

    #endregion

}
