using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text WinMessage;

	// Use this for initialization
	void Start ()
    {
        if (GameManager.Instance.GameState == -1)
        {
            WinMessage.text = "Draw! [Everyone's a loser]";
            WinMessage.color = Color.grey;
        }
        else if (GameManager.Instance.GameState == 1)
        {
            WinMessage.text = "X Wins!";
            WinMessage.color = Color.red;
        }
        else
        {
            WinMessage.text = "O Wins!";
            WinMessage.color = Color.green;
        }
    }
	
    public void MainMenu()
    {
        GameManager.Instance.MainMenu();
        Debug.Log("Loading Menu");
    }

    public void EndGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
