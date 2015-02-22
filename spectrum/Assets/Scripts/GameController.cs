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
	public GameObject prism;
	public List<GameObject> cubitos = new List<GameObject>();
	private movement movimento;
	private static Color[] cores = {Color.red,Color.blue,Color.green,Color.white};
	public Text timeLeftText;
	public float timeLeft;
	private float timeoccurred = 0.0f;
	public float xspeed;
	public float tempo;
	private bool restart = false;
	public GameObject lost;
	public Text text1;
	public Text text2;
	public Text text3;
	public Text text4;
	public Text text5;
	public Text finalscore;
	public float timeInit;
	public bool next = true;
	private float speedActual;
	private float timeActual;
	private float toccurred;

	void Start () {
		Time.timeScale = 1f;
		StartCoroutine(SpawnWaves ());
	}

	void Update()
	{	
		if (!prism.GetComponent<Colider> ().rainbowlerping) {
			UpdateTimeLeft ();
			if (timeLeft <= 0.0f) {
				gameOver ();
			} else {
				timeLeft -= Time.deltaTime;
				timeoccurred += Time.deltaTime;
			}
		} 
		else {
			timeLeft = prism.GetComponent<Colider>().timeLeftAux;
		}
		if(Input.GetKey(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while(true)
		{

			if((int)timeoccurred%10 == 0 && timeoccurred!=0)
			{
				if(cubitos.Count>0){
					foreach(GameObject cubo in cubitos){
						cubo.GetComponent<movement>().speed=cubo.GetComponent<movement>().speed*xspeed;
					}
				}
				xspeed += xspeed * 0.05f;
				tempo -= tempo * 0.01f;
			}
			SpawnCube(cubosPrincipal);
			SpawnCube(cubosLateralEsq);
			SpawnCube(cubosLateralDir);
			SpawnCube(cubosBaixo);
			yield return new WaitForSeconds(tempo);
			if(restart)
			{
				break;
			}
		}
	}

	void SpawnCube(GameObject[] arrayCubos)
	{
		Quaternion spawnRotation = Quaternion.identity;
		GameObject child = Instantiate(block, arrayCubos[Random.Range(0,3)].transform.position, spawnRotation) as GameObject;
		child.transform.SetParent(parent.transform);
		child.renderer.material.SetColor("_Color",cores[Random.Range(0,4)]);
		movimento = child.GetComponent<movement>();
		movimento.speed = movimento.speed * xspeed;
		cubitos.Add(child);
	}

	void UpdateTimeLeft(){
		timeLeft = Mathf.Round (timeLeft * 100f) / 100f;
		timeLeftText.text = "" +(int) timeLeft;

	}

	public void removeCube(GameObject cubo){
		cubitos.Remove (cubo);
	}

	public void gameOver()
	{
		Time.timeScale = 0f;
		lost.SetActive(true);
		timeLeftText.enabled = false;
		finalscore.text = text4.text;
		text1.enabled = false;
		text2.enabled = false;
		text3.enabled = false;
		text4.enabled = false;
		text5.enabled = false;
		restart = true;
	}


}