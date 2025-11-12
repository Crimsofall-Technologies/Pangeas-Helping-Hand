using UnityEngine;

public class XRMovementTracker : MonoBehaviour
{
	[Tooltip("Assign the XR Camera (center eye) or XR Origin transform)")]
	public Transform trackedTransform;
	[Tooltip("Speed (units/sec) above which player is considered moving")]
	public float moveThreshold = 0.05f;

	Vector3 prevPos;
	public bool IsMoving { get; private set; }
	public float Speed { get; private set; }

	private void Start()
	{
		if (trackedTransform == null) trackedTransform = Camera.main.transform;
		prevPos = trackedTransform.position;
	}

	private void Update()
	{
		Vector3 cur = trackedTransform.position;
		Vector3 delta = cur - prevPos;
		delta.y = 0f; // ignore vertical/head bob / steps
		Speed = delta.magnitude / Time.deltaTime;
		IsMoving = Speed > moveThreshold;
		prevPos = cur;
	}
}
