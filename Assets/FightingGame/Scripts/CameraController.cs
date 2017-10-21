using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController instance;

    [SerializeField]
    private GameObject player1;

    [SerializeField]
    private GameObject player2;

    

	void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    
    // Use this for initialization
	IEnumerator Start ()
    {
        yield return new WaitForEndOfFrame();

        PlayerController[] playerController = FindObjectsOfType(typeof(PlayerController)) as PlayerController[];

        foreach (PlayerController pc in playerController)
        {
            if(pc.playerNumber == 0)
            {
                player1 = pc.gameObject;
            }
            else if (pc.playerNumber == 1)
            {
                player2 = pc.gameObject;
            }
        }

       

        float middle = (player2.transform.position.x - player1.transform.position.x) / 2;
        transform.position = new Vector3(player2.transform.position.x - middle, 2.18f, -4.17f);
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = (player1.transform.position - player2.transform.position).x;
        float middle = distance / 2;

        //if player 2 is on the right, distance will be negative
        if (distance < 0)
        {
            Debug.Log("Player2 is on the right");
            transform.position = new Vector3(player2.transform.position.x + middle, transform.position.y, transform.position.z);
        }
        else if(distance > 0)//means player1 is on the right
        {
            Debug.Log("Player1 is on the right");
            transform.position = new Vector3(player2.transform.position.x + middle, transform.position.y, transform.position.z);
        }
        else if(distance == 0)
        {
            transform.position = new Vector3(player2.transform.position.x , transform.position.y, transform.position.z);
        }
	}
}
