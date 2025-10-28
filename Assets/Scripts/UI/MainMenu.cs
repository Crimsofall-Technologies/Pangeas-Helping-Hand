using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainmenuUI, settingsUI, creditsUI;
    public Slider musicSlider, soundSlider;

    public AudioMixer soundMixer, musicMixer;

    public AudioClip musicClip;
	public Text versionText;

    private float musicLevel, soundLevel;

    private void Start()
    {
        //load and apply settings from disk!
        musicLevel = PlayerPrefs.GetFloat("musicVol", 0f);
        soundLevel = PlayerPrefs.GetFloat("soundVol", 0f);
        musicMixer.SetFloat("Vol", musicLevel);
        soundMixer.SetFloat("Vol", soundLevel);

        //update UI too
        musicSlider.value = musicLevel;
        soundSlider.value = soundLevel;
		
		versionText.text = "v."+Application.version;
        
        GameManager.Instance.bgmPlayer.PlayMusic(musicClip);
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        mainmenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void Credits()
    {
        mainmenuUI.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void Back()
    {
        settingsUI.SetActive(false);
        creditsUI.SetActive(false);
        mainmenuUI.SetActive(true);

        //save settings to disk for loading later!
        PlayerPrefs.SetFloat("musicVol", musicLevel);
        PlayerPrefs.SetFloat("soundVol", soundLevel);
    }

    public void SetMusic(float value)
    {
        musicMixer.SetFloat("Vol", value);
        musicLevel = value;
    }

    public void SetSound(float value)
    {
        soundMixer.SetFloat("Vol", value);
        soundLevel = value;
    }

    private void OnApplicationQuit()
    {
        //save settings to disk for loading later!
        PlayerPrefs.SetFloat("musicVol", musicLevel);
        PlayerPrefs.SetFloat("soundVol", soundLevel);
    }
}
