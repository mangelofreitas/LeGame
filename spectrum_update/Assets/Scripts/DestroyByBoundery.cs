using UnityEngine;
using System.Collections;

public class DestroyByBoundery : MonoBehaviour {

	public GameController gameController;

	void OnTriggerExit(Collider other) {
		if(other.tag=="Player" || other.tag =="Circle")
		{
			return;
		}
		gameController.removeCube (other.gameObject);
		Destroy(other.gameObject);
	}
}
