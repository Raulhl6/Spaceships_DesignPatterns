using System;
using UnityEngine;

/** Template method **/
[RequireComponent(typeof(Rigidbody2D))]
public abstract class ProjectileBase : MonoBehaviour, IProjectile, IDamageable
{
    [SerializeField] protected ProjectileId _id;
    [SerializeField] protected float _speed = 5f;
    [SerializeField] protected float _lifeTime = 4f;

    protected Transform _transform;
    public string Id => _id.Value;
    public ETeams Team { get; private set; }


    public virtual void Configure(ETeams team)
    {
        Team = team;
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
        if (!other.TryGetComponent(out IDamageable damageable)) return;

        if (damageable.Team == Team) return;
        
        CancelInvoke(nameof(DestroyProjectile));
        damageable.AddDamage(100);
    }

    #endregion
    
    private void DestroyProjectile()
    {
        DoDestroy();
        Destroy(gameObject);
    }

    

    public void AddDamage(int amount)
    {
        DestroyProjectile();
    }
}
