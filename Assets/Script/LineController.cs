using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private Transform topLine;
    [SerializeField] private Transform bottomLine;
    [SerializeField] private LineRenderer lineRenderer;

    private void Update()
    {
        lineRenderer.SetPosition(0, new Vector3(topLine.position.x, topLine.position.y, 0.0f));
        lineRenderer.SetPosition(1, new Vector3(bottomLine.position.x, bottomLine.position.y, 0.0f));
    }
}
