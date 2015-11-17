using UnityEngine;
using System.Collections;

public class swipe : MonoBehaviour {

	private bool moving = false;
	private bool falling = false;
	private bool jumping = false;
	private Vector3 xdestination;
	private Vector3 ydestination;
	public float speed = 50f;
	public float speedy = 30f;
	private float xStart = 0.0f;
	private float xEnd = 0.0f;
	public float rot = 50f;
	private Vector3[] positions = {new Vector3(-7f, 12.0f, -100.0f),new Vector3(0.0f,12.0f,-100.0f),new Vector3(7f, 12.0f,-100.0f)}; //MUDAR
	private int pos = 1;
	private Rotation rotateCube;
	private GameObject aux;
	private Quaternion rotationdesired;
    private int SwipeID = -1;

	void Start()
	{
		aux =  GameObject.FindWithTag("Cube");
		rotateCube = aux.GetComponent<Rotation>();
	}

	void Update () {
		//print (this.renderer.material.color);
		if (moving) {
			if(transform.position == xdestination){
				transform.rotation = Quaternion.Euler(0,0,0);
				moving = false;

			}
			else{
				transform.rotation = Quaternion.RotateTowards(transform.rotation,rotationdesired,speed/5*rot);
				transform.position = Vector3.MoveTowards(transform.position,xdestination,speed*Time.deltaTime);
			}
		}
		else if(falling){
			if(transform.position == xdestination){
				falling = false;
				moving = true;
			}
			else{
				transform.position = Vector3.MoveTowards(transform.position,xdestination,speed*Time.deltaTime);
			}
		}
		else if(jumping){
			if(transform.position == ydestination && !rotateCube.moving){
				jumping = false;
				falling = true;
			}
			else{
				transform.position = Vector3.MoveTowards(transform.position,ydestination,speedy*Time.deltaTime);
			}
		}
		else{
            if(PlayerPrefs.GetInt("Mode") == 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began && SwipeID == -1)
                    {
                        xStart = touch.position.x;
                        SwipeID = touch.fingerId;
                    }
                    else if (touch.fingerId == SwipeID)
                    {
                        if (touch.phase == TouchPhase.Moved)
                        {
                            SwipeID = -1;
                            xEnd = touch.position.x;

                            if ((xStart < xEnd))
                            {
                                //print ("Right Swipe");
                                if (pos == 2)
                                {
                                    pos = 0;
                                    ydestination = new Vector3(-7f, 15f, -100f);
                                    xdestination = positions[pos];
                                    rotateCube.moveLeft = true;
                                    jumping = true;
                                }
                                else
                                {
                                    pos++;
                                    xdestination = positions[pos];
                                    rotationdesired = Quaternion.Euler(0f, 0f, 240f);
                                    moving = true;
                                }
                            }
                            if ((xStart > xEnd))
                            {
                                //print ("Left Swipe");
                                if (pos == 0)
                                {
                                    pos = 2;
                                    ydestination = new Vector3(7f, 15f, -100f);
                                    xdestination = positions[pos];
                                    rotateCube.moveRight = true;
                                    jumping = true;
                                }
                                else
                                {
                                    pos--;
                                    xdestination = positions[pos];
                                    rotationdesired = Quaternion.Euler(0f, 0f, 120f);
                                    moving = true;
                                }
                            }

                        }
                        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                        {
                            SwipeID = -1;
                            xStart = 0.0f;    // resetting start and end x position.
                            xEnd = 0.0f;

                        }
                    }
                }
            }
            else if(PlayerPrefs.GetInt("Mode") == 1)
            {
                foreach (Touch touch in Input.touches)
                {

                    if (touch.phase == TouchPhase.Began)
                    {
                        xStart = touch.position.x;
                    }
                    if (touch.phase == TouchPhase.Moved)
                    {
                        xEnd = touch.position.x;

                        if ((xStart < xEnd))
                        {
                            //print ("Right Swipe");
                            if (pos == 2)
                            {
                                pos = 0;
                                ydestination = new Vector3(-7f, 15f, -100f);
                                xdestination = positions[pos];
                                rotateCube.moveLeft = true;
                                jumping = true;
                            }
                            else
                            {
                                pos++;
                                xdestination = positions[pos];
                                rotationdesired = Quaternion.Euler(0f, 0f, 240f);
                                moving = true;
                            }
                        }
                        if ((xStart > xEnd))
                        {
                            //print ("Left Swipe");
                            if (pos == 0)
                            {
                                pos = 2;
                                ydestination = new Vector3(7f, 15f, -100f);
                                xdestination = positions[pos];
                                rotateCube.moveRight = true;
                                jumping = true;
                            }
                            else
                            {
                                pos--;
                                xdestination = positions[pos];
                                rotationdesired = Quaternion.Euler(0f, 0f, 120f);
                                moving = true;
                            }
                        }

                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        xStart = 0.0f;    // resetting start and end x position.
                        xEnd = 0.0f;

                    }

                }
            }

            if (Input.GetKeyDown("d") | Input.GetKeyDown(KeyCode.RightArrow)) {
				if(pos==2)
				{
					pos=0;
					ydestination = new Vector3(-7f,15f,-100f);
					xdestination = positions[pos];
					rotateCube.moveLeft=true;
					jumping=true;
				}
				else
				{
					pos++;
					xdestination = positions[pos];
					rotationdesired = Quaternion.Euler(0f,0f,240f);
					moving = true;
				}
			}
			else if (Input.GetKeyDown("a") | Input.GetKeyDown(KeyCode.LeftArrow)){
				if(pos==0)
				{
					pos=2;
					ydestination = new Vector3(7f,15f,-100f);
					xdestination = positions[pos];
					rotateCube.moveRight=true;
					jumping = true;
				}
				else
				{
					pos--;
					xdestination = positions[pos];
					rotationdesired = Quaternion.Euler(0f,0f,120f);
					moving = true;
				}
			}
		}
	}
}


