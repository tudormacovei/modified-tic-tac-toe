using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // "Singleton" implementation
    // The GameManager Class is a singleton, which is good pracice (apparently)
    // I don't understand how this works quite yet, but I'll get there
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

    private static int boardSize = 5;
    private int[,] gameMatrix = new int[boardSize, boardSize];
    private int player = new int(); //the player that will place the next piece, 1 = X, 2 = O
    private int outerMoveX;
    private int outerMoveO;
    private bool outerMoveTry;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < boardSize; i++)
            for (int j = 0; j < boardSize; j++)
                gameMatrix[i, j] = 0;
        player = 1;
        outerMoveX = 0;
        outerMoveO = 0;
        outerMoveTry = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // To prevent duplicates when replaying scenes
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Object.Destroy(gameObject);
    }

    private void outerMove()
    {
        if (player == 1)
            outerMoveX++;
        else
            outerMoveO++;
        outerMoveTry = true;
    }

    public void SwitchPlayer()
    {
        if (player == 1)
            player = 2;
        else
            player = 1;
    }

    public int PlayerMoving()
    {
        return player;
    }

    private void TileToXY(int tile, ref int x, ref int y)
    {
        tile = tile - 1;
        x = tile % boardSize;
        y = tile / boardSize;
    }

    // Checks if (x, y) is the "center" of a win
    // Returns what player won if it is a win
    private int WinInXY(int x, int y)
    {
        int ok = new int();
        ok = gameMatrix[x, y];

        if (ok == 0)
            return 0;
        // Main diagonal
        for (int i = -1; i <= 1; i++)
            if (!(x + i >= 0 && x + i < boardSize && y + i >= 0 && y + i < boardSize))
                ok = 0;
            else if (gameMatrix[x + i, y + i] != ok)
                ok = 0;
        if (ok != 0)
            return ok;
        // Secondary diagonal
        ok = gameMatrix[x, y];
        for (int i = -1; i <= 1; i++)
            if (!(x - i >= 0 && x - i < boardSize && y + i >= 0 && y + i < boardSize))
                ok = 0;
            else if (gameMatrix[x - i, y + i] != ok)
                ok = 0;
        if (ok != 0)
            return ok;
        // Row Y
        ok = gameMatrix[x, y];
        for (int i = -1; i <= 1; i++)
            if (!(x + i >= 0 && x + i < boardSize && y >= 0 && y < boardSize))
                ok = 0;
            else if (gameMatrix[x + i, y] != ok)
                ok = 0;
        if (ok != 0)
            return ok;
        // Column X
        ok = gameMatrix[x, y];
        for (int i = -1; i <= 1; i++)
            if (!(x >= 0 && x < boardSize && y + i >= 0 && y + i < boardSize))
                ok = 0;
            else if (gameMatrix[x, y + i] != ok)
                ok = 0;
        if (ok != 0)
            return ok;
        return 0;
    }

    // Returns 0 as the default
    // Returns 1 if player 1 (X) wins, 2 if player 2 (O) wins
    // TODO: Draw detection
    private int GameState(int x, int y)
    {
        for (int i = 0; i < boardSize; i++)
            for (int j = 0; j < boardSize; j++)
                if (WinInXY(i, j) != 0)
                    return WinInXY(i, j);
        return 0;
    }

    private bool validPosition(int x, int y)
    {
        if (outerMoveTry)
        {
            outerMoveTry = false;
            if (player == 1 && outerMoveX > 1)
            {
                UIManager.Instance.OuterMoveInvalid();
                return false;
            }
            if (player == 2 && outerMoveO > 1)
            {
                UIManager.Instance.OuterMoveInvalid();
                return false;
            }
        }
        if (gameMatrix[x, y] != 0)
        {
            //UI action: invalid position
            UIManager.Instance.PositionInvalid();
            return false;
        }
        return true;
    }

    // uses the index of a tile to detect its position
    private bool outerTile(int tile)
    {
        if (0 <= tile && tile < boardSize || boardSize * (boardSize - 1) <= tile &&
            tile < boardSize * (boardSize - 1) + boardSize)
            return true;
        if (tile % boardSize == 0 || (tile + 1 % boardSize) == 0)
            return true;
        return false;
    }

    public void GameBoardUpdate(GameObject cross, GameObject nought, int tile)
    {
        int x = new int();
        int y = new int();
        TileToXY(tile, ref x, ref y);

        if (outerTile(tile))
            outerMove();
        if (validPosition(x, y))
            if (player == 1)
            {
                gameMatrix[x, y] = 1;
                Object.Instantiate(cross, new Vector3(2 * (x - 2), -2 * (y - 2), 0), transform.rotation, null);
                player = 2;
                UIManager.Instance.PlayerMovingUI();
            }
            else
            {
                gameMatrix[x, y] = 2;
                Object.Instantiate(nought, new Vector3(2 * (x - 2), -2 * (y - 2), 0), transform.rotation, null);
                player = 1;
                UIManager.Instance.PlayerMovingUI();
            }
        // Game end condition
        if (GameState(x, y) != 0)
        {
            // Play audio here
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
