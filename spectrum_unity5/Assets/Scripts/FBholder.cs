using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FBholder : MonoBehaviour {

	public GameObject UIFBIsLoggedIn;
	public GameObject UIFBIsNotLoggedIn;
	public GameObject UIFBAvatar;
	public GameObject UIFBUsername;
	public Text scoreText;
	private Dictionary <string,string> profile = null;

	private void Awake()
	{
		FB.Init (SetInit, onHideUnity);
	}

	private void SetInit()
	{
		Debug.Log ("FB Init done!");

		if (FB.IsLoggedIn) 
		{
			Debug.Log ("FB Logged In!");
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
		FB.Login ("email", AuthCallback);
	}

	private void AuthCallback(FBResult result)
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
			FB.API(Util.GetPictureURL("me",128,128),Facebook.HttpMethod.GET,DealWithProfilePicture);
			FB.API ("/me?fields=id,first_name",Facebook.HttpMethod.GET,DealWithUserName);
			//buscar o codigo do username
		}
		else
		{
			UIFBIsLoggedIn.SetActive(false);
			UIFBIsNotLoggedIn.SetActive(true);
		}
	}

	private void DealWithUserName(FBResult result)
	{
		if(result.Error != null)
		{
			Debug.Log ("problem with getting username");
			FB.API ("/me?fields=id,first_name",Facebook.HttpMethod.GET,DealWithUserName);
			return;
		}
		profile = Util.DeserializeJSONProfile (result.Text);
		Text UserName = UIFBUsername.GetComponent<Text>();
		UserName.text = "Welcome, " + profile["first_name"];
	}

	private void DealWithProfilePicture(FBResult result)
	{
		if(result.Error != null)
		{
			Debug.Log ("problem with getting profile picture");
			FB.API(Util.GetPictureURL("me",128,128),Facebook.HttpMethod.GET,DealWithProfilePicture);
			return;
		}
		Image UserAvatar = UIFBAvatar.GetComponent<Image>();
		UserAvatar.sprite = Sprite.Create (result.Texture,new Rect(0,0,128,128),new Vector2(0,0));
	}

	public void ShareWithFriends()
	{
		/*Application.CaptureScreenshot ("Assets/imagens/screenshot.png");
		string _path = "file://" + System.IO.Path.Combine(Application.dataPath, "imagens/screenshot.png");*/
		FB.Feed(
			linkCaption:"Look at my score: "+scoreText.text+" points! Try to beat me!",
			picture: "https://scontent-mad.xx.fbcdn.net/hphotos-xfp1/v/t1.0-9/11002507_887929541257887_8668410503360177524_n.jpg?oh=6aabbaaf0e4d1cc4e2fc6db39731e318&oe=559AA7F6",
			linkName: "Spectrum the game",
			link: "https://student.dei.uc.pt/~mfreitas/"
		);
	}




}
