using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	void Update () {
		rigidbody.transform.Rotate (new Vector3 (10, 30, 45) * Time.deltaTime);
	}
}
