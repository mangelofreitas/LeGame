using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Colider : MonoBehaviour {
	private int scoreValue = 10;
	public int countValue;
	public GameController gameController;
	public ColorManagement colorManagement;
	private bool lerping = false;
	public bool rainbowlerping = false;
	private Color finalcolor;
	private Color inicialcolor;
	private int test=1;
	private int contadorRed=0;
	private int contadorGreen=0;
	private int contadorBlue=0;
	private Color currentcolor;
	private int [] multiplier = {1,2,4,6};
	private int posMulti = 0;
	private int ncores;
	public float timeLeftAux;
	public Text text6;
	public Text text7;
	
	void Start(){
		this.renderer.material.color = Color.white;
		finalcolor = this.renderer.material.color;
		colorManagement = gameController.GetComponent<ColorManagement>();
		currentcolor = finalcolor; 
		text6.text="";
		text7.text="x"+multiplier[posMulti];
	}
	
	void OnTriggerEnter(Collider other){
		if (rainbowlerping) {
			colorManagement.AddScore(scoreValue*multiplier[posMulti]);
		}
		else {
			if(other.tag=="Boundery"){
				return;
			}
			else if(other.gameObject.renderer.material.GetColor("_Color")==Color.white){
				posMulti=0;
				multiplierEffect();
				resetCounter(Color.red);
				resetCounter(Color.blue);
				resetCounter(Color.green);
				inicialcolor=finalcolor;
				finalcolor = new Color(1f,1f,1f);
				test = 1;
				lerping = true;
				gameController.timeLeft-=2;
				timeEffect("-2");
			}
			else{
				if(currentcolor != other.renderer.material.color){
					if(currentcolor == Color.red && contadorRed != 3){
						resetCounter(Color.red);
						resetCounter(Color.blue);
						resetCounter(Color.green);
						posMulti=0;
						multiplierEffect();
					}
					else if(currentcolor == Color.blue && contadorBlue != 3){
						resetCounter(Color.red);
						resetCounter(Color.blue);
						resetCounter(Color.green);
						posMulti=0;
						multiplierEffect();
					}
					else if(currentcolor == Color.green && contadorGreen != 3){
						resetCounter(Color.red);
						resetCounter(Color.blue);
						resetCounter(Color.green);
						posMulti=0;
						multiplierEffect();
					}
					currentcolor = other.renderer.material.color;			
				}
				colorManagement.AddScore(scoreValue*multiplier[posMulti]);
				if(other.renderer.material.color == Color.red){
					inicialcolor=finalcolor;
					if(contadorRed<=3){
						if(contadorRed==2){
							gameController.timeLeft+=4;
							timeEffect("+4");
							if(posMulti<3){
								posMulti++;
								multiplierEffect();
							}
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
							timeEffect("+4");
							if(posMulti<3){
								posMulti++;
								multiplierEffect();
							}
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
							timeEffect("+4");
							if(posMulti<3){
								posMulti++;
								multiplierEffect();
							}
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
		}
		audio.Play ();
		gameController.removeCube (other.gameObject);
		Destroy(other.gameObject);
	}

	void Update(){
		if(contadorBlue == 3 && contadorRed == 3 && contadorGreen == 3){
			lerping=false;
			superSayainmode();
			resetCounter(Color.red);
			resetCounter(Color.blue);
			resetCounter(Color.green);
		}
		if (lerping) {
			this.renderer.material.SetColor("_Color",Color.Lerp(inicialcolor,finalcolor,test/50f));
			if(test == 50){
				test=1;
				lerping = false;
			}
			test++;
		}
		else if (rainbowlerping) {
			this.renderer.material.SetColor("_Color",Color.Lerp(inicialcolor,finalcolor,test/20f));
			if(gameController.cubitos.Count>0){
				foreach(GameObject cubo in gameController.cubitos){
					cubo.renderer.material.color = this.renderer.material.color;
				}
			}
			if(test == 20){
				test=1;
				ncores--;
				if(ncores==-1){
					rainbowlerping = false;
					inicialcolor = transform.renderer.material.color;
					finalcolor = Color.white;
					foreach(GameObject cubo in gameController.cubitos){
						cubo.renderer.material.color = cubo.GetComponent<movement>().color;
					}
					posMulti=0;
					multiplierEffect();
					lerping = true;
				}
				else{
					inicialcolor = finalcolor;
					finalcolor = new Color(Random.value,Random.value,Random.value);
				}
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

	void timeEffect(string time)
	{
		text6.color = new Color (1,1,1,1);
		Vector3 posres = text6.transform.position;
		Vector3 vec = new Vector3 (text6.transform.position.x, 1100, text6.transform.position.z);
		text6.text = ""+time;
		Vector3 destin = Vector3.MoveTowards(posres,vec,5.0f*Time.deltaTime);
		if (text6.transform.position == destin) 
		{
			text6.text="";
			text6.transform.position=posres;
		}
		else
		{
			text6.transform.position = destin;
			StartCoroutine(FadeTo(text6, 0.0f,1.0f));
		}
	}

	void multiplierEffect()
	{
		StartCoroutine(FadeTo(text7,0.0f,0.1f));
		text7.text="x"+multiplier[posMulti];
		StartCoroutine(FadeTo(text7,1.0f,0.1f));
	}

	IEnumerator FadeTo(Text text,float aValue, float aTime)
	{
		float alpha = text.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			text.color = newColor;
			yield return null;
		}
	}

	void superSayainmode(){
		timeLeftAux = gameController.timeLeft;
		print (timeLeftAux);
		inicialcolor = Color.white;
		finalcolor = new Color (Random.value, Random.value, Random.value);
		lerping = false;
		test = 1;
		ncores = 10;
		rainbowlerping = true;
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