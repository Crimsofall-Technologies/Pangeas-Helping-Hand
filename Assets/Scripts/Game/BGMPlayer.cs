using System.Collections;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public AudioSource source;
    public float replayLoop = 3f;
    public float crossfadeDuration = 1.5f; // Duration of the crossfade in seconds

    // Stops and plays the new music with crossfade
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
            return;

        // If the same clip is already playing, do nothing
        if (source.clip != null && clip.name == source.clip.name)
            return;

        // Start the crossfade coroutine
        StartCoroutine(CrossfadeToNewClip(clip));
    }

    private IEnumerator CrossfadeToNewClip(AudioClip newClip)
    {
        // Fade out the current music
        float startVolume = source.volume;
        for (float t = 0; t < crossfadeDuration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVolume, 0, t / crossfadeDuration);
            yield return null;
        }
        source.volume = 0;

        // Switch to the new clip
        source.clip = newClip;
        source.Play();

        // Fade in the new music
        for (float t = 0; t < crossfadeDuration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(0, startVolume, t / crossfadeDuration);
            yield return null;
        }
        source.volume = startVolume;
    }

    private void Replay()
    {
        source.Play();
        ReallyPlay();
    }

    private void ReallyPlay()
    {
        source.Play();
    }
}