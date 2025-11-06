using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon;

    public Item myItem { get; private set; }
    private PlayerInventory inventory;
    public int myIndex { get; private set; }

    public void Init(PlayerInventory inv, int index) {
        myIndex = index;
        inventory = inv;
        Clear();
    }

    public void Fill(Item item) 
    {
        myItem = item;
        icon.sprite = GameManager.playerInventory.itemIcons[myItem.iconIndex];
        icon.gameObject.SetActive(true);
    }

    public void Clear() 
    {
        myItem = new();
        icon.sprite = null;
        icon.gameObject.SetActive(false);
    }

    public void OnClick() {
        //use the item kept in this slot:
        if(myItem.InstanceActive)
            inventory.TryUseItem(myIndex);
        else
            inventory.TryAddPickedItem(myIndex);
    }
}
