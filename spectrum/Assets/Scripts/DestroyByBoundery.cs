using UnityEngine;
using System.Collections;

public class DestroyByBoundery : MonoBehaviour {

	public GameController gameController;

	void OnTriggerExit(Collider other) {
		if(other.tag=="Player")
		{
			return;
		}
		gameController.removeCube (other.gameObject);
		Destroy(other.gameObject);
	}
}
