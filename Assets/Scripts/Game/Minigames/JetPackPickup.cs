using CrimsofallTechnologies.XR;
using UnityEngine;

namespace CrimsofallTechnologies.XR.Gameplay {
    public class JetPackPickup : MonoBehaviour
    {
        public PlayerMotor playerMotor;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                playerMotor.hasJetpack = true;
                Destroy(gameObject);
            }
        }
    }
}
