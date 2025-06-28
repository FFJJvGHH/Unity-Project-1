using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class SyncXSpeedToAnimator : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public int FacingDir = 1;
    public bool FacingRight = true;
    private float intputx;
    public Transform transform;
    [Header("Animator 中的 float 参数名")]
    public string speedXParameter = "speed";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        intputx = Input.GetAxisRaw("Horizontal");
        if (FacingRight && intputx < 0)
        {
            Flip();
        }
        else if(!FacingRight && intputx > 0)
        {
            Flip();
        }
        float speedX = Mathf.Abs(rb.velocity.x); // 同步绝对值，防止负数影响状态机
        animator.SetFloat(speedXParameter, speedX);
    }
    public virtual void Flip()
    {
        FacingDir *= -1;
        FacingRight = !FacingRight;
        transform.Rotate(0, 180, 0);
    }
}

