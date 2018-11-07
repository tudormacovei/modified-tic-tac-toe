using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // "Singleton" implementation
    // The GameManager Class is a singleton, which is good pracice (apparently)
    // I don't understand how this works quite yet, but I'll get there
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
                Debug.Log("UIManager initialized");
            }

            return _instance;
        }
    }

    // not necessary in this implementation as we only have 1 level
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public Text TextPlayer, TextPosWarning;
    public Color ColorPlayer_1;
    public Color ColorPlayer_2;

    // Use this for initialization
    void Start ()
    {
        PlayerMovingUI();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    // To prevent duplicates
    public void MainMenu()
    {
        Object.Destroy(gameObject);
    }

    public void PlayerMovingUI()
    {
        if (GameManager.Instance.PlayerMoving() == 1)
        {
            TextPlayer.text = "X";
            TextPlayer.color = ColorPlayer_1;
        }
        else
        {
            TextPlayer.text = "O";
            TextPlayer.color = ColorPlayer_2;
        }
    }

    private void ClearPosWarning()
    {
        TextPosWarning.text = "-";
    }

    public void PositionInvalid()
    {
        TextPosWarning.text = "INVALID MOVE!";
        Invoke("ClearPosWarning", 2);
    }

    public void OuterMoveInvalid()
    {
        TextPosWarning.text = "ONLY 1 OUTSIDE MOVE ALLOWED";
        Invoke("ClearPosWarning", 2);
    }
}
