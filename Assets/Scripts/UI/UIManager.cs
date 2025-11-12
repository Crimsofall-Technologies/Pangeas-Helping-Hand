using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[DefaultExecutionOrder(0)]
public class UIManager : MonoBehaviour
{
    public Image hpFill;
    public Text pointsText;
    public Animator loadingAnimator;
    public GameObject playerInventoryGO;
    public ItemVar testItem;

    [Space]
    public Slider soundSlider;
    public Slider musicSlider;

    public AudioMixer soundMixer, musicMixer;

    public InputActionProperty openInventoryAction;

    public PlayerHealth pHealth { get; set; }
	
	private bool IsMenu;
    private float musicLevel, soundLevel;

    public void SetMainMenuValue(bool value)
    {
        IsMenu = value;

        gameObject.SetActive(!IsMenu);
    }

    private void Start()
    {
        if(GameManager.ui == null)
        {
            DontDestroyOnLoad(gameObject);
            GameManager.ui = this;
            loadingAnimator.SetBool("Open", false);

            musicLevel = PlayerPrefs.GetFloat("musicVol", 0f);
            soundLevel = PlayerPrefs.GetFloat("soundVol", 0f);
            musicMixer.SetFloat("Vol", musicLevel);
            soundMixer.SetFloat("Vol", soundLevel);

            soundSlider.value = soundLevel;
            musicSlider.value = musicLevel;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.playerInventory.AddItem(new(testItem));
        }

        if(openInventoryAction.action.WasPressedThisFrame())
        {
            ToggleInventory();
        }
    }

    public void UpdateUI()
    {
        hpFill.fillAmount = (float)pHealth.currentHealth / (float)pHealth.maxHealth;
        pointsText.text = "Score: "+GameManager.Instance.points + "";
    }

    public void FakeLoading()
    {
        loadingAnimator.SetBool("Open", true);
        Invoke(nameof(LoadingOff), 1f);
    }

    private void LoadingOff()
    {
        loadingAnimator.SetBool("Open", false);
    }

    public void ToggleInventory()
    {
        playerInventoryGO.SetActive(!playerInventoryGO.activeSelf);
    }

    public void SetSoundValue(float value)
    {
        soundMixer.SetFloat("Vol", value);
        soundLevel = value;
    }

    public void SetMusicValue(float value)
    {
        musicMixer.SetFloat("Vol", value);
        musicLevel = value;
    }

    public void OnSettingUiClosed()
    {
        PlayerPrefs.SetFloat("musicVol", musicLevel);
        PlayerPrefs.SetFloat("soundVol", soundLevel);
    }
}
