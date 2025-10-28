using UnityEngine;

public class WristWatch : MonoBehaviour
{
    public string homeSceneName;
    public float timeToHold = 5f;

    private bool held = false;
    private float heldTime = 0f;

    private void Update()
    {
        if(GameManager.Instance.LoadingLevel)
            return;

        if(held)
        {
            heldTime += Time.deltaTime;

            if(heldTime>=timeToHold)
            {
                GameManager.Instance.LoadLevel(homeSceneName);
                held = false;
            }
        }
    }

    public void OnHold()
    {
        held = true;
    }

    public void OnLetGo()
    {
        held = false;
        heldTime = 0f;
    }
}
