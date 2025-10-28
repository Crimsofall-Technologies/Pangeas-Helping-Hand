using UnityEngine;

[DefaultExecutionOrder(1)]
public class CustomSceneManager : MonoBehaviour
{
    public Light sun;
    public AudioClip musicClip;

    private void Awake()
    {
        GameManager.sceneM = this;
    }
}
