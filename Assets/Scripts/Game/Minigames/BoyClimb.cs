using UnityEngine;

//Talk with him normally or start the climbing challenge!
public class BoyClimb : MonoBehaviour
{
    public GameObject raceObject, loseObject, jumpDownObject, idleObject;
    public NPC npc;
    public float climbDelay = 15.0f;

    public bool win { get; private set; }
    private bool raceComplete;

    private void Start()
    {
        npc.dialogBehaviour.BindExternalFunction("StartClimbing", ()=>StartClimbing());

        idleObject.SetActive(true);
        raceObject.SetActive(false);
        loseObject.SetActive(false);
        jumpDownObject.SetActive(false);
    }

    public void StartClimbing()
    {
        idleObject.SetActive(false);
        raceObject.SetActive(true);
        loseObject.SetActive(false);
        jumpDownObject.SetActive(false);

        Invoke(nameof(ClimbingComplete), climbDelay);
    }

    private void ClimbingComplete()
    {
        raceComplete = true;

        idleObject.SetActive(false);
        raceObject.SetActive(false);
        loseObject.SetActive(false);
        jumpDownObject.SetActive(false);

        //jump back down then start the dialouge!
        Invoke(nameof(JumpDown), 2f);
    }

    private void JumpDown()
    {
        idleObject.SetActive(false);
        raceObject.SetActive(false);
        loseObject.SetActive(false);
        jumpDownObject.SetActive(true);
    }

    public void OnPlayerReachedTop()
    {
        npc.ProgressDialouge();
        if (raceComplete)
        {
            win = false;
            npc.ProgressDialouge();
        }
        else
        {
            win = true;
        }

        npc.OnTalked();
    }
}
