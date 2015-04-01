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
	private float colorswitching = 20f;
	private Color finalcolor;
	private Color inicialcolor;
	private int test=1;
	private int contadorRed=0;
	private int contadorGreen=0;
	private int contadorBlue=0;
	private Color currentcolor;
	private int [] multiplier = 
	{1,2,4,6};
	private int posMulti = 0;
	private int ncores;
	public float timeLeftAux;
	public Text text6;
	public Text text7;
	
	void Start(){
		this.GetComponent<Renderer>().material.color = Color.white;
		finalcolor = this.GetComponent<Renderer>().material.color;
		colorManagement = gameController.GetComponent<ColorManagement>();
		currentcolor = finalcolor; 
		text6.text="";
		text7.text="";
	}
	
	void OnTriggerEnter(Collider other){
		if (rainbowlerping) {
			colorManagement.AddScore(scoreValue*multiplier[posMulti]);
		}
		else {
			if(other.tag=="Boundery"){
				return;
			}
			else if(other.gameObject.GetComponent<Renderer>().material.GetColor("_Color")==Color.white){
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
				if(currentcolor != other.GetComponent<Renderer>().material.color){
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
					currentcolor = other.GetComponent<Renderer>().material.color;			
				}
				colorManagement.AddScore(scoreValue*multiplier[posMulti]);
				if(other.GetComponent<Renderer>().material.color == Color.red){
					inicialcolor=finalcolor;
					if(contadorRed<=3){
						if(contadorRed==2){
							gameController.timeLeft+=2*(posMulti+1);
							timeEffect("+" + 2*(1+posMulti));
							if(posMulti<3){
								posMulti++;
								multiplierEffect();
							}
						}
						addCounter(other.GetComponent<Renderer>().material.color);
						finalcolor = new Color(1f,(3-contadorRed)/3f,(3-contadorRed)/3f);
						test = 1;
						lerping = true;
					}
				}
				else if(other.GetComponent<Renderer>().material.color == Color.blue){
					inicialcolor=finalcolor;
					if(contadorBlue<=3){
						if(contadorBlue == 2){
							gameController.timeLeft+=2*(posMulti+1);
							timeEffect("+" + 2*(1+posMulti));
							if(posMulti<3){
								posMulti++;
								multiplierEffect();
							}
						}
						addCounter(other.GetComponent<Renderer>().material.color);
						finalcolor = new Color((3-contadorBlue)/3f,(3-contadorBlue)/3f,1f);
						test = 1;
						lerping = true;
					}
				}
				else if(other.GetComponent<Renderer>().material.color == Color.green){
					inicialcolor=finalcolor;
					if(contadorGreen<=3){
						if(contadorGreen == 2){
							gameController.timeLeft+=2*(posMulti+1);
							timeEffect("+" + 2*(1+posMulti));
							if(posMulti<3){
								posMulti++;
								multiplierEffect();
							}
						}
						addCounter(other.GetComponent<Renderer>().material.color);
						finalcolor = new Color((3-contadorGreen)/3f,1f,(3-contadorGreen)/3f);
						test = 1;
						lerping = true;
					}
				}
				else{
					this.GetComponent<Renderer>().material.SetColor("_Color",other.GetComponent<Renderer>().material.color);
				}
			}
		}
		//audio.Play ();
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
			this.GetComponent<Renderer>().material.SetColor("_Color",Color.Lerp(inicialcolor,finalcolor,test/colorswitching));
			if(test == colorswitching){
				test=1;
				lerping = false;
			}
			test++;
		}
		else if (rainbowlerping) {
			this.GetComponent<Renderer>().material.SetColor("_Color",Color.Lerp(inicialcolor,finalcolor,test/20f));
			if(gameController.cubitos.Count>0){
				foreach(GameObject cubo in gameController.cubitos){
					cubo.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;
				}
			}
			if(test == 20){
				test=1;
				ncores--;
				if(ncores==-1){
					rainbowlerping = false;
					inicialcolor = transform.GetComponent<Renderer>().material.color;
					finalcolor = Color.white;
					foreach(GameObject cubo in gameController.cubitos){
						cubo.GetComponent<Renderer>().material.color = cubo.GetComponent<movement>().color;
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
			StartCoroutine(FadeTo(text6, 0.0f,1.0f,1));
		}
	}

	void multiplierEffect()
	{

		if (posMulti == 0) 
		{
			text7.text="";
			text7.color=new Color(text7.color.r,text7.color.g,text7.color.b,0.0f);
		}
		else
		{
			text7.color=new Color(text7.color.r,text7.color.g,text7.color.b,0.0f);
			text7.text="x"+multiplier[posMulti];
			StartCoroutine(FadeTo(text7,1.0f,0.5f,0));
		}

	}

	IEnumerator FadeTo(Text text,float aValue, float aTime, int tipo)
	{
		float alpha = text.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(alpha,aValue,t));
			text.color = newColor;
			yield return null;
		}
		if (tipo == 1)
		{
			text.color = new Color(text6.color.r, text6.color.g, text6.color.b, 0.0f);
		}
	}

	IEnumerator FadeToDif(Text text, float aTime)
	{
		float alpha = text.color.a;
		if(text.color.a>0.0f)
		{
			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
			{
				Color newColor = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(alpha,0.0f,t));
				text.color = newColor;
				yield return null;
			}
		}
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(alpha,1.0f,t));
			text.color = newColor;
			yield return null;
		}
	}

	void superSayainmode(){
		timeLeftAux = gameController.timeLeft;
		//print (timeLeftAux);
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