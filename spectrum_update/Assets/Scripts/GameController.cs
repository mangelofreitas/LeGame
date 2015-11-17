using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public GameObject centralBlock;
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
	private float xspeed;
	public float tempo;
	private bool restart = false;
	public GameObject lost;
	public GameObject pauseM;
	public GameObject pauseB;
	public Text text1;
	public Text text2;
	public Text text3;
	public Text text4;
	public Text text5;
	public Text text7;
	public Text finalscore;
	public float timeInit;
	public bool next = true;
	private float speedActual;
	private float timeActual;
	private float toccurred;
	public Text highScoreText;
    private float velocitytime = 9f;
    public GameObject circlePos;
    public GameObject circleToSpawn;

	void Start () {
        if(PlayerPrefs.GetInt("BackgroundMusic") == 0)
        {
            GetComponent<AudioSource>().Play();
        }
        xspeed = 8;
		Time.timeScale = 1f;
		StartCoroutine(SpawnWaves ());
	}

	public void pauseMusic()
	{
        if(PlayerPrefs.GetInt("BackgroundMusic") == 0)
        {
            GetComponent<AudioSource>().Pause();
        }
	}

	public void continueMusic()
	{
        if (PlayerPrefs.GetInt("BackgroundMusic") == 0)
        {
            GetComponent<AudioSource>().Play();
        }
	}

	void Update()
	{	
		UpdateTimeLeft ();
		if (!prism.GetComponent<Colider> ().rainbowlerping) {
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
        StartCoroutine(increaseVelocity());
		while(true)
		{
            int randomPlus = Random.Range(0, 99);
            if(randomPlus > 65)
            {
                if (centralBlock.transform.rotation.eulerAngles.z==0)
                {
                    SpawnBarrier(cubosPrincipal);
                    SpawnCube(cubosLateralEsq);
                    SpawnCube(cubosLateralDir);
                    SpawnCube(cubosBaixo);
                }
                else if (centralBlock.transform.rotation.eulerAngles.z>179 && centralBlock.transform.rotation.eulerAngles.z<181)
                {
                    SpawnCube(cubosPrincipal);
                    SpawnCube(cubosLateralEsq);
                    SpawnCube(cubosLateralDir);
                    SpawnBarrier(cubosBaixo);
                }
                else if(centralBlock.transform.rotation.eulerAngles.z>269 && centralBlock.transform.rotation.eulerAngles.z<271)
                {
                    SpawnCube(cubosPrincipal);
                    SpawnBarrier(cubosLateralEsq);
                    SpawnCube(cubosLateralDir);
                    SpawnCube(cubosBaixo);
                }
                else if(centralBlock.transform.rotation.eulerAngles.z>89 && centralBlock.transform.rotation.eulerAngles.z<91)
                {
                    SpawnCube(cubosPrincipal);
                    SpawnCube(cubosLateralEsq);
                    SpawnBarrier(cubosLateralDir);
                    SpawnCube(cubosBaixo);
                }
            }
            else
            {
                SpawnCube(cubosPrincipal);
                SpawnCube(cubosLateralEsq);
                SpawnCube(cubosLateralDir);
                SpawnCube(cubosBaixo);
            }
            yield return new WaitForSeconds(tempo);
			if(restart)
			{
				break;
			}
		}
	}

    void SpawnBarrier(GameObject[] arrayCubos)
    {
        Quaternion spawnRotation = Quaternion.identity;
        for(int i=0;i<3;i++)
        {
            //int random = Random.Range(0, 3);
            GameObject child = Instantiate(block, arrayCubos[i].transform.position, spawnRotation) as GameObject;
            child.transform.SetParent(parent.transform);
            movimento = child.GetComponent<movement>();
            movimento.color = cores[3];
            movimento.speed = movimento.speed * xspeed;
            child.GetComponent<Renderer>().material.SetColor("_Color", movimento.color);
            cubitos.Add(child);
        }
    }

	void SpawnCube(GameObject[] arrayCubos)
	{
		Quaternion spawnRotation = Quaternion.identity;
		GameObject child = Instantiate(block, arrayCubos[Random.Range(0,3)].transform.position, spawnRotation) as GameObject;
		child.transform.SetParent(parent.transform);
		movimento = child.GetComponent<movement>();
		movimento.color = cores [Random.Range (0, 4)];
		movimento.speed = movimento.speed * xspeed;
		child.GetComponent<Renderer>().material.SetColor("_Color",movimento.color);
		cubitos.Add(child);
	}

	void UpdateTimeLeft(){
		timeLeft = Mathf.Round (timeLeft * 100f) / 100f;
		timeLeftText.text = "" +(int) timeLeft;

	}

	public void removeCube(GameObject cubo){
		cubitos.Remove (cubo);
	}

    public void removeCircle(GameObject circle)
    {
        cubitos.Remove(circle);
    }

	public void gameOver()
	{
		int _score = PlayerPrefs.GetInt("CurrentScore");       
		int _highscore = PlayerPrefs.GetInt("HighScore");

		_score = transform.GetComponent<ColorManagement> ().score;
		if (_score > _highscore)
		{
			PlayerPrefs.SetInt("HighScore", _score);
			highScoreText.text = "New High Score: "+_score;
			//Debug.Log(_score);
		}
		else
		{
			highScoreText.text = "High Score: "+_highscore;
			//Debug.Log(_highscore);
		}

		prism.GetComponent<swipe>().enabled = false;
		Time.timeScale = 0f;
		pauseB.SetActive (false);
		pauseM.SetActive (false);
		lost.SetActive(true);
		timeLeftText.enabled = false;
		finalscore.text = text4.text;
		text1.enabled = false;
		text2.enabled = false;
		text3.enabled = false;
		text4.enabled = false;
		text5.enabled = false;
		text7.enabled = false;
		prism.GetComponent<Colider>().text7.enabled = false;
		restart = true;
		PlayerPrefs.Save ();
	}

    IEnumerator increaseVelocity()
    {
        while (true)
        {
            if (cubitos.Count > 0)
            {
                foreach (GameObject cubo in cubitos)
                {
                    cubo.GetComponent<movement>().speed = cubo.GetComponent<movement>().speed * xspeed;
                }
            }
            if (xspeed < 31)
            {
                xspeed += xspeed * 0.04f;
                Quaternion spawnRotation = Quaternion.identity;
                GameObject child = Instantiate(circleToSpawn,circlePos.transform.position,spawnRotation) as GameObject;
                child.transform.SetParent(parent.transform);
                movimento = child.GetComponent<movement>();
                movimento.color = prism.GetComponent<Renderer>().material.color;
                movimento.speed = movimento.speed * xspeed;
                movimento.player = prism;
                child.GetComponent<Renderer>().material.SetColor("_Color", movimento.color);
                cubitos.Add(child);
            }
            if(tempo >0.1)
            {
                tempo -= tempo * 0.04f;
            }
            yield return new WaitForSeconds(velocitytime);
        }
    }
}