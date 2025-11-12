using UnityEngine;

public class SimpleFollower : MonoBehaviour
{
	public Transform toFollow;
	public Vector3 offset;

	private void Start() {
		transform.parent = null;
	}
	
	private void Update() {
		if(toFollow != null)
		{
			Vector3 pos = toFollow.position;
			transform.position = pos; 
		}
	}
}
