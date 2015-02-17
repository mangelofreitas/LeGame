using UnityEngine;
using System.Collections;

public class Colider : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundery") 
		{
			return;
		}
		else if(other.gameObject.renderer.material.GetColor("_Color")==Color.white)
		{
			Destroy (gameObject);
		}
		else{
			this.renderer.material.SetColor("_Color",other.renderer.material.color);
		}
		Destroy (other.gameObject);
	}
}
