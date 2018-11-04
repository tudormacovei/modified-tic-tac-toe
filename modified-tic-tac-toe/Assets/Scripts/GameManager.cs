using UnityEngine;

public class GameManager : MonoBehaviour
{
    //"Singleton" implementation
    //The GameManager Class is a singleton, which is good pracice (apparently)
    //I don't understand how this works quite yet, but I'll get there
    //<BLACK MAGIC>
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                Debug.Log("GameManager initialized");
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    //</BLACK MAGIC>

    private int[,] GameMatrix = new int[3, 3];
    private int player = new int(); //the player that will place the next piece, 1 = X, 2 = O

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i <= 2; i++)
            for (int j = 0; j <= 2; j++)
                GameMatrix[i, j] = 0;
        player = 1;
    }

    // Update is called once per frame
    void Update ()
    {
	}

    private void TileToXY(int tile, ref int x, ref int y)
    {
        tile = tile - 1;
        Debug.Log("tile: " + tile + "\n");
        x = tile % 3;
        y = tile / 3;
    }

    //TODO: Place GameMatrix functions in a separate file, only results should be visible
    //      in the GameManager
    //checks if (x, y) is the "center" of a win
    //returns what player won if it is a win
    private int WinInXY(int x, int y)
    {
        int ok = new int();
        ok = GameMatrix[x, y];

        if (ok == 0)
            return 0;
        //Main diagonal
        for (int i = -1; i <= 1; i++)
            if (!(x + i >= 0 && x + i <= 2 && y + i >= 0 && y + i <= 2))
                ok = 0;
            else if (GameMatrix[x + i, y + i] != ok)
                ok = 0;
        if (ok != 0)
            return ok;
        //Secondary diagonal
        ok = GameMatrix[x, y];
        for (int i = -1; i <= 1; i++)
            if (!(x - i >= 0 && x - i <= 2 && y + i >= 0 && y + i <= 2))
                ok = 0;
            else if (GameMatrix[x - i, y + i] != ok)
                ok = 0;
        if (ok != 0)
            return ok;
        //row Y
        ok = GameMatrix[x, y];
        for (int i = -1; i <= 1; i++)
            if (!(x + i >= 0 && x + i <= 2 && y >= 0 && y <= 2))
                ok = 0;
            else if (GameMatrix[x + i, y] != ok)
                ok = 0;
        if (ok != 0)
            return ok;
        //col X
        ok = GameMatrix[x, y];
        for (int i = -1; i <= 1; i++)
            if (!(x >= 0 && x <= 2 && y + i >= 0 && y + i <= 2))
                ok = 0;
            else if (GameMatrix[x, y + i] != ok)
                ok = 0;
        if (ok != 0)
            return ok;
        return 0;
    }

    //returns 0 as the default
    //returns 1 if player 1 (X) wins, 2 if player 2 (O) wins
    private int GameState(int x, int y)
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (WinInXY(i, j) != 0)
                    return WinInXY(i, j);
        return 0;
    }

    public void GameBoardUpdate(GameObject cross, GameObject nought, int tile)
    {
        int x = new int();
        int y = new int();
        TileToXY(tile, ref x, ref y);

        Debug.Log("x: " + x + "\n y: " + y);
        if (player == 1)
        {
            GameMatrix[x, y] = 1;
            Object.Instantiate(cross, new Vector3(2.2f * (x - 1), -2.2f * (y - 1), 0), transform.rotation, null);
            player = 2;
        }
        else
        {
            GameMatrix[x, y] = 2;
            Object.Instantiate(nought, new Vector3(2.2f * (x - 1), -2.2f * (y - 1), 0), transform.rotation, null);
            player = 1;
        }
        Debug.Log("GameState: " + GameState(x, y));
    }
}
