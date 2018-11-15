using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    private static int boardSize = 5;
    private int[,] gameMatrix = new int[boardSize, boardSize];
    private int player = new int(); //the player that will place the next piece, 1 = X, 2 = O
    private int outerMoveX;
    private int outerMoveO;
    private bool outerMoveTry;

    public GameBoard()
    {
        for (int i = 0; i < boardSize; i++)
            for (int j = 0; j < boardSize; j++)
                gameMatrix[i, j] = 0;
        player = 1;
        outerMoveX = 0;
        outerMoveO = 0;
        outerMoveTry = false;
    }

    public int Player
    {
        get
        {
            return player;
        }
        set
        {
            
        }
    }

    public void SwitchPlayer()
    {
        if (player == 1)
            player = 2;
        else
            player = 1;
    }

    public int GameStateCheck(int x, int y)
    {
        int winner = new int();
        winner = GameWin(x, y);
        if (winner != 0)
            return winner;
        // the end move could be the last one but shouldn't call a draw
        if (AllMovesInvalid())
            return -1;
        // default
        return 0;
    }

    public void BoardUpdate(GameObject cross, GameObject nought, int tile)
    {
        int x = new int();
        int y = new int();
        TileToXY(tile, ref x, ref y);

        if (OuterTile(tile))
            OuterMove();
        if (ValidPosition(x, y))
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
        GameManager.Instance.GameState = GameStateCheck(x, y);
        if (GameManager.Instance.GameState != 0)
            GameManager.Instance.NextScene();
    }

    private void OuterMove()
    {
        if (player == 1)
            outerMoveX++;
        else
            outerMoveO++;
        outerMoveTry = true;
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
    private int GameWin(int x, int y)
    {
        for (int i = 0; i < boardSize; i++)
            for (int j = 0; j < boardSize; j++)
                if (WinInXY(i, j) != 0)
                    return WinInXY(i, j);
        return 0;
    }

    private bool ValidPosition(int x, int y)
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
    private bool OuterTile(int tile)
    {
        Debug.Log(tile);
        if ((0 <= tile && tile < boardSize) || (boardSize * (boardSize - 1) <= tile &&
                                                tile < boardSize * (boardSize - 1) + boardSize))
            return true;
        if (tile % boardSize == 0 || (tile - 1) % boardSize == 0)
            return true;
        return false;
    }

    private bool FullBoard()
    {
        for (int i = 1; i < boardSize - 1; i++)
            for (int j = 1; j < boardSize - 1; j++)
                // inner board is not full
                if (gameMatrix[i, j] == 0)
                    return false;
        return true;
    }

    private bool AllMovesInvalid()
    {
        if (!FullBoard())
            return false;
        // board is full
        if (outerMoveX > 0 && player == 1)
            return true;
        if (outerMoveO > 0 && player == 2)
            return true;
        return false;
    }
}
