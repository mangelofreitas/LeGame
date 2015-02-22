using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public float speed;
	public Color color;
	void Start()
	{
		rigidbody.velocity = new Vector3(0,0,-speed);
	}
}
