using UnityEngine;

public class ColliderDisabler : MonoBehaviour
{
	public Collider col; //disabled on trigger exit, enabled on trigger enter
	
	private void OnTriggerEnter(Collider other) {
		if(other.tag == "Player")
		{
			col.enabled = true;
		}
	}
	
	private void OnTriggerExit(Collider other) {
		if(other.tag == "Player")
		{
			col.enabled = false;
		}
	}
}
