using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(0)]
public class PlayerInventory : MonoBehaviour
{
    public GameObject[] slotsPages;
    public Transform cameraT;

    public List<Sprite> itemIcons = new();
    public List<GameObject> itemPrefabs = new();
    public Button nextButton, prevButton;
    public int maxPages;

    private int currentPage = 0;
    public Slot[] slots;

    private void Start()
    {
        GameManager.playerInventory = this;

        //start at page 0
        currentPage = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Init(this, i);
        }

        prevButton.interactable = false;
        nextButton.interactable = true;

        slotsPages[0].SetActive(true);
        slotsPages[1].SetActive(false);
        slotsPages[2].SetActive(false);
    }

    public Item[] GetAllItems()
    {
        Item[] items = new Item[slots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            items[i] = slots[i].myItem;
        }
        return items;
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].myItem.InstanceActive)
            {
                slots[i].Fill(item);
                return true;
            }
        }

        Debug.Log("Inventory Full!");
        return false;
    }

    //will not replace already exsisting items
    public bool AddItem(Item item, int index)
    {
        if (!slots[index].myItem.InstanceActive)
        {
            slots[index].Fill(item);
            return true;
        }

        return false;
    }

    public Item RemoveItem(int index)
    {
        if (slots[index].myItem.InstanceActive)
        {
            Item i = slots[index].myItem;
            slots[index].Clear();
            return i;
        }

        return null;
    }

    public void TryUseItem(int index)
    {
        if (!slots[index].myItem.InstanceActive)
            return;

        Debug.Log("Using item: " + slots[index].myItem.itemName);

        //spawn the item near player to pick and use!
        Vector3 playerForward = cameraT.forward;
        Instantiate(itemPrefabs[slots[index].myItem.prefabIndex], cameraT.position + playerForward * 2f, Quaternion.LookRotation(playerForward));

        RemoveItem(index);
    }

    public void TryAddPickedItem(int index)
    {
        //get the object in either left or right hand!
        PlayerHandObjectTracker handTracker = GameManager.playerObject.GetComponent<PlayerHandObjectTracker>();
        GameObject left = handTracker.GetLeftHandObject(), right = handTracker.GetRightHandObject();

        if (left != null)
        {
            Pickup pick = left.GetComponent<Pickup>();
            if (pick != null && AddItem(new(pick.itemVar), index))
                Destroy(left);
        }

        if (right != null)
        {
            Pickup pick = right.GetComponent<Pickup>();
            if (pick != null && AddItem(new(pick.itemVar), index))
                Destroy(right);
        }
    }

    public void NextPage()
    {
        slotsPages[currentPage].SetActive(false);
        currentPage++;
        if (currentPage >= maxPages - 1)
        {
            currentPage = maxPages - 1;
            prevButton.interactable = true;
            nextButton.interactable = false;
        }
        else
        {
            prevButton.interactable = true;
            nextButton.interactable = true;
        }

        slotsPages[currentPage].SetActive(true);
    }

    public void PrevPage()
    {
        slotsPages[currentPage].SetActive(false);
        currentPage--;
        if (currentPage <= 0)
        {
            currentPage = 0;
            prevButton.interactable = false;
            nextButton.interactable = true;
        }
        else
        {
            prevButton.interactable = true;
            nextButton.interactable = true;
        }
        slotsPages[currentPage].SetActive(true);
    }

    public void ApplyLoadedData(PlayerData data)
    {
        for (int i = 0; i < data.inventoryData.Length; i++)
        {
            if (data.inventoryData[i].InstanceActive)
                slots[i].Fill(data.inventoryData[i]);
            else
                slots[i].Clear();
        }
    }
}
