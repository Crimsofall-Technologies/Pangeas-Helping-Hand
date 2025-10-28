using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[DefaultExecutionOrder(0)]
public class UIManager : MonoBehaviour
{
    public Image hpFill;
    public Animator loadingAnimator;
    public GameObject playerInventoryGO;
    public ItemVar testItem;

    public InputActionProperty openInventoryAction;

    public PlayerHealth pHealth { get; set; }
	
	private bool IsMenu;

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

    public void OnChangeDialougeUI(bool value)
    {
        
    }
}
