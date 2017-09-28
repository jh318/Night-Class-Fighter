using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	GameObject playerInput;
	InputBuffer inputBuffer;
	Animator animator;
	public int playerNumber;

	void Start(){
		playerInput = GameObject.Find("PlayerInput");
		animator = GetComponentInParent<Animator>();
		//Find player's input buffer
		InputBuffer[] buff = FindObjectsOfType(typeof(InputBuffer)) as InputBuffer[];
		for(int i = 0; i < buff.Length; i++){
			if(buff[i].playerNumber == playerNumber){
				inputBuffer = buff[i];
			}
		}
		
	}

	void Update(){
		DirectionUpdate();
		ButtonUpdate();

	}

	void ButtonUpdate(){
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
