using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	GameObject playerInput;
	//GameObject inputBuffer;
	Animator animator;

	void Start(){
		playerInput = GameObject.Find("PlayerInput");
		animator = GetComponentInParent<Animator>();
	}

	void Update(){
		if(playerInput.GetComponent<InputBuffer>().direction == GameButton.None){
			animator.Play("Stand");
		}
		else if(ControlMapper.GetButton(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.Right)){
			animator.Play("MoveForward");
		}
		else if(ControlMapper.GetButton(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.Left)){
			animator.Play("MoveBackward");
		}
		
		ButtonUpdate();

	}

	void ButtonUpdate(){
		// if(playerInput.GetComponent<InputBuffer>().button == GameButton.LightAttack){
		// 	animator.Play("stA");
		// }
		if(ControlMapper.GetButton(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.LightAttack)){
			animator.Play("stA");
		}
	}
}
