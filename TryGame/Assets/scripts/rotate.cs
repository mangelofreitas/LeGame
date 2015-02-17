using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	public float tumble;
	
	void Start()
	{
		rigidbody.angularVelocity = Random.insideUnitSphere*tumble;
	}
}
