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
		//renderer.material.SetColor ("_Color", Color.black);
	}

	void Update () {
		if (moving) {
			//print(xdestination);
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
			if (Input.GetKeyDown("d")) {
				if(pos==2)
				{
					pos=0;
					ydestination = new Vector3(-7f,20f,-100f); //MUDAR
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
					ydestination = new Vector3(7f,20f,-100f); //MUDAR
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


