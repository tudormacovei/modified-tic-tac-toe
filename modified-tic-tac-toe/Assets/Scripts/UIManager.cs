using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // "Singleton" implementation
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

    public Text TextPlayer, TextPosWarning;
    public Color ColorPlayer_1;
    public Color ColorPlayer_2;
    private GameBoard Board;

    // Use this for initialization
    void Start ()
    {
        Board = GameManager.Instance.Board;
        PlayerMovingUI();
    }

    public void PlayerMovingUI()
    {
        if (Board.Player == 1)
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
