using UnityEngine;

[DefaultExecutionOrder(1)]
public class CustomSceneManager : MonoBehaviour
{
    public Light sun;

    public GameObject[] dayObjects, nightObjects;

    public AudioClip musicClipDay, musicClipNight;

    private void Awake()
    {
        GameManager.sceneM = this;
    }
}
