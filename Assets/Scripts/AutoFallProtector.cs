using UnityEngine;

public class AutoFallProtector : MonoBehaviour
{
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if(transform.position.y <= -50f)
        {
            ResetPos();
        }   
    }

    public void DelayedReset(){
        Invoke(nameof(ResetPos), 3f);     
    }

    private void ResetPos()
    {
        transform.position = startPos;
    }
}
