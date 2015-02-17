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
		Destroy (other.gameObject);
		Debug.Log (other.gameObject);
	}
}
