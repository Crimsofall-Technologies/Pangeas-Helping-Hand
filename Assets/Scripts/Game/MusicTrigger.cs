using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioClip outMusicDay, outMusicNight, inMusicDay, inMusicNight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetupMusic(inMusicDay, inMusicNight);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetupMusic(outMusicDay, outMusicNight);
        }
    }
}
