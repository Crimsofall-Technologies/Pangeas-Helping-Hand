using UnityEngine;

public class JumpCanceler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<XRJump_Crouch>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<XRJump_Crouch>().enabled = true;
        }
    }
}
