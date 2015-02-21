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
	private movement movimento;
	private static Color[] cores = {Color.red,Color.blue,Color.green,Color.white};
	public Text timeLeftText;
	public float timeLeft = 0.0f;
	private float timeoccurred = 0.0f;
	public float xspeed;
	private float tempo = 2.0f;
	private bool restart = false;

	void Start () {
		StartCoroutine(SpawnWaves ());
	}

	void Update()
	{	
		UpdateTimeLeft ();
		if (timeLeft <= 0.0f) {
			gameOver ();
		} else {
			timeLeft -= Time.deltaTime;
			timeoccurred += Time.deltaTime;
		}
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
			/*if(cubitos.Count>0){
				foreach(GameObject cubo in cubitos){
					movimento.speed = movimento.speed*xspeed;
				}
			}*/
			if(((int)timeoccurred%10 == 0)&&(int)timeoccurred!=0 && timeoccurred<80){
				xspeed += xspeed * 0.25f;
				tempo -= tempo * 0.15f;
			}
			else if((int)timeoccurred%10 == 0 && timeoccurred>=80)
			{
				xspeed += xspeed * 0.04f;
				tempo -= tempo * 0.05f;
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

	public void SpawnCube(GameObject[] arrayCubos)
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
		timeLeftText.text = "Timer: " +(int) timeLeft;
	}

	public void removeCube(GameObject cubo){
		cubitos.Remove (cubo);
	}

	public void gameOver()
	{
		restart = true;
		Time.timeScale = 0f;
	}


}