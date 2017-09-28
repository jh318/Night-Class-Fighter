using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	GameObject playerInput;
	InputBuffer inputBuffer;
	Animator animator;

	void Start(){
		playerInput = GameObject.Find("PlayerInput");
		animator = GetComponentInParent<Animator>();
		inputBuffer = playerInput.GetComponent<InputBuffer>();
	}

	void Update(){
		DirectionUpdateBuffer();
		ButtonUpdateBuffer();

	}

	void ButtonUpdate(){
		// if(playerInput.GetComponent<InputBuffer>().button == GameButton.LightAttack){
		// 	animator.Play("stA");
		// }
		if(ControlMapper.GetButtonDown(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.LightAttack)){
			//PlayAttackAnim("stA");
			// Debug.Log("Punch");
			animator.Play("stA");
		}
		else if(ControlMapper.GetButtonDown(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.MediumAttack)){
			// Debug.Log("MPunch");
			animator.Play("stB");
		}
		else if(ControlMapper.GetButtonDown(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.HeavyAttack)){
			animator.Play("stC");
		}
	}

	void ButtonUpdateBuffer(){
		//Debug.Log(inputBuffer.inputBuffer[inputBuffer.inputBuffer.Count-1]);
		if(inputBuffer.inputBuffer[inputBuffer.inputBuffer.Count-1] == GameButton.LightAttack){
			animator.Play("stA");
			inputBuffer.inputBuffer.Clear();	
		}
		else if(inputBuffer.inputBuffer[inputBuffer.inputBuffer.Count-1] == GameButton.MediumAttack){
			animator.Play("stB");
			inputBuffer.inputBuffer.Clear();	
			
		}
		else if(inputBuffer.inputBuffer[inputBuffer.inputBuffer.Count-1] == GameButton.HeavyAttack){
			animator.Play("stC");
			inputBuffer.inputBuffer.Clear();	
		}
	}

	void DirectionUpdate(){
		float x = 0;

		if(ControlMapper.GetButton(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.Right)){
			x = 1;
		}
		else if(ControlMapper.GetButton(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.Left)){
			x = -1;
		}
		else if(ControlMapper.GetButton(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.Down)){
			
			animator.Play("Crouch");
		}
		else if(ControlMapper.GetButton(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.Up)){
			animator.Play("NeutralJumpStart");
		}

		animator.SetFloat("xInput", x);
	}

	void DirectionUpdateBuffer(){
		float x = 0;

		if(inputBuffer.inputBuffer[inputBuffer.inputBuffer.Count-1] == GameButton.Right){
			x = 1;
		}
		else if(inputBuffer.inputBuffer[inputBuffer.inputBuffer.Count-1] == GameButton.Left){
			x = -1;
		}
		else if(inputBuffer.inputBuffer[inputBuffer.inputBuffer.Count-1] == GameButton.Down){
			animator.Play("Crouch");
		}
		else if(inputBuffer.inputBuffer[inputBuffer.inputBuffer.Count-1] == GameButton.Up){
			animator.Play("NeutralJumpStart");
		}
		Debug.Log(inputBuffer.inputBuffer[inputBuffer.inputBuffer.Count-1]);

		animator.SetFloat("xInput", x);
	}
	
}
