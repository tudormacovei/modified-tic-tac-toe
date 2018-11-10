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

    void Start()
    {
        music = Instance.GetComponent<AudioSource>();
    }

    // Picks music using the current context
    public void PickMusic(int levelIndex)
    {
        stopMusic();
        // we cold have a function in the GameManager for this
        if (levelIndex == 1)
        {
            Debug.Log("GameMusic on");
            music.clip = GameMusic;
        }
        else
        {
            Debug.Log("Menu on");
            music.clip = MenuMusic;
        }
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        music.Play();
        music.loop = true;
    }

    private void stopMusic()
    {
        music.loop = false;
        music.Stop();
    }
}
