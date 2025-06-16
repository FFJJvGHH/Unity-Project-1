using System.Collections;
using System.Collections.Generic;
using Autodesk.Fbx;
using UnityEngine;

public class Entity : MonoBehaviour
{

    #region Components

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    #endregion
    [Header("Knockback info")]
    [SerializeField] public Vector2 KnockbackDirection;
    public bool isKnocked;
    [SerializeField] public float knockBackDuration;  
    [Header("Coillion Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsEnemy;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponentInChildren<EntityFX>();
    }

    protected virtual void Update()
    {

    }

    public virtual bool isGroundedDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    public virtual bool isEnemyDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsEnemy);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public virtual void FilpController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }

    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitEnockBack");
        Debug.Log(gameObject.name + "was Damaged");
    }

    protected virtual IEnumerator HitEnockBack()
    {
        isKnocked = true;
        rb.velocity = new Vector2(KnockbackDirection.x*facingDir,KnockbackDirection.y);
        yield return new WaitForSeconds(knockBackDuration);
        isKnocked = false;
    }
}
