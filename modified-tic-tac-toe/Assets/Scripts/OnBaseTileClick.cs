using System;
using UnityEngine;

public class OnBaseTileClick : MonoBehaviour
{
    public GameObject Cross, Nought;
    
    // Use this function to get the integer part of our object name
    // TODO: Convert array names to a 0-index
    private int GetTile()
    {
        string suffix = string.Empty;
        int startIndex = gameObject.name.IndexOf('_');
        suffix = gameObject.name.Substring(startIndex + 1);
        return int.Parse(suffix);
    }

    // We detect the correct matrix position by using the name of the object
    private void OnMouseDown ()
    {
        int tile = new int();
        tile = GetTile();
        GameManager.Instance.GameUpdate(Cross, Nought, tile);
    }
}
