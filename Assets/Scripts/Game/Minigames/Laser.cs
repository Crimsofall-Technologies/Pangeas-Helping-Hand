using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform checkPoint;
    public GameObject decalEffect;
    public float laserLength = 4f;
    public float speed = 10f;

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
        // Move the entire laser object forward
        transform.position += transform.forward * speed * Time.deltaTime;

        /*if (Physics.SphereCast(checkPoint.position, 0.25f, transform.forward, out RaycastHit hit))
        {
            if (hit.collider.tag != "Player" && hit.collider.tag != "Laser")
            {
                Destroy(gameObject);
                Destroy(Instantiate(decalEffect, hit.point, Quaternion.LookRotation(-hit.normal)), 1f);
            }
        }*/
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(checkPoint.position, 0.25f);
    }*/
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Laser" && other.tag != "NoCollision")
        {
            Destroy(gameObject);
            Destroy(Instantiate(decalEffect, checkPoint.position, Quaternion.identity), 1f);
        }
    }
}
