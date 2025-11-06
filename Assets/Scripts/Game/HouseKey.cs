using CrimsofallTechnologies.XR.Interaction;
using UnityEngine;

namespace CrimsofallTechnologies.XR.Gameplay 
{
    public class HouseKey : Interactable
    {
        public Entrance entranceToEnable;
        public Transform teleportPos;
        public Transform player;

        public override void OnInteract()
        {
            base.OnInteract();
            entranceToEnable.enabled = true;
            entranceToEnable.GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            Invoke(nameof(DelayedDestroy), 2f);
        }

        private void DelayedDestroy()
        {
            player.position = teleportPos.position;
            Destroy(gameObject);
        }
    }
}
