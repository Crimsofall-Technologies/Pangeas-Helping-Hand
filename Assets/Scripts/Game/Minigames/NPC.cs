using UnityEngine;
using UnityEngine.UI;
using cherrydev;
using System;

public class NPC : MonoBehaviour
{
    public DialogBehaviour dialogBehaviour;
    public DialogNodeGraph[] nodeGraphs;

    private int dialougeIndex;

    public Action OnTalkedAction;
    public bool DisableTalk = false; //will not talk if this is true

    public void OnTalked(bool allowRestrictions = true)
    {
        if((allowRestrictions && DisableTalk) || dialogBehaviour._isDialogStarted)
            return;

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
