using UnityEngine;
using System.Collections;

public class DestroyByBoundery : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}
}
