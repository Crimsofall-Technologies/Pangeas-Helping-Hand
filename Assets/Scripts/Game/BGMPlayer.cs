using UnityEngine;

[DefaultExecutionOrder(0)]
public class BGMPlayer : MonoBehaviour
{
    public AudioSource source;
    public float replayLoop = 3f;

    //stops and plays the new music
    public void PlayMusic(AudioClip clip)
    {
        if(clip == null)
            return;
        
        //similar?
        if(source.clip != null && clip.name == source.clip.name)
            return;

        source.clip = clip;
        ReallyPlay();
    }

    private void Replay()
    {
        source.Play();
        ReallyPlay();
    }

    private void ReallyPlay()
    {
        source.Play();
        CancelInvoke(nameof(Replay));
        Invoke(nameof(Replay), source.clip.length + replayLoop);
    }

    /*private IEnumerator Loop()
    {
        while(source.isPlaying)
        {
            yield return null;
        }

        yield return new WaitForSeconds(replayLoop);
        PlayMusic(source.clip);
    }*/
}
