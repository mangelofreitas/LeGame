using UnityEngine;
using System.Collections;

public class Colider : MonoBehaviour {
	private int scoreValue = 10;
	public int countValue;
	public GameController gameController;
	public ColorManagement colorManagement;
	private bool lerping = false;
	private Color finalcolor;
	private Color inicialcolor;
	private int test=1;
	private int contadorRed=0;
	private int contadorGreen=0;
	private int contadorBlue=0;
	private Color currentcolor;
	private int [] multiplier = {1,2,4,6};
	private int posMulti = 0;
	
	void Start(){
		this.renderer.material.color = Color.white;
		finalcolor = this.renderer.material.color;
		colorManagement = gameController.GetComponent<ColorManagement>();
		currentcolor = finalcolor; 
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag=="Boundery"){
			return;
		}
		else if(other.gameObject.renderer.material.GetColor("_Color")==Color.white){
			resetCounter(Color.red);
			resetCounter(Color.blue);
			resetCounter(Color.green);
			inicialcolor=finalcolor;
			finalcolor = new Color(1f,1f,1f);
			test = 1;
			lerping = true;
			
		}
		else{

			if(currentcolor != other.renderer.material.color){
				if(currentcolor == Color.red && contadorRed != 3){
					resetCounter(Color.red);
					resetCounter(Color.blue);
					resetCounter(Color.green);
					posMulti=0;
				}
				else if(currentcolor == Color.blue && contadorBlue != 3){
					resetCounter(Color.red);
					resetCounter(Color.blue);
					resetCounter(Color.green);
					posMulti=0;
				}
				else if(currentcolor == Color.green && contadorGreen != 3){
					resetCounter(Color.red);
					resetCounter(Color.blue);
					resetCounter(Color.green);
					posMulti=0;
				}
				currentcolor = other.renderer.material.color;			
			}
			colorManagement.AddScore(scoreValue*multiplier[posMulti]);
			if(other.renderer.material.color == Color.red){
				inicialcolor=finalcolor;
				if(contadorRed<=3){
					if(contadorRed==2){
						gameController.timeLeft+=4;
						posMulti++;
					}
					addCounter(other.renderer.material.color);
					finalcolor = new Color(1f,(3-contadorRed)/3f,(3-contadorRed)/3f);
					test = 1;
					lerping = true;
				}
			}
			else if(other.renderer.material.color == Color.blue){
				inicialcolor=finalcolor;
				if(contadorBlue<=3){
					if(contadorBlue == 2){
						gameController.timeLeft+=4;
						posMulti++;
					}
					addCounter(other.renderer.material.color);
					finalcolor = new Color((3-contadorBlue)/3f,(3-contadorBlue)/3f,1f);
					test = 1;
					lerping = true;
				}
			}
			else if(other.renderer.material.color == Color.green){
				inicialcolor=finalcolor;
				if(contadorGreen<=3){
					if(contadorGreen == 2){
						gameController.timeLeft+=4;
						posMulti++;
					}
					addCounter(other.renderer.material.color);
					finalcolor = new Color((3-contadorGreen)/3f,1f,(3-contadorGreen)/3f);
					test = 1;
					lerping = true;
				}
			}
			else{
				this.renderer.material.SetColor("_Color",other.renderer.material.color);
			}
		}
		gameController.removeCube (other.gameObject);
		Destroy(other.gameObject);
	}
	
	void Update(){
		if(contadorBlue == 3 && contadorRed == 3 && contadorGreen == 3){
			resetCounter(Color.red);
			resetCounter(Color.blue);
			resetCounter(Color.green);
			posMulti=0;
		}
		if (lerping) {
			this.renderer.material.SetColor("_Color",Color.Lerp(inicialcolor,finalcolor,test/50f));
			if(test == 50){
				test=1;
				lerping = false;
			}
			test++;
		}
	}
	
	void addCounter(Color cor){
		if (cor == Color.red) {
			if(contadorRed<3)
			{
				contadorRed++;
				colorManagement.addContador(cor,1);
			}
		}
		else if(cor == Color.blue){
			if(contadorBlue<3) 
			{
				contadorBlue++;
				colorManagement.addContador(cor,1);
			}
		}
		else if(cor == Color.green){
			if(contadorGreen<3)
			{
				contadorGreen++;
				colorManagement.addContador(cor,1);
			}
		}
	}
	
	void resetCounter(Color cor){
		if (cor == Color.red) {
			contadorRed = 0;
			colorManagement.addContador(cor,-3);
		}
		else if(cor == Color.blue){
			contadorBlue = 0;
			colorManagement.addContador(cor,-3);
		}
		else if(cor == Color.green){
			contadorGreen = 0;
			colorManagement.addContador(cor,-3);
		}
	}
}