using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float laserLength = 4f;
    public float speed = 10f;

    private Vector3 startPoint = Vector3.zero;
    private float zOffset = 0f;

    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        ResetLaser();
    }

    void Update()
    {
        zOffset += speed * Time.deltaTime;

        Vector3 newStart = new Vector3(0, 0, zOffset);
        Vector3 newEnd = newStart + new Vector3(0, 0, laserLength);

        lineRenderer.SetPosition(0, newStart);
        lineRenderer.SetPosition(1, newEnd);
    }

    public void ResetLaser()
    {
        zOffset = 0f;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, startPoint + new Vector3(0, 0, laserLength));
    }
}
