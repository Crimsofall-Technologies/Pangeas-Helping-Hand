using UnityEngine;
using UnityEngine.InputSystem;

namespace CrimsofallTechnologies.XR.Interaction
{
    public class Interactable : MonoBehaviour
    {
        public bool ShowDebug;

        public Transform playerBody { get; set; }
        public bool IsEntrance { get; set;  }

        public virtual void Start() 
        {
            playerBody = GameObject.FindGameObjectWithTag("Player").transform;
        }

        //called when player interacts with this object, useful for classes that derive from this class
        public virtual void OnInteract() { if(ShowDebug) Debug.Log("Interacting with " + gameObject.name+"..."); }
    }
}
