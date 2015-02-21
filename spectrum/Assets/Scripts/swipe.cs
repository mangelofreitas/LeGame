using UnityEngine;
using System.Collections;

public class swipe : MonoBehaviour {

	private bool moving = false;
	private bool movingdown = false;
	private bool jumping = false;
	private bool chegou = false;
	private Vector3 xdestination;
	private Vector3 ydestination;
	public float speed = 10f;
	public float speedy = 20f;
	private float xStart = 0.0f;
	private float xEnd = 0.0f;
	private float yStart = 0.0f;
	private float yEnd = 0.0f;
	public float rot = 50f;
	private float tilt;
	private bool sendCall = true; 
	private Vector3[] positions = {new Vector3(-7f, 12.0f, -100.0f),new Vector3(0.0f,12.0f,-100.0f),new Vector3(7f, 12.0f,-100.0f)}; //MUDAR
	private int pos = 1;
	private Rotation rotateCube;
	private GameObject aux;

	void Start()
	{
		aux =  GameObject.FindWithTag("Cube");
		rotateCube = aux.GetComponent<Rotation> ();
	}

	void Update () {
		if (moving) {
			if(transform.position == xdestination){
				moving = false;

			}
			else{
				transform.position = Vector3.MoveTowards(transform.position,xdestination,speed*Time.deltaTime);
			}
		}
		else if(jumping){
			if(transform.position == ydestination && !rotateCube.moving){
				jumping = false;
				moving = true;
			}
			else{
				transform.position = Vector3.MoveTowards(transform.position,ydestination,speedy*Time.deltaTime);
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
						if(pos==2)
						{
							pos=0;
							ydestination = new Vector3(-7f,20f,-100f);
							xdestination = positions[pos];
							rotateCube.moveLeft=true;
							jumping=true;
						}
						else
						{
							pos++;
							xdestination = positions[pos];
							print (xdestination);
							moving = true;
							tilt = -rot;
						}
					}
					if ((xStart > xEnd)) {
						//print ("Left Swipe");
						if(pos==0)
						{
							pos=2;
							ydestination = new Vector3(7f,20f,-100f);
							xdestination = positions[pos];
							rotateCube.moveRight=true;
							jumping = true;
						}
						else
						{
							pos--;
							xdestination = positions[pos];
							print (xdestination);
							moving = true;
							tilt = rot;
						}
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
					ydestination = new Vector3(-7f,20f,-100f);
					xdestination = positions[pos];
					rotateCube.moveLeft=true;
					jumping=true;
				}
				else
				{
					pos++;
					xdestination = positions[pos];
					print (xdestination);
					moving = true;
					tilt = -rot;
				}
			}
			else if (Input.GetKeyDown("a")){
				if(pos==0)
				{
					pos=2;
					ydestination = new Vector3(7f,20f,-100f);
					xdestination = positions[pos];
					rotateCube.moveRight=true;
					jumping = true;
				}
				else
				{
					pos--;
					xdestination = positions[pos];
					print (xdestination);
					moving = true;
					tilt = rot;
				}
			}
		}
	}
}


