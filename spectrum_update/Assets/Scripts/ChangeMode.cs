using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChangeMode : MonoBehaviour
{
    public GameObject ProModeActive;
    public GameObject ProModeUnActive;
    public GameObject NormalModeActive;
    public GameObject NormalModeUnActive;
    public GameObject BackgroundMusicUnactive;
    public GameObject BackgroundMusicActive;
    public GameObject SoundEffectsUnactive;
    public GameObject SoundEffectsActive;

    public void Start () {
        if(PlayerPrefs.GetInt("Mode")==0)
        {
            NormalModeActive.SetActive(true);
            NormalModeUnActive.SetActive(false);
            ProModeActive.SetActive(false);
            ProModeUnActive.SetActive(true);
        }
        else if(PlayerPrefs.GetInt("Mode") == 1)
        {
            NormalModeActive.SetActive(false);
            NormalModeUnActive.SetActive(true);
            ProModeActive.SetActive(true);
            ProModeUnActive.SetActive(false);
        }
        if(PlayerPrefs.GetInt("BackgroundMusic") == 1)
        {
            BackgroundMusicActive.SetActive(false);
            BackgroundMusicUnactive.SetActive(true);
        }
        else if(PlayerPrefs.GetInt("BackgroundMusic") == 0)
        {
            BackgroundMusicActive.SetActive(true);
            BackgroundMusicUnactive.SetActive(false);
        }
        if (PlayerPrefs.GetInt("SoundEffects") == 1)
        {
            SoundEffectsActive.SetActive(false);
            SoundEffectsUnactive.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("SoundEffects") == 0)
        {
            SoundEffectsActive.SetActive(true);
            SoundEffectsUnactive.SetActive(false);
        }
    }

    public void ChangeBackgroundMusic()
    {
        if (PlayerPrefs.GetInt("BackgroundMusic") == 1)
        {
            BackgroundMusicActive.SetActive(true);
            BackgroundMusicUnactive.SetActive(false);
            PlayerPrefs.SetInt("BackgroundMusic", 0);
        }
        else if (PlayerPrefs.GetInt("BackgroundMusic") == 0)
        {
            BackgroundMusicActive.SetActive(false);
            BackgroundMusicUnactive.SetActive(true);
            PlayerPrefs.SetInt("BackgroundMusic", 1);
        }
    }

    public void ChangeSoundEffects()
    {
        if (PlayerPrefs.GetInt("SoundEffects") == 1)
        {
            SoundEffectsActive.SetActive(true);
            SoundEffectsUnactive.SetActive(false);
            PlayerPrefs.SetInt("SoundEffects",0);
        }
        else if (PlayerPrefs.GetInt("SoundEffects") == 0)
        {
            SoundEffectsActive.SetActive(false);
            SoundEffectsUnactive.SetActive(true);
            PlayerPrefs.SetInt("SoundEffects", 1);
        }
    }

	public void Click () {
        if (PlayerPrefs.GetInt("Mode") == 1)
        {
            PlayerPrefs.SetInt("Mode",0);
            NormalModeActive.SetActive(true);
            NormalModeUnActive.SetActive(false);
            ProModeActive.SetActive(false);
            ProModeUnActive.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Mode") == 0)
        {
            PlayerPrefs.SetInt("Mode", 1);
            NormalModeActive.SetActive(false);
            NormalModeUnActive.SetActive(true);
            ProModeActive.SetActive(true);
            ProModeUnActive.SetActive(false);
        }
    }
}
