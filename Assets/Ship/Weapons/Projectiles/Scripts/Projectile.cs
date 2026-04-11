using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    

    #region Unity Methods

    private void Awake()
    {
        InitRigidbody();
        Invoke(nameof(DestroyProjectile), _lifeTime);
    }

    private void Start()
    {
        Move();
    }

    #endregion
    
    
    #region Rigidbody

    private Rigidbody2D _rb;

    private void InitRigidbody()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    #endregion


    #region Data
    [SerializeField] private ProjectileId _id;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lifeTime = 4f;

    public string Id => _id.Value;
    
    public enum EProjectileType
    {
        Red, Green
    }

    #endregion

    #region Movement
    
    private void Move()
    {
        _rb.linearVelocity = transform.up * _speed;
    }

    #endregion


    #region Destroy

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    #endregion

}
