using UnityEngine;

public class Laser : MonoBehaviour
{
    public bool showDebug = false;
    public LineRenderer lineRenderer;
    public Transform checkPoint;
    public GameObject decalEffect;
    public float laserLength = 4f;
    public float speed = 10f;

    private bool done;

    private void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;

        // Local positions from start to end of the beam
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.forward * laserLength);

        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
		if(lineRenderer.enabled == false) return;
		
        // Move the entire laser object forward
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //ignore triggers
        if(other.isTrigger || done) return;

        if (other.tag != "Player" && other.tag != "Laser" && other.tag != "NoCollision")
        {
            if(showDebug)
                Debug.Log("Laser hit: " + other.name);

			lineRenderer.enabled = false;
            Destroy(gameObject, 5f);
            Destroy(Instantiate(decalEffect, checkPoint.position, Quaternion.identity), 1f);

            done = true;

            MovingBoards boards = other.GetComponent<MovingBoards>();
            if(boards!=null)
            {
                //add/remove points!
                if(boards.removePoints)
                    GameManager.Instance.RemovePoints(boards.points);
                else
                    GameManager.Instance.AddPoints(boards.points);

                boards.animator.SetBool("Hit", true);
            }
        }
    }
}
