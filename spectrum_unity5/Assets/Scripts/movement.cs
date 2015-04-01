using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public float speed;
	public Color color;
	void Start()
	{
		GetComponent<Rigidbody>().velocity = new Vector3(0,0,-speed);
	}
}
