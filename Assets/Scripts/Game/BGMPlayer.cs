using System.Collections;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public AudioSource source;
    public float replayLoop = 3f;
    public float crossfadeDuration = 1.5f; // Duration of the crossfade in seconds
    private Coroutine loopCoroutine;

    // Stops and plays the new music with crossfade
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
            return;

        // If the same clip is already playing, do nothing
        if (source.clip != null && clip.name == source.clip.name)
            return;

        // Stop any existing loop coroutine
        if (loopCoroutine != null)
        {
            StopCoroutine(loopCoroutine);
        }

        // Start the crossfade coroutine
        StartCoroutine(CrossfadeToNewClip(clip));
    }

    public void StopMusic()
    {
        // Stop any existing loop coroutine
        if (loopCoroutine != null)
        {
            StopCoroutine(loopCoroutine);
            loopCoroutine = null;
        }
        
        source.Stop();
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

        // Start the looping coroutine
        loopCoroutine = StartCoroutine(LoopMusic());
    }

    private IEnumerator LoopMusic()
    {
        while (source.clip != null)
        {
            // Wait for the audio to finish playing
            yield return new WaitUntil(() => !source.isPlaying);
            
            // Wait for the replay delay
            yield return new WaitForSeconds(replayLoop);
            
            // Replay the music if we still have a clip
            if (source.clip != null)
            {
                Replay();
            }
        }
    }

    private void Replay()
    {
        source.Play();
    }
}