using UnityEngine;

public class WaterCan : MonoBehaviour
{
	public ParticleSystem waterParticle;
	public float pourAngle = 45f;

	private void Update()
	{
		if (!waterParticle) return;

		// Compare can's "up" vector to world up
		float angle = Vector3.Angle(transform.up, Vector3.up);
		bool shouldPour = angle > pourAngle;

		var emission = waterParticle.emission;
		emission.enabled = shouldPour;
	}
}
