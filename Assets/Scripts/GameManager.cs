using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Gravity;

[DefaultExecutionOrder(0)]
public class GameManager : MonoBehaviour
{
    #region SINGLETON

    public static GameManager Instance;
    public static UIManager ui;
    public static CustomSceneManager sceneM;
    public BGMPlayer bgmPlayer;
    public static Transform playerObject;
    public static PlayerInventory playerInventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Application.targetFrameRate = maxFramerate;
            DontDestroyOnLoad(gameObject);
	        Instance = this;
	        CanEnterEntrances = true;
            SceneManager.sceneLoaded += OnSceneLoadComplete;

            //always start in day!
            IsDay = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

	public int maxFramerate = 60;
	public float levelLoadDelay = 2f;
    public Material skyLight, skyDark;

    public bool LoadingLevel { get; private set; }
    private string sceneName;
    public bool InteractedRecent { get; private set; } //has recently interacted with something?
    public bool IsDay { get; set; }
	public bool EnteredEntrance { get; private set; }
	public bool CanEnterEntrances { get; set; }

    public int points { get; private set; }
    public void AddPoints(int amount)
    {
        points += amount;
        ui.UpdateUI();
    }
    public void RemovePoints(int amount)
    {
        points -= amount;
        ui.UpdateUI();
    }

    public void SetDayNightValue(bool value)
    {
        IsDay = value;

        //set day/night sun light
        SetupSceneLight();
    }

    public void OnEnteredEntrance()
    {
        EnteredEntrance = true;
        Invoke(nameof(ResetEntrance), 0.5f);
    }

    private void ResetEntrance()
    {
        EnteredEntrance = false;
    }

    public void Interacted()
    {
        if (!InteractedRecent)
        {
            InteractedRecent = true;
            Invoke(nameof(ResetInteraction), 1f);
        }
    }

    private void ResetInteraction()
    {
        InteractedRecent = false;
    }

    private void OnSceneLoadComplete(Scene arg0, LoadSceneMode arg1)
    {
        LoadingLevel = false;

        //set day/night sun light
        Invoke(nameof(SetupSceneLight), 0.5f);
        Invoke(nameof(LoadPlayerData), 0.5f);
        //DynamicGI.UpdateEnvironment();
    }

    private void SetupSceneLight()
    {
        if (IsDay)
        {
            if (sceneM != null)
            {
                if (sceneM.sun)
                    sceneM.sun.intensity = 1f;
                bgmPlayer.PlayMusic(sceneM.musicClipDay);
                
                //enable/disable night/day objects from scene
                for (int i = 0; i < sceneM.dayObjects.Length; i++)
                    sceneM.dayObjects[i].SetActive(true);
                for (int i = 0; i < sceneM.nightObjects.Length; i++)
                    sceneM.nightObjects[i].SetActive(false);
            }
            RenderSettings.skybox = skyLight;

        }
        else
        {
            if (sceneM != null)
            {
                if (sceneM.sun)
                    sceneM.sun.intensity = 0f;
                if (sceneM.musicClipNight != null)
                    bgmPlayer.PlayMusic(sceneM.musicClipNight);
                else
                    bgmPlayer.PlayMusic(sceneM.musicClipDay);

                //enable/disable night/day objects from scene
                for (int i = 0; i < sceneM.dayObjects.Length; i++)
                    sceneM.dayObjects[i].SetActive(false);
                for (int i = 0; i < sceneM.nightObjects.Length; i++)
                    sceneM.nightObjects[i].SetActive(true);
            }
            RenderSettings.skybox = skyDark;
        }
    }

    public void SetupMusic(AudioClip dayClip, AudioClip nightClip)
    {
        if(IsDay) bgmPlayer.PlayMusic(dayClip);
        else bgmPlayer.PlayMusic(nightClip);

        //apply to scene manager for future use
        if (sceneM)
        {
            sceneM.musicClipDay = dayClip;
            sceneM.musicClipNight = nightClip;
        }
    }

    public void LoadLevel(string Name)
    {
        //save player data!
        SavePlayerData();

        LoadingLevel = true;
        sceneName = Name;
        sceneM = null;
        if (ui != null)
            ui.loadingAnimator.SetBool("Open", true);
	    Invoke(nameof(DelayedLoadLevel), levelLoadDelay);
    }

    private void DelayedLoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SavePlayerData()
    {
        PlayerData data = new PlayerData()
        {
            inventoryData = playerInventory.GetAllItems(), 
            points = points,
        };
        PlayerDataSaver.SaveData(data);
    }

    private void LoadPlayerData()
    {
        PlayerData data = PlayerDataSaver.LoadData();

        if (data != null) {
            playerInventory.ApplyLoadedData(data);
            points = data.points;
        }

        playerObject.GetComponentInChildren<GravityProvider>().useGravity = true;
        ui.UpdateUI();
    }
}
