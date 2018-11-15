using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameMusic;
    public AudioClip MenuMusic;
    public AudioClip WinEffect;
    private AudioSource music;

    // "Singleton" implementation
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
                Debug.Log("AudioManager initialized");
            }
            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void MuteToggle()
    {
        if (music.volume != 0)
            music.volume = 0;
        else
            music.volume = 100;
        
    }

    // Picks music using the current context
    public void PickMusic(int levelIndex)
    {
        StopMusic();
        if (levelIndex == 1)
        {
            music.clip = GameMusic;
        }
        else
        {
            music.clip = MenuMusic;
        }
        music.Play();
        music.loop = true;
    }

    public void StopMusic()
    {
        music.loop = false;
        music.Stop();
    }

    public void WinAudio()
    {
        music.PlayOneShot(WinEffect);
    }

    void Start()
    {
        music = Instance.GetComponent<AudioSource>();
    }
}
