using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public static BoundaryController instance { get; private set; }
    private BoxCollider2D boxCollider;
    public float currentLeftBoundary;
    public float currentRightBoundary;
    private float originalLeftBoundary;
    private float originalRightBoundary;
    private float extraWidth = 0.2f;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        boxCollider = GetComponent<BoxCollider2D>();
        originalLeftBoundary = boxCollider.bounds.min.x + Player.instance.offset + extraWidth;
        originalRightBoundary = boxCollider.bounds.max.x + Player.instance.offset - extraWidth;
        currentLeftBoundary = originalLeftBoundary;
        currentRightBoundary = originalRightBoundary;
    }

    public void ChangeBoundary(CapsuleCollider2D planetCollider)
    {
        currentLeftBoundary = originalLeftBoundary;
        currentRightBoundary = originalRightBoundary;

        currentLeftBoundary += planetCollider.bounds.extents.x;
        currentRightBoundary -= planetCollider.bounds.extents.x;
    }

}
