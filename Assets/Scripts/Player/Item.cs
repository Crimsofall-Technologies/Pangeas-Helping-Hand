using UnityEngine;

[System.Serializable]
public class Item
{
    public bool InstanceActive = false;
    public string itemName;
    public int iconIndex, prefabIndex;

    public Item() {}

    public Item(ItemVar var) {
        InstanceActive = true;
        itemName = var.itemName;
        iconIndex = GameManager.playerInventory.itemIcons.IndexOf(var.Icon);
        prefabIndex = GameManager.playerInventory.itemPrefabs.IndexOf(var.pickupObject);
    }
}
