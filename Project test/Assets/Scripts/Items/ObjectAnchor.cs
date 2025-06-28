using UnityEngine;

public class ObjectAnchor : MonoBehaviour
{
    [Header("血条设置")]
    public Vector2 healthBarOffset = new Vector2(0, 1f);

    [Header("移动设置")]
    public float moveSpeed = 5f;
    public bool isPlayer = false;
    public float jumpForce = 8f; // 仅玩家有效

    [Header("Gizmos")]
    public bool showOffsetGizmo = true;

    void OnDrawGizmos()
    {
        if (showOffsetGizmo)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere((Vector2)transform.position + healthBarOffset, 0.1f);
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + healthBarOffset);
        }
    }
}