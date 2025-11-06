using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "")]
public class ItemVar : ScriptableObject
{
    public string itemName;
    public Sprite Icon;
    public GameObject pickupObject;
}
