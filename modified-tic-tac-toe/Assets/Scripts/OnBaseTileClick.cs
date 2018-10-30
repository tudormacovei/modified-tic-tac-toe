using System.Collections;
using System.Collections.Generic;
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

    //How to detect which tile we clicked?
    void OnMouseDown ()
    {
        int tile = new int();
        tile = this.gameObject.name[9] - '0';
        GameManager.Instance.GameBoardUpdate(Cross, Nought, tile);
    }
    //Object.Instantiate(Cross, new Vector3(-2.2f, -2.2f, 0), transform.rotation, null);
    //spawn sprites in the game manager, when you update the matrix
}
