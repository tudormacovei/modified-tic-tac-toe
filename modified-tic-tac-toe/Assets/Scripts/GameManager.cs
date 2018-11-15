using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // "Singleton" implementation
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                Debug.Log("GameManager initialized");
            }
            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public GameBoard Board;

    public int GameState { get; set; }

    public void GameUpdate(GameObject cross, GameObject nought, int tile)
    {
        Board.BoardUpdate(cross, nought, tile);
        if (GameState != 0)
            NextScene();
    }

    // To prevent duplicates when reloading scenes
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Object.Destroy(gameObject);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.Instance.PickMusic(SceneManager.GetActiveScene().buildIndex + 1);
        if (GameState != -1 && GameState != 0)
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.WinAudio();
        }
    }

    private void Start()
    {
        AudioManager.Instance.PickMusic(0);
        Board = gameObject.AddComponent<GameBoard>();
    }
}
