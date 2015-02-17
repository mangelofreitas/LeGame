using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject block;
	public float startWait;
	public GameObject[] cubosPrincipal;
	public GameObject[] cubosLateralEsq;
	public GameObject[] cubosLateralDir;
	public GameObject[] cubosBaixo;
	public GameObject parent;
	private static Color[] cores = {Color.red,Color.blue,Color.green,Color.white};
	private movement movimento;
	private float timeLeft = 20.0f;
	private float timeoccurred = 0.0f;
	public float xspeed;
	private float tempo = 2.0f;

	void Start () {
		StartCoroutine(SpawnWaves ());
	}

	void Update()
	{
		timeLeft -= Time.deltaTime;
		timeoccurred += Time.deltaTime;
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while(true)
		{
			Quaternion spawnRotation = Quaternion.identity;
			GameObject child = Instantiate(block, cubosPrincipal[Random.Range(0,3)].transform.position, spawnRotation) as GameObject;
			child.transform.SetParent(parent.transform);
			child.renderer.material.SetColor("_Color",cores[Random.Range(0,4)]);
			movimento = child.GetComponent<movement>();
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
			movimento = child2.GetComponent<movement>();
			movimento.speed = movimento.speed * xspeed;
			GameObject child3 = Instantiate(block, cubosLateralDir[Random.Range(0,3)].transform.position, spawnRotation) as GameObject;
			child3.transform.SetParent(parent.transform);
			child3.renderer.material.SetColor("_Color",cores[Random.Range(0,4)]);
			movimento = child3.GetComponent<movement>();
			movimento.speed = movimento.speed * xspeed;
			GameObject child4 = Instantiate(block, cubosBaixo[Random.Range(0,3)].transform.position, spawnRotation) as GameObject;
			child4.transform.SetParent(parent.transform);
			child4.renderer.material.SetColor("_Color",cores[Random.Range(0,4)]);
			movimento = child4.GetComponent<movement>();
			movimento.speed = movimento.speed * xspeed;
			yield return new WaitForSeconds(tempo);

		}
	}
}
