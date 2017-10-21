using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

	public int healthPointMax = 10;
	public int healthPointCurr = 10;
	[HideInInspector]
	public Scrollbar healthBarUI;

	void Start(){	
		if(GetComponent<PlayerController>().playerNumber == 0){
			healthBarUI = GameObject.Find("Player1UI").GetComponentInChildren<Scrollbar>();
		}
		else if(GetComponent<PlayerController>().playerNumber == 1){
			healthBarUI = GameObject.Find("Player2UI").GetComponentInChildren<Scrollbar>();
		}

		healthPointCurr = healthPointMax;
	}

}
