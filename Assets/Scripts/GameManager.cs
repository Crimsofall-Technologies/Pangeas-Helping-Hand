using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Material skyLight, skyDark;

    public bool LoadingLevel { get; private set; }
    private string sceneName;
    public bool InteractedRecent { get; private set; } //has recently interacted with something?
    public bool IsDay { get; set; }
    public bool EnteredEntrance { get; private set; }

    public void SetDayNightValue(bool value)
    {
        IsDay = value;

        //set day/night sun light
        SetupSceneLight();
        //DynamicGI.UpdateEnvironment();
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
                bgmPlayer.PlayMusic(sceneM.musicClip);
            }
            RenderSettings.skybox = skyLight;
        }
        else
        {
            if (sceneM != null)
            {
                if (sceneM.sun)
                    sceneM.sun.intensity = 0f;
                bgmPlayer.PlayMusic(sceneM.musicClip);
            }
            RenderSettings.skybox = skyDark;
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
        Invoke(nameof(DelayedLoadLevel), 1f);
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
        };
        PlayerDataSaver.SaveData(data);
    }

    private void LoadPlayerData()
    {
        PlayerData data = PlayerDataSaver.LoadData();

        if (data != null) {
            playerInventory.ApplyLoadedData(data);
        }
    }
}
