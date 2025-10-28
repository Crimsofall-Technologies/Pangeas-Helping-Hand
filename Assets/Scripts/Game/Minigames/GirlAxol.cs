using UnityEngine;
using UnityEngine.UI;

namespace CrimsofallTechnologies.XR.Gameplay 
{
    public class GirlAxol : MonoBehaviour
    {
        public GameObject bowObject;
        public float applyAnimationDelay = 1f;
        public NPC npc, elderNpc;

        private Animator animator;
        public bool complete { get; private set; }
        public bool bowSpawned { get; private set; }
        
        private bool firstTime = true;

        private void Start()
        {
            animator = GetComponent<Animator>();

            animator.SetBool("Hold Out Hands", false);
            animator.SetBool("Happy", false);
            bowObject.SetActive(false);

            npc.OnTalkedAction += OnTalked;
        }

        private void OnTalked()
        {
            if(complete)
                return;

            //on first time talk show ... and progress elder dialouge by 1
            if(firstTime) {
                elderNpc.ProgressDialouge();
                firstTime = false;
            }
        }

        public void OnBowFound()
        {
            //play the hold out hands animation
            animator.SetBool("Hold Out Hands", true);
            bowSpawned = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(complete) return;

            if(other.tag == "Bow") 
            {
                Destroy(other.gameObject); //remove the 3D bow object
                animator.SetTrigger("Apply_Bow");
                npc.ProgressDialouge(); //progress self dialouge
                npc.DisableTalk = true;
                Invoke(nameof(DelayedApplyBow), applyAnimationDelay);
            }
        }

        private void DelayedApplyBow()
        {
            bowObject.SetActive(true);
            animator.SetBool("Happy", true); //when player returns the bow to her!
            npc.OnTalked(false);
            elderNpc.ProgressDialouge(); //progress elkder dialouge
            npc.DisableTalk = false;
            complete = true;
            npc.ProgressDialouge(); //my final dialouge!
        }
    }
}
