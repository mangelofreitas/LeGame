using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject block;
	public float startWait;
	public GameObject[] cubosPrincipal;
	public GameObject[] cubosLateralEsq;
	public GameObject[] cubosLateralDir;
	public GameObject[] cubosBaixo;
	public GameObject parent;
	private List<GameObject> cubitos = new List<GameObject>();
	private static Color[] cores = {Color.red,Color.blue,Color.green,Color.white};
	private movement movimento;
	private float timeLeft = 20.0f;
	private float timeoccurred = 0.0f;
	public float xspeed;
	private float tempo = 2.0f;
	private bool restart = false;

	public Text scoreText;
	private int score;

	public int contadorRed;
	private int contadorGreen;
	private int contadorBlue;
	public Text countRed;
	public Text countGreen;
	public Text countBlue;

	void Start () {
		score = 0;
		contadorRed = 0;
		contadorBlue = 0;
		contadorGreen = 0;
		countRed.text = "Red: ";
		countRed.color = Color.red;
		countGreen.text = "Green: ";
		countGreen.color = Color.green;
		countBlue.text = "Blue: ";
		countBlue.color = Color.blue;	
//		UpdateCounters ();
		UpdateScore ();
		StartCoroutine(SpawnWaves ());
	}

	void Update()
	{
		timeLeft -= Time.deltaTime;
		timeoccurred += Time.deltaTime;
		if(Input.GetKey(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		} 
		if (restart) 
		{
			foreach (Touch touch in Input.touches) {
				if ((touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled))
				{
					Application.LoadLevel(Application.loadedLevel);
				}
			}
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while(true)
		{
			if(cubitos.Count>0){
				foreach(GameObject cubo in cubitos){
					movimento.speed = movimento.speed*xspeed;
				}
			}
			Quaternion spawnRotation = Quaternion.identity;
			GameObject child = Instantiate(block, cubosPrincipal[Random.Range(0,3)].transform.position, spawnRotation) as GameObject;
			child.transform.SetParent(parent.transform);
			child.renderer.material.SetColor("_Color",cores[Random.Range(0,4)]);
			movimento = child.GetComponent<movement>();
			cubitos.Add(child);
			if(((int)timeoccurred%10 == 0)&&(int)timeoccurred!=0 && timeoccurred<80){
				xspeed += xspeed * 0.25f;
				tempo -= tempo * 0.15f;
			}
			else if((int)timeoccurred%10 == 0 && timeoccurred>=80)
			{
				xspeed += xspeed * 0.04f;
				tempo -= tempo * 0.05f;
			}
			movimento.speed = movimento.speed * xspeed;
			GameObject child2 = Instantiate(block, cubosLateralEsq[Random.Range(0,3)].transform.position, spawnRotation) as GameObject;
			child2.transform.SetParent(parent.transform);
			child2.renderer.material.SetColor("_Color",cores[Random.Range(0,4)]);
			cubitos.Add(child2);
			movimento = child2.GetComponent<movement>();
			movimento.speed = movimento.speed * xspeed;
			GameObject child3 = Instantiate(block, cubosLateralDir[Random.Range(0,3)].transform.position, spawnRotation) as GameObject;
			child3.transform.SetParent(parent.transform);
			child3.renderer.material.SetColor("_Color",cores[Random.Range(0,4)]);
			cubitos.Add(child3);
			movimento = child3.GetComponent<movement>();
			movimento.speed = movimento.speed * xspeed;
			GameObject child4 = Instantiate(block, cubosBaixo[Random.Range(0,3)].transform.position, spawnRotation) as GameObject;
			child4.transform.SetParent(parent.transform);
			child4.renderer.material.SetColor("_Color",cores[Random.Range(0,4)]);
			cubitos.Add(child4);
			movimento = child4.GetComponent<movement>();
			movimento.speed = movimento.speed * xspeed;
			yield return new WaitForSeconds(tempo);
			if(restart)
			{
				break;
			}

		}
	}
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	public void addContador(Color cor){
		if( cor == Color.red)
		{
			contadorRed++;
			if(contadorRed<4)countRed.text = ((string)countRed.text)+="|";
		}
		else if( cor == Color.blue)
		{
			contadorBlue++;
			if(contadorBlue<4)countBlue.text = ((string)countBlue.text)+="|";
		}
		else if(cor == Color.green)
		{
			contadorGreen++;
			if(contadorGreen<4)countGreen.text = ((string)countGreen.text)+="|";
		}
		//UpdateCounters();
	}
	public void removeCube(GameObject cubo){
		cubitos.Remove (cubo);
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	/*void UpdateCounters(){
		if(contadorRed<3)
			countRed.text ("|");
		if(contadorGreen<3)
			countGreen.text.Concat();
		if(contadorBlue<3)
			countBlue.text.Concat("|");
	}*/	

	public void gameOver()
	{
		restart = true;
	}


}