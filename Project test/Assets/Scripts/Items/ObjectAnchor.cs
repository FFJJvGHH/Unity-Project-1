using UnityEngine;

public class ObjectAnchor : MonoBehaviour
{
    [Header("Ѫ������")]
    public Vector2 healthBarOffset = new Vector2(0, 1f);

    [Header("�ƶ�����")]
    public float moveSpeed = 5f;
    public bool isPlayer = false;
    public float jumpForce = 8f; // �������Ч

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