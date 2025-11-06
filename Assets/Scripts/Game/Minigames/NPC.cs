using UnityEngine;
using UnityEngine.UI;
using cherrydev;
using System;

public class NPC : MonoBehaviour
{
    public DialogBehaviour dialogBehaviour;
    public DialogNodeGraph[] nodeGraphs;
	public bool setTalkAnimationWhileTalking = false; //must have a trigger Talk and animation in the animator component!

    private int dialougeIndex;
	private Animator animator;

    public Action OnTalkedAction;
    public bool DisableTalk = false; //will not talk if this is true

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

    public void OnTalked(bool allowRestrictions = true)
    {
        if((allowRestrictions && DisableTalk) || dialogBehaviour._isDialogStarted)
            return;
		
		if(setTalkAnimationWhileTalking && animator != null) {
			animator.SetTrigger("Talk");
		}

        dialogBehaviour.StartDialog(nodeGraphs[dialougeIndex]);
        OnTalkedAction?.Invoke();
    }

    public void ProgressDialouge()
    {
        dialougeIndex++;

        if(dialougeIndex >= nodeGraphs.Length) 
            dialougeIndex = nodeGraphs.Length - 1;
    }
}
