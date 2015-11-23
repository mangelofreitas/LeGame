using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public float speed;
	public Color color;
    public GameObject player;
    public bool isBarrier;
	void Start()
	{
		GetComponent<Rigidbody>().velocity = new Vector3(0,0,-speed);
	}
    void Update()
    {
        if(this.tag == "Circle")
        {
            color = player.GetComponent<Renderer>().material.color;
            this.GetComponent<Renderer>().material.SetColor("_Color", color);
        }
    }
}
