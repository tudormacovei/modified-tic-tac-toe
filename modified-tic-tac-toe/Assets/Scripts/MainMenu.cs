using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text MoveSetText;

    // Use this for initialization
	void Start ()
    {
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

    public void SetMove()
    {
        GameManager.Instance.SwitchPlayer();
        if (GameManager.Instance.PlayerMoving() == 1)
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