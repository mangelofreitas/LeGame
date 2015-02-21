using UnityEngine;
using System.Collections;

public class Colider : MonoBehaviour {
	public int scoreValue;
	public int countValue;
	private GameController gameController;

	void Start(){
		GameObject aux = GameObject.FindWithTag ("GameController");
		if(aux!=null){
			gameController = aux.GetComponent <GameController>();
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag=="Boundery"){
			return;
		}
		else if(other.gameObject.renderer.material.GetColor("_Color")==Color.white){
			Destroy(gameObject);
			gameController.gameOver();
		}
		else{
			this.renderer.material.SetColor("_Color",other.renderer.material.color);
			gameController.AddScore(scoreValue);
			gameController.addContador(other.renderer.material.color);
		}
		Destroy(other.gameObject);
	}
}