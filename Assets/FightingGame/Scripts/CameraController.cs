using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController instance;

    public GameObject player1;
    public GameObject player2;

    

	void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    
    // Use this for initialization
	void Start ()
    {
        float middle = (player2.transform.position.x - player1.transform.position.x) / 2;
        transform.position = new Vector3(player2.transform.position.x - middle, 2.18f, -4.17f);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
