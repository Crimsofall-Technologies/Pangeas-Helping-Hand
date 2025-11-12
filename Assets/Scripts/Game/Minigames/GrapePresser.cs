using UnityEngine;

public class GrapePresser : MonoBehaviour
{
    public float yRemoveOffset = 0.25f;
    public float maxY;
    public Transform jarSpawnArea;
    public GameObject jarPrefab;

    private Vector3 originalPos;
    private bool inside;
	private AudioSource source;

    private void Start()
    {
        originalPos = transform.position;
		source = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Grape")
        {
            Destroy(collider.gameObject);
            float y = transform.position.y + yRemoveOffset;
            if (y >= maxY) y = maxY;
            transform.position = new Vector3(originalPos.x, y, originalPos.z);
        }
        
        if (inside) return;

        if (collider.tag == "Player")
        {
            //player jumps on this and presses it!
            transform.position = new Vector3(originalPos.x, transform.position.y - yRemoveOffset, originalPos.z);
            Instantiate(jarPrefab, jarSpawnArea.position, Quaternion.identity);
            inside = true;
			
			//play a sound
			source.PlayOneShot(source.clip, source.volume);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            inside = false;
        }
    }

    public void Reset()
    {
        transform.position = originalPos;
    }
}
