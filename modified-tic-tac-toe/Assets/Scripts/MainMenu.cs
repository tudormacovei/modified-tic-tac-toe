using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text MoveSetText;
    private GameBoard Board;

    // Use this for initialization
	void Start ()
    {
        Board = GameManager.Instance.Board;
        MoveSetText.text = "First Move: X";
        MoveSetText.color = Color.red;
    }

    public void StartGame()
    {
        GameManager.Instance.NextScene();
    }

    public void EndGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void Mute()
    {
        AudioManager.Instance.MuteToggle();
    }

    public void SetMove()
    {
        Board.SwitchPlayer();
        if (Board.Player == 1)
        {
            MoveSetText.text = "First Move: X";
            MoveSetText.color = Color.red;
        }
        else
        {
            MoveSetText.text = "First Move: O";
            MoveSetText.color = Color.green;
        }
    }
}