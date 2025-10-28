using UnityEngine;

public class BoyTopTrigger : MonoBehaviour
{
    public BoyClimb boyClimb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            boyClimb.OnPlayerReachedTop();
        }
    }
}
