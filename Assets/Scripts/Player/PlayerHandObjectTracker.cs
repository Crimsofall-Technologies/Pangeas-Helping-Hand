using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PlayerHandObjectTracker : MonoBehaviour
{
    public NearFarInteractor interactorLeft;
    public NearFarInteractor interactorRight;

    private void Start()
    {
        GameManager.playerObject = transform;
    }

    public GameObject GetLeftHandObject()
    {
        IXRSelectInteractable held = interactorLeft.GetOldestInteractableSelected();
        if(held!=null)
        {
            return held.transform.gameObject;
        }
        
        return null;
    }

    public GameObject GetRightHandObject()
    {
        IXRSelectInteractable held = interactorRight.GetOldestInteractableSelected();
        if(held!=null)
        {
            return held.transform.gameObject;
        }
        
        return null;
    }
}
