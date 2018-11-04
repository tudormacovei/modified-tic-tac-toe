using System;
using UnityEngine;

public class OnBaseTileClick : MonoBehaviour
{
    public GameObject Cross, Nought;
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Use this function to get the integer part of our object name
    //TODO: Convert array names to a 0-index
    int getTile()
    {
        string suffix = string.Empty;
        int startIndex = gameObject.name.IndexOf('_');
        suffix = gameObject.name.Substring(startIndex + 1);
        Debug.Log(suffix);
        return int.Parse(suffix);
    }

    //We detect the correct matrix position by using the name of the object
    void OnMouseDown ()
    {
        int tile = new int();
        tile = getTile();
        Debug.Log(tile + "pressed");
        GameManager.Instance.GameBoardUpdate(Cross, Nought, tile);
    }
}
