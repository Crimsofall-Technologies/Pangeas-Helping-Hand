using UnityEngine;
using UnityEngine.InputSystem;

public class LaserGun : MonoBehaviour
{
    public InputActionProperty inputActionLeft, inputActionRight;
	public float laserDestroyDelay = 0.5f;
    public GameObject laserObject;
    public Transform spawnPoint;
	public ParticleSystem muzzleEffect;

	private bool Held;
    //private float time;

    private void Update()
	{
		if (!Held) return;
		//if(Time.time < time) return;

		if (inputActionLeft.action.WasPressedThisFrame() ||
			inputActionRight.action.WasPressedThisFrame())
		{
			ShootLaser();
			//time = Time.time + laserDestroyDelay + 0.15f;
		}
	}
	
	public void OnHoldChange(bool _held)
	{
		Held = _held;
	}

	private void ShootLaser()
    {
		muzzleEffect.Play();
        GameObject g = Instantiate(laserObject, spawnPoint.position, Quaternion.LookRotation(spawnPoint.forward));
		Destroy(g, laserDestroyDelay);
    }
}
