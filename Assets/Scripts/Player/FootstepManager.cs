using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Gravity;

public class FootstepManager : MonoBehaviour
{
	public XRMovementTracker movementTracker;
	public GravityProvider gravity;
	public AudioClip concreteSteps, grassStep, woodStep, waterStep;
	public float stepDelay = 0.25f;
	
	public float radius;
	public LayerMask mask;

	private float time;
	private string lastFootstepTag;
	private AudioSource source;
	
	private void Start() 
	{
		source = GetComponent<AudioSource>();
		source.clip = concreteSteps;
	}
	
	private void Update()
	{
		//really moving? play footsteps
		if(movementTracker.IsMoving && gravity.isGrounded)
		{
			PlayFootsteps();
		}
		
		//detect footsteps
		if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, radius, mask, QueryTriggerInteraction.Ignore))
		{
			if(hit.collider.tag == "Untagged")
				return;
				
			string t = lastFootstepTag;
			
			if(hit.collider.tag == "Concrete") {
				source.clip = concreteSteps;
			}
			if(hit.collider.tag == "Wood") {
				source.clip = woodStep;
			}
			if(hit.collider.tag == "Grass") {
				source.clip = grassStep;
			}
			if(hit.collider.tag == "Water") {
				source.clip = waterStep;
			}
			
			lastFootstepTag = hit.collider.tag;
			if(t != lastFootstepTag) 
			{
				source.Stop();
			}
		}
	}

	public void PlayFootsteps()
	{
		if(Time.time >= time)
		{
			source.PlayOneShot(source.clip, source.volume);
			time = Time.time + stepDelay;
		}
	}
	
	// Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected.
	protected void OnDrawGizmosSelected() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
	
	// OnControllerColliderHit is called when the controller hits a collider while performing a Move.
	/*private void OnTriggerEnter(Collider hit) {
		if(hit.tag == "Untagged") return;
			if(hit.tag == "Concrete") {
				source.clip = concreteSteps;
			}
			if(hit.tag == "Wood") {
				source.clip = woodStep;
			}
			if(hit.tag == "Grass") {
				source.clip = grassStep;
			}
			if(hit.tag == "Water") {
				source.clip = waterStep;
			}
	}*/
}
