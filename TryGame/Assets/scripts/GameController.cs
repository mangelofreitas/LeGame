using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour {

	public GameObject block;
	public float startWait;
	private Vector3 [] cubosPrincipal = {new Vector3 (-29f,11.9f,0f),new Vector3 (-29f,11.9f,-6.6f), new Vector3 (-29f,11.9f,6.6f)};
	private Vector3 [] cubosLateralEsq = {new Vector3 (-29f,6.6f,11.9f), new Vector3 (-29f,0,11.9f), new Vector3 (-29f,-6.6f,11.9f)};
	private Vector3 [] cubosLateralDir = {new Vector3 (-29f,6.6f,-11.9f), new Vector3 (-29f,0,-11.9f), new Vector3 (-29f,-6.6f,-11.9f)};
	public GameObject parent;
	private movement movimento;
	private float timeLeft = 20.0f;
	private float timeoccurred = 0.0f;
	public float xspeed;
	private float tmin = 1.0f;
	private float tmax = 3.0f;

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

			GameObject child = Instantiate(block, cubosPrincipal[Random.Range(0,3)], spawnRotation) as GameObject;
			child.transform.SetParent(parent.transform);
			child.renderer.material.SetColor("_Color",new Color( Random.value, Random.value, Random.value, 1.0f ));

			movimento = child.GetComponent<movement>();
			Debug.Log ("cenas ->>>>>" +(int)timeoccurred%10);
			if(((int)timeoccurred%10 == 0)&&(int)timeoccurred!=0 ){
				Debug.Log ("xpeed ->>>>" +xspeed);
				xspeed += xspeed * 0.5f;
				tmin-=0.05f;
				tmax-=0.05f;
			}
			movimento.speed = movimento.speed * xspeed;
			GameObject child2 = Instantiate(block, cubosLateralEsq[Random.Range(0,3)], spawnRotation) as GameObject;
			child2.transform.SetParent(parent.transform);
			child2.renderer.material.SetColor("_Color",new Color( Random.value, Random.value, Random.value, 1.0f ));
			movimento = child2.GetComponent<movement>();
			movimento.speed = movimento.speed * xspeed;
			GameObject child3 = Instantiate(block, cubosLateralDir[Random.Range(0,3)], spawnRotation) as GameObject;
			child3.transform.SetParent(parent.transform);
			child3.renderer.material.SetColor("_Color",new Color( Random.value, Random.value, Random.value, 1.0f ));
			movimento = child3.GetComponent<movement>();
			movimento.speed = movimento.speed * xspeed;
			yield return new WaitForSeconds(Random.Range(tmin,tmax));
		}
	}
}
