using UnityEngine;

public class PaintCan : MonoBehaviour
{
	public Color color;
	
	//when player dips brush in this can - apply color on brush!
	private void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Brush")
		{
			col.GetComponent<PaintBrush>().currentColor = color; 
		}
	}
}
