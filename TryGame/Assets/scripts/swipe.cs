using UnityEngine;
using System.Collections;

public class swipe : MonoBehaviour {

	private bool moving = false;
	private Vector3 destination;

	private float xStart = 0.0f;
	private float xEnd = 0.0f;
	private float yStart = 0.0f;
	private float yEnd = 0.0f;
	private bool sendCall = true;

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
			foreach (Touch touch in Input.touches) {
				
				if (touch.phase == TouchPhase.Began) {
					xStart = touch.position.x;
					yStart = touch.position.y;
				}
				if (touch.phase == TouchPhase.Moved) {
					xEnd = touch.position.x;
					yEnd = touch.position.y;
					
					if ((xStart < xEnd) && (rigidbody.position.z<6.3f)) {
						//print ("Right Swipe");
						sendCall = false;
						destination = new Vector3(rigidbody.position.x,rigidbody.position.y,rigidbody.position.z+6.3f);
						moving = true;
					}
					if ((xStart > xEnd) && (rigidbody.position.z>-6.3f) ) {
						//print ("Left Swipe");
						sendCall = false;
						destination = new Vector3(rigidbody.position.x,rigidbody.position.y,rigidbody.position.z-6.3f);
						moving = true;
					}
					
				}
				
				if (touch.phase == TouchPhase.Ended) {
					xStart = 0.0f;    // resetting start and end x position.
					xEnd = 0.0f;
					sendCall = true;      //Reset to send call again after touch has been completed.
					
				}

			}
			if (Input.GetKeyDown("d") && rigidbody.position.z<6.3f) {
				destination = new Vector3(rigidbody.position.x,rigidbody.position.y,rigidbody.position.z+6.3f);
				moving = true;
				
			}
			else if (Input.GetKeyDown("a") && rigidbody.position.z>-6.3f){
				destination = new Vector3(rigidbody.position.x,rigidbody.position.y,rigidbody.position.z-6.3f);
				moving = true;
				
			}
	}
}
}

