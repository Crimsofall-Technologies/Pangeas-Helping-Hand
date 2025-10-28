using CrimsofallTechnologies.XR.Interaction;
using UnityEngine;

namespace CrimsofallTechnologies.XR.Gameplay 
{
    public class Blueberry : Interactable
    {
        public GameObject bowPrefab;
        public Transform spawnPoint;
        public float dropForce = 5f;

        public GirlAxol girl;
        private bool done = false;

        //when player interacts randomly give player the bow!
        public override void OnInteract()
        {
            base.OnInteract();
            if(done || girl.complete || girl.bowSpawned) return;

            if(Random.value <= 0.65f)
            {
                GameObject g = Instantiate(bowPrefab, spawnPoint.position, Quaternion.identity);
                g.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * dropForce);
                girl.OnBowFound();
            }
            
            done = true;
        }
    }
}