using CrimsofallTechnologies.XR.Interaction;
using UnityEngine;

namespace  CrimsofallTechnologies.XR.Gameplay
{
    public class TVButton : MonoBehaviour
    {
        public bool RedButton, BlueButton, YellowButton;
        public TV tv;
        /*private Camera cam;

        public void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            //really pressed this button?
            if(interactInput.action.WasPressedThisFrame() && Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 20.0f)) {
                if(hit.collider.name != gameObject.name)
                    return;

                if(RedButton) //eject tape
                {
                    tv.Eject();
                }

                if(BlueButton) //enter the level
                {
                    tv.EnterLevel();
                }

                if(YellowButton) //play tape video
                {
                    Debug.Log("For future!");
                }
            }
        }*/

        public void OnSelect()
        {
            if(RedButton) //eject tape
            {
                tv.Eject();
            }

            if(BlueButton) //enter the level
            {
                tv.EnterLevel();
            }

            if(YellowButton) //play tape video
            {
                Debug.Log("For future!");
            }
        }
    }
}