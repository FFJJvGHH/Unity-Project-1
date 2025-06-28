using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HealthController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Slider healthSlider;
    public Color controllingColor = Color.red;

    private RectTransform rectTransform;
    private Transform currentTarget; // 存储Transform而不是ObjectAnchor
    private Image healthBarImage;
    private bool isDragging;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        healthBarImage = healthSlider.fillRect.GetComponent<Image>();

        if (Camera.main.GetComponent<Physics2DRaycaster>() == null)
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        ReleaseControl(); // 拖动开始时立即释放控制
    }

    private void ReleaseControl()
    {
        if (currentTarget != null)
        {
            // 关键修复：确保正确移除脚本
            var move = currentTarget.GetComponent<Move2D>();
            if (move != null) Destroy(move);

            var playerMove = currentTarget.GetComponent<PlayerMove2D>();
            if (playerMove != null) Destroy(playerMove);

            healthBarImage.color = Color.green;
            currentTarget = null;

            Debug.Log("已移除移动脚本"); // 调试用
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, rectTransform.position);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(screenPos), Vector2.zero);

        if (hit.collider != null && hit.collider.TryGetComponent<ObjectAnchor>(out var anchor))
        {
            currentTarget = anchor.transform;
            healthBarImage.color = controllingColor;

            // 移除旧的移动组件
            Destroy(hit.collider.GetComponent<Move2D>());
            Destroy(hit.collider.GetComponent<PlayerMove2D>());

            // 添加正确的移动脚本
            if (anchor.isPlayer)
            {
                var playerMove = hit.collider.gameObject.AddComponent<PlayerMove2D>();
                playerMove.moveSpeed = anchor.moveSpeed;
                playerMove.jumpForce = anchor.jumpForce;
          

            }
            else
            {
                var move = hit.collider.gameObject.AddComponent<Move2D>();
                move.speed = anchor.moveSpeed;
            }
        }
    }

    void LateUpdate()
    {
        if (!isDragging && currentTarget != null)
        {
            ObjectAnchor anchor = currentTarget.GetComponent<ObjectAnchor>();
            if (anchor != null)
            {
                Vector2 worldPos = (Vector2)currentTarget.position + anchor.healthBarOffset;
                Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    (RectTransform)rectTransform.parent,
                    screenPos,
                    null,
                    out Vector2 localPos
                );
                rectTransform.anchoredPosition = localPos;
            }
        }
    }
}