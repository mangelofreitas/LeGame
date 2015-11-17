using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	void Update () {
		GetComponent<Rigidbody>().transform.Rotate (new Vector3 (10, 30, 45) * Time.deltaTime);
	}
}
