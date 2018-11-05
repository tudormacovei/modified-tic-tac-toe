using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text WinMessage;

	// Use this for initialization
	void Start ()
    {
        if (GameManager.Instance.PlayerMoving() == 2)
        {
            WinMessage.text = "X Wins!";
            WinMessage.color = Color.red;
        }
        else
        {
            WinMessage.text = "O Wins!";
            WinMessage.color = Color.green;
        }   
        Debug.Log("Here");
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void MainMenu()
    {
        GameManager.Instance.MainMenu();
        UIManager.Instance.MainMenu();
        Debug.Log("Loading Menu");
    }

    public void EndGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
