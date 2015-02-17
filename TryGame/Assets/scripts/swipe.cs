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
	public GameObject[] positions;
	private int pos = 1;
	private Rotation rotateCube;

	void Start()
	{
		rotateCube.GetComponent<Rotation>();
		destination = new Vector3(rigidbody.position.x,rigidbody.position.y,positions[pos].transform.position.z);
		rigidbody.position = destination;
	}

	void FixedUpdate () {
		if (moving) {
			rigidbody.position = destination;
			moving=false;
			Debug.Log("False");
		}
		else{
			foreach (Touch touch in Input.touches) {
				
				if (touch.phase == TouchPhase.Began) {
					xStart = touch.position.x;
					yStart = touch.position.y;
				}
				if (touch.phase == TouchPhase.Moved && sendCall==true){
					xEnd = touch.position.x;
					yEnd = touch.position.y;
					
					if ((xStart < xEnd)) {
						//print ("Right Swipe");
						sendCall = false;
						if(pos==11)
						{
							pos=0;
						}
						else
						{
							pos++;
						}
						destination = new Vector3(rigidbody.position.x,rigidbody.position.y,positions[pos].transform.position.z);
						moving = true;
					}
					if ((xStart > xEnd)) {
						//print ("Left Swipe");
						sendCall = false;
						if(pos==0)
						{
							pos=11;
						}
						else
						{
							pos--;
						}
						destination = new Vector3(rigidbody.position.x,rigidbody.position.y,positions[pos].transform.position.z);
						moving = true;
					}
					
				}
				
				if (touch.phase == TouchPhase.Ended) {
					xStart = 0.0f;    // resetting start and end x position.
					xEnd = 0.0f;
					sendCall = true;      //Reset to send call again after touch has been completed.
					
				}

			}
			if (Input.GetKeyDown("d")) {
				if(pos==11)
				{
					pos=0;
				}
				else
				{
					pos++;
				}
				destination = new Vector3(rigidbody.position.x,rigidbody.position.y,positions[pos].transform.position.z);
				moving = true;
				
			}
			else if (Input.GetKeyDown("a")){
				if(pos==0)
				{
					pos=11;
				}
				else
				{
					pos--;
				}
				destination = new Vector3(rigidbody.position.x,rigidbody.position.y,positions[pos].transform.position.z);
				moving = true;
				
			}
	}
}
}

