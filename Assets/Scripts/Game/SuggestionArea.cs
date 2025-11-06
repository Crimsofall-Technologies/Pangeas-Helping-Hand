using UnityEngine;

public class SuggestionArea : MonoBehaviour
{
    public string suggestionServerURL = "https://example.com/suggestions";

    public void OnButtonPress()
    {
        Application.OpenURL(suggestionServerURL);
    }
}
