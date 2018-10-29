using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBaseTileClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //How to detect which tile we clicked?
    void OnMouseDown ()
    {
        Debug.Log("You Cliked a Tile");
    }
}
