using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	GameObject playerInput;

	void Start(){
		playerInput = GameObject.Find("PlayerInput");
	}

	void Update(){
		if(ControlMapper.GetButton(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.Right)){

		}
	}
}
