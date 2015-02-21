using UnityEngine;
using System.Collections;

public class AuraEffect : MonoBehaviour {

	private float counter;


	void Start () {
		counter = 0;
		GlowEffect.Instantiate (transform);
		//((Behaviour)GetComponent ("Halo")).enabled = true;
	}
	

	void Update () {
		counter += Time.deltaTime;
		/*if ((int)counter % 2 == 0) 
		{
			((Behaviour)GetComponent ("Halo")).enabled = false;
		}
		else 
		{
			((Behaviour)GetComponent ("Halo")).enabled = true;
		}*/
	}
}
