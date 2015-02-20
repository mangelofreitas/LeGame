using UnityEngine;
using System.Collections;

public class swipe : MonoBehaviour {

	private bool moving = false;
	private bool jumping = false;
	private Vector3 destination;
	public float speed = 10f;
	private float xStart = 0.0f;
	private float xEnd = 0.0f;
	private float yStart = 0.0f;
	private float yEnd = 0.0f;
	public float rot = 50f;
	private float tilt;
	private bool sendCall = true;
	public GameObject[] positions;
	private int pos = 1;
	private Rotation rotateCube;
	private GameObject aux;

	void Start()
	{
		aux =  GameObject.FindWithTag("Cube");
		rotateCube = aux.GetComponent<Rotation> ();
		renderer.material.SetColor ("_Color", Color.black);
		//destination = new Vector3(rigidbody.position.x,rigidbody.position.y,positions[pos].transform.position.z);
		//rigidbody.position = destination;
	}

	void Update () {
		if (moving) {
			if (transform.position == destination) {
				transform.rotation = Quaternion.Euler(270f,210f,0f);
				moving = false;	
			}
			else{
				transform.rotation = Quaternion.Euler (270f,210f,speed*-tilt);
				transform.position = Vector3.MoveTowards(transform.position,destination,speed*Time.deltaTime);
			}
		}
		else if (jumping) {
			if(transform.position == destination)
			{
				destination = new Vector3(positions[pos].transform.position.x,positions[pos].transform.position.y,positions[pos].transform.position.z); 
				moving = true;
			}
			else{
				transform.position = Vector3.MoveTowards(transform.position,destination,2*speed*Time.deltaTime);
			}
		}
		else{
			foreach (Touch touch in Input.touches) {
				
				if (touch.phase == TouchPhase.Began) {
					xStart = touch.position.x;
					yStart = touch.position.y;
				}
				if (touch.phase == TouchPhase.Moved){
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
						/*if(pos%3==0)
						{
							rotateCube.moveLeft=true;
							Debug.Log("Left");
						}*/
						destination = new Vector3(transform.position.x,transform.position.y,positions[pos].transform.position.z);
						tilt = -rot;
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
						/*if(pos%3==0)
						{
							rotateCube.moveRight=true;
							Debug.Log("Right");
						}*/
						destination = new Vector3(transform.position.x,transform.position.y,positions[pos].transform.position.z);
						tilt = rot;
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
				if(pos==2)
				{
					pos=0;
					destination = new Vector3(positions[3].transform.position.x,positions[3].transform.position.y,positions[3].transform.position.z);
					while(transform.position != destination)
					{
						transform.position = Vector3.MoveTowards(transform.position,destination,speed*Time.deltaTime);
					}
					rotateCube.moveRight=true;
					jumping=true;
				}
				else
				{
					pos++;
					moving = true;
				}
				destination = new Vector3(transform.position.x,positions[pos].transform.position.y,positions[pos].transform.position.z);
				tilt = -rot;

			}
			else if (Input.GetKeyDown("a")){
				if(pos==0)
				{
					pos=2;
					destination = new Vector3(positions[3].transform.position.x,positions[3].transform.position.y,positions[3].transform.position.z);
					rotateCube.moveLeft=true;
					jumping = true;
				}
				else
				{
					pos--;
					moving = true;
				}
				destination = new Vector3(transform.position.x,positions[pos].transform.position.y,positions[pos].transform.position.z);
				tilt = rot;

			}
	}
}
}

