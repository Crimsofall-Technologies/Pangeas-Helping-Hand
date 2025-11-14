using UnityEngine;

public class FlowerGlitter : MonoBehaviour
{
	public ParticleSystem[] glitterParticle; 
	
	private void OnParticleCollision(GameObject other) 
	{
		if(other.tag == "Water")
		{
			Glitter();
		}
	}
	
	public void Glitter()
	{
		for (int i = 0; i < glitterParticle.Length; i++) {
			if(!glitterParticle[i].isPlaying) 
				glitterParticle[i].Play();
		}
	}
}
