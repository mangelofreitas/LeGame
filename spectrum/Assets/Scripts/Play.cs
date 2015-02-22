using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {

	
	public void ChangeScene (int i) {
		Application.LoadLevel(i);
	}
}
