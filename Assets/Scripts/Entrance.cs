using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CrimsofallTechnologies.XR.Interaction {
    public class Entrance : Interactable
    {
        public InputActionProperty enterInputAction;
        public Transform teleportPoint;
		public Transform player;
        public AudioClip musicClip; //this will be played when moving to this entrance area!

	    //an object player needs to enter this place  - will be destroyed and unlocked
	    public GameObject key;

        private XROrigin xR;
	    private bool Locked = false;
		private float distanceToEnter = 3f;

        public override void Start()
        {
            IsEntrance = true;
            base.Start();
	        xR = playerBody.GetComponent<XROrigin>();
            
	        if(key != null)
	        	Locked = true;
	        else
            	GetComponent<BoxCollider>().isTrigger = true;
        }

        /*private void Update()
        {
            if(GameManager.Instance.EnteredEntrance)
                return;

            if(enterInputAction.action.WasPressedThisFrame() && Vector3.Distance(transform.position, player.position) <= distanceToEnter)
            {
				Enter();
                GameManager.Instance.OnEnteredEntrance();
            }   
        }*/
		
		private void Enter() {
			//show a simple loading UI
            //GameManager.ui.FakeLoading();
            //Invoke(nameof(Teleport), 0.5f);
            Teleport();
		}

        /*public override void OnInteract()
        {
            base.OnInteract();

            //show a simple loading UI
            GameManager.ui.FakeLoading();
            Invoke(nameof(Teleport), 0.5f);
        }*/

        private void Teleport()
        {
            //really enter
            xR.transform.position = teleportPoint.position;

            //play the music
            if(musicClip != null)
                GameManager.Instance.bgmPlayer.PlayMusic(musicClip);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(GameManager.Instance.EnteredEntrance)
                return;
			
	        if(key != null && Locked && other.tag == "Key" && other.gameObject == key)
	        {
	        	Locked = false;
	        	GetComponent<Collider>().isTrigger = true;
	        	Destroy(other.gameObject);
	        }


	        if(other.tag == "Player" && !Locked)
            {
                GameManager.Instance.OnEnteredEntrance();
                Teleport();
            }
        }

        private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, distanceToEnter);
		}
    }
}
