using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public float speed;
	void Start()
	{
		rigidbody.velocity = new Vector3(0,0,-speed);
	}
}
