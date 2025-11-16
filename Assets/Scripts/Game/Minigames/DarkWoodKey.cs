using UnityEngine;

public class DarkWoodKey : MonoBehaviour
{
	public Transform teleportPos;
	public Transform player;
	
	private bool firstPicked;
	
	public void OnPick()
	{
		if(!firstPicked)
		{
			player.transform.position = teleportPos.position;
			firstPicked = true;
		}
		
		//disable entrances
		GameManager.Instance.CanEnterEntrances = false;
	}
	
	public void OnDrop()
	{
		//enable entrances
		GameManager.Instance.CanEnterEntrances = true;
	}
}
