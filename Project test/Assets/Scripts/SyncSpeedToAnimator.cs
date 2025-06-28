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
    [Header("Animator �е� float ������")]
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
        float speedX = Mathf.Abs(rb.velocity.x); // ͬ������ֵ����ֹ����Ӱ��״̬��
        animator.SetFloat(speedXParameter, speedX);
    }
    public virtual void Flip()
    {
        FacingDir *= -1;
        FacingRight = !FacingRight;
        transform.Rotate(0, 180, 0);
    }
}

