using UnityEngine;
using System.Collections;

public class Colider : MonoBehaviour {
	private int scoreValue = 10;
	public int countValue;
	public GameController gameController;
	private bool lerping = false;
	private Color finalcolor;
	private Color inicialcolor;
	private int test=1;
	private float colorstate=3;

	void Start(){
		this.renderer.material.color = Color.white;
		finalcolor = this.renderer.material.color;
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
			if((this.renderer.material.color.r == 1 &&  other.renderer.material.color.r == 1)
			   ||(this.renderer.material.color.g == 1 &&  other.renderer.material.color.g == 1)
			   ||(this.renderer.material.color.b == 1 &&  other.renderer.material.color.b == 1))
			{
				if(colorstate!=0){
					print ("Baixou");
					colorstate--;
				}

			}
			else{
				colorstate = 2;
			}
			if(colorstate>-1){
				inicialcolor=finalcolor;
				if(other.renderer.material.color == Color.red)
					finalcolor = new Color(1f,colorstate/3f,colorstate/3f);
				else if(other.renderer.material.color == Color.blue)
					finalcolor = new Color(colorstate/3f,colorstate/3f,1f);
				else if(other.renderer.material.color == Color.green)
					finalcolor = new Color(colorstate/3f,1f,colorstate/3f);
				test = 1;
				lerping = true;
			}
			else{
				this.renderer.material.SetColor("_Color",other.renderer.material.color);
			}
			gameController.AddScore(scoreValue);
			gameController.addContador(other.renderer.material.color);
		}
		gameController.removeCube (other.gameObject);
		Destroy(other.gameObject);
	}

	void Update(){
		if (lerping) {
			//print ("cor inicial - " + inicialcolor);
			//print ("cor final - " + finalcolor);
			this.renderer.material.SetColor("_Color",Color.Lerp(inicialcolor,finalcolor,test/50f));
			if(test == 50){
				test=1;
				lerping = false;
			}
			test++;
		}
	}	
}