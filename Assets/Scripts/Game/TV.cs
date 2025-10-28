using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace CrimsofallTechnologies.XR.Gameplay 
{
    public class TV : MonoBehaviour
    {
        public VideoPlayer player;
        public GameObject blackScreen;
        public VideoClip[] clips;
        public GameObject[] Tapes;
        public Transform outForcePoint;
        public float ejectForce = 100f;

        private string CurrentTape = "";
        private GameObject InsertedTape;
        private Vector3[] tapePos;
        private Quaternion[] tapeRot;
        private Animator animator;
        private bool hasTape, ejecting = false;
        private Rigidbody tapeRB;

        private void Start()
        {
            player.Stop();
            blackScreen.SetActive(true);
            animator = GetComponent<Animator>();

            //record tape positions!
            tapePos = new Vector3[Tapes.Length];
            tapeRot = new Quaternion[Tapes.Length];
            for(int i = 0; i < Tapes.Length; i++)
            {
                Tapes[i].transform.parent = null; //clear any parents from all tape objects!
                tapePos[i] = Tapes[i].transform.position;
                tapeRot[i] = Tapes[i].transform.rotation;
            }
        }

        void LateUpdate()
        {
            if(InsertedTape != null)
            {
                tapeRB.isKinematic = true;
            }
        }

        public void Eject()
        {
            if(hasTape) {
                //animator.SetBool("Inserted", false);
                player.Stop();
                blackScreen.SetActive(true);
                Invoke(nameof(ReturnTape), 1.5f); //return to table.
            }
        }

        private void ReturnTape()
        {
            //return the tape out on table!
            InsertedTape.SetActive(true);
            InsertedTape.GetComponent<AutoFallProtector>().DelayedReset();
            tapeRB.position = outForcePoint.position;
            InsertedTape.SetActive(true);
            InsertedTape = null;
            tapeRB.isKinematic = false;
            tapeRB.AddForce(outForcePoint.forward * ejectForce, ForceMode.Impulse);
            tapeRB = null;
            hasTape = false;
        }

        public void EnterLevel()
        {
            if(!hasTape) return;

            string sceneName  = "";
            //actually load the level...
            if(CurrentTape == "GREEN_TAPE") sceneName = "Fruit Land";
            if(CurrentTape == "ORANGE_TAPE") sceneName ="Cloud City";
            if(CurrentTape == "BROWN_TAPE") sceneName ="Toy Land";
            if(CurrentTape == "PURPLE_TAPE") sceneName ="Dark Wood";
            GameManager.Instance.LoadLevel(sceneName);
        }

        public void OnInsertTape(GameObject tape)
        {
            CurrentTape = tape.name;

            //play movie:
            player.clip = null;
            if(CurrentTape == "GREEN_TAPE") player.clip = clips[0];
            if(CurrentTape == "ORANGE_TAPE") player.clip = clips[1];
            if(CurrentTape == "BROWN_TAPE") player.clip = clips[2];
            if(CurrentTape == "PURPLE_TAPE") player.clip = clips[3];
            player.Play(); //auto play the movie for the inserted tape!
            blackScreen.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            //do not accept more tapes if one is already inserted!
            if(hasTape)
                return;

            if(other.tag == "Tape") //is this a Tape?
            {
                if(CurrentTape == other.name) //same as last?
                    return;

                //animator.SetBool("Inserted", true); //inserted the tape?
                tapeRB = other.GetComponent<Rigidbody>();
                other.gameObject.SetActive(false);

                InsertedTape = other.gameObject;
                hasTape = true;
                OnInsertTape(other.gameObject);
            }
        }
    }
}
