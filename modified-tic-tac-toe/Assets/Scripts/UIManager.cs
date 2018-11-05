using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //"Singleton" implementation
    //The GameManager Class is a singleton, which is good pracice (apparently)
    //I don't understand how this works quite yet, but I'll get there
    //<BLACK MAGIC>
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

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    //</BLACK MAGIC>

    public Text TextPlayer, TextPosWarning;
    public Color ColorPlayer_1;
    public Color ColorPlayer_2;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	
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
}
