using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Facebook.Unity;

public class FBholder : MonoBehaviour {

	public GameObject UIFBIsLoggedIn;
	public GameObject UIFBIsNotLoggedIn;
	public GameObject UIFBUsername;
	public Text scoreText;
	private Dictionary <string,string> profile = null;

	private void Awake()
	{
        if(!FB.IsInitialized)
        {
            FB.Init(SetInit, onHideUnity);
        }
		else
        {
            FB.ActivateApp();
        }
        
	}

	private void SetInit()
	{
		Debug.Log ("FB Init done!");

		if (FB.IsLoggedIn) 
		{
			Debug.Log ("FB Logged In!");
            DealWithFBMenus(true);
		}
		else
		{
			DealWithFBMenus(false);
		}
	}

	private void onHideUnity(bool isGameShown)
	{
		if(!isGameShown)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	public void FBLogin()
	{
        List<string> perms = new List<string> {"public_profile","email","user_friends"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
	}

	private void AuthCallback(ILoginResult result)
	{
		if(FB.IsLoggedIn)
		{
			Debug.Log ("FB login worked!");
			DealWithFBMenus(true);
		}
		else
		{
			Debug.Log("FB login fail!");
			DealWithFBMenus(false);
		}
	}

	private void DealWithFBMenus(bool isLoggedIn)
	{
		if(isLoggedIn)
		{
			UIFBIsLoggedIn.SetActive(true);
			UIFBIsNotLoggedIn.SetActive(false);
			//buscar o codigo de foto de perfil
			FB.API ("/me?fields=id,first_name",HttpMethod.GET,DealWithUserName);
			//buscar o codigo do username
		}
		else
		{
			UIFBIsLoggedIn.SetActive(false);
			UIFBIsNotLoggedIn.SetActive(true);
		}
	}

	private void DealWithUserName(IGraphResult result)
	{
		if(result.ResultDictionary != null)
		{
            foreach (string key in result.ResultDictionary.Keys)
            {
                Debug.Log(key + ":" + result.ResultDictionary[key].ToString());
            }
		}
		Text UserName = UIFBUsername.GetComponent<Text>();
		UserName.text = "Welcome, " + result.ResultDictionary["first_name"].ToString();
	}

	public void ShareWithFriends()
	{
		/*Application.CaptureScreenshot ("Assets/imagens/screenshot.png");
		string _path = "file://" + System.IO.Path.Combine(Application.dataPath, "imagens/screenshot.png");*/
		FB.FeedShare(
			linkCaption:"Look at my score: "+scoreText.text+" points! Try to beat me!",
			picture: new System.Uri("https://scontent-mad.xx.fbcdn.net/hphotos-xfp1/v/t1.0-9/11002507_887929541257887_8668410503360177524_n.jpg?oh=6aabbaaf0e4d1cc4e2fc6db39731e318&oe=559AA7F6"),
			linkName: "Spectrum the game",
			link: new System.Uri("https://student.dei.uc.pt/~mfreitas/")
		);
	}




}
