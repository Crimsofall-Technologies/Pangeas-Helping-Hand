using UnityEngine;

public class PaintBrush : MonoBehaviour
{
	public GameObject shapePrefab;   // Assign a prefab (circle, square, etc.)
	public float spacing = 0.1f, distance;     // Distance between stamps
	public bool IsDrawing = true;    // Toggle drawing on/off
	public Color currentColor;
	public LayerMask canvasMask;
	public Transform rayPoint;

	private float lastTime;

	private void Update()
	{
		// Only draw if IsDrawing is true
		if (IsDrawing && Time.time >= lastTime)
		{
			// Cast a ray downward from the brush to detect the canvas
			Ray ray = new Ray(rayPoint.position, rayPoint.forward);

			if (Physics.Raycast(ray, out RaycastHit hit, distance, canvasMask))
			{
				// Align shape with canvas surface using hit.normal
				Quaternion rotation = Quaternion.LookRotation(Vector3.up, hit.normal);

				// Spawn shape at hit point
				Instantiate(shapePrefab, hit.point + hit.normal, Quaternion.LookRotation(-hit.normal)).GetComponent<SpriteRenderer>().color = currentColor;
			}
			
			lastTime = Time.time + spacing;
		}
	}
	
	public void OnPicked(bool value)
	{
		if(value) IsDrawing = true;
		else IsDrawing = false;
	}
}
