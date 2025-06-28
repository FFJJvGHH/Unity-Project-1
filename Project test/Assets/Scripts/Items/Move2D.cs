using UnityEngine;

public class Move2D : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    void Start() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        // 可以自由移动
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}