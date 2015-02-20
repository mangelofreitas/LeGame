using UnityEngine;
using System.Collections;

public class Colider : MonoBehaviour {

	private GameController gameController;
	public int scoreValue;

	void Start()
	{
		GameObject aux = GameObject.FindWithTag ("GameController");
		if(aux!=null)
			gameController = aux.GetComponent <GameController>();
		else
			print ("Nao encontrou");
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Boundery") 
		{
			return;
		}
		else if(other.gameObject.renderer.material.GetColor("_Color")==Color.white)
		{
			Destroy (gameObject);
			gameController.gameOver();
		}
		else
		{
			this.renderer.material.SetColor("_Color",other.renderer.material.color);
			gameController.AddScore (scoreValue);
		}
		Destroy (other.gameObject);
	}
}
