using UnityEngine;
using System.Collections;

public class swipe : MonoBehaviour {

	private bool moving = false;
	private Vector3 destination;

	void FixedUpdate () {
		if (moving) {
			if (rigidbody.position == destination) {
				moving = false;	
			}
			else{
				rigidbody.position = destination;
			}
		}
		else{
			if (Input.GetKeyDown("d") && rigidbody.position.z<6.3f) {
				destination = new Vector3(rigidbody.position.x,rigidbody.position.y,rigidbody.position.z+6.3f);
				moving = true;
				Debug.Log("direita");
			}
			else if (Input.GetKeyDown("a") && rigidbody.position.z>-6.3f){
				destination = new Vector3(rigidbody.position.x,rigidbody.position.y,rigidbody.position.z-6.3f);
				moving = true;
				Debug.Log("esquerda");
			}
		}
	}
}
