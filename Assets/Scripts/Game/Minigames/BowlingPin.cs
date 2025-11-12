using UnityEngine;

public class BowlingPin : MonoBehaviour
{
    public Transform[] pins;
    public GameObject ball;
	public float resetTime = 5f;

    private Vector3[] positions;
    private Quaternion[] rotations;
	
	private Vector3 ballPos;
	private Quaternion ballRot;
	private bool hidden = false;

    private void Start()
    {
		ballPos = ball.transform.position;
		ballRot = ball.transform.rotation;
		
        positions = new Vector3[pins.Length];
        rotations = new Quaternion[pins.Length];
        for (int i = 0; i < pins.Length; i++)
        {
            positions[i] = pins[i].position;
            rotations[i] = pins[i].rotation;
        }
    }
	
	private void OnTriggerEnter(Collider col) {
		if(hidden) return;
		
		//hide all pins and ball and reset them after a few seconds!
		if(col.tag == "Ball") {
			//for (int i = 0; i < pins.Length; i++)
			//	pins[i].gameObject.SetActive(false);
			
			ball.SetActive(false);
			Invoke(nameof(ResetBall), 2f);
			Invoke(nameof(ResetPins), resetTime);
			hidden = true;
		}
	}

	//resets pins and ball at their starting positions/rotations!
	private void ResetPins()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].position = positions[i];
            pins[i].rotation = rotations[i];
			pins[i].gameObject.SetActive(true);
            Rigidbody rb = pins[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
		
		hidden = false;
    }
    
	private void ResetBall()
	{
		if (ball != null)
		{
			ball.transform.position = ballPos; // Example starting position
			ball.transform.rotation = ballRot;
			ball.SetActive(true);
			Rigidbody ballRb = ball.GetComponent<Rigidbody>();
			if (ballRb != null)
			{
				ballRb.linearVelocity = Vector3.zero;
				ballRb.angularVelocity = Vector3.zero;
			}
		}
	}
}
