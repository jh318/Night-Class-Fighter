﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	GameObject playerInput;
	//GameObject inputBuffer;
	Animator animator;

	void Start(){
		playerInput = GameObject.Find("PlayerInput");
		animator = GetComponentInParent<Animator>();
		animator.Play("Stand");
	}

	void Update(){
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
		ButtonUpdate();

	}

	void ButtonUpdate(){
		// if(playerInput.GetComponent<InputBuffer>().button == GameButton.LightAttack){
		// 	animator.Play("stA");
		// }
		if(ControlMapper.GetButtonDown(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.LightAttack)){
			//PlayAttackAnim("stA");
			Debug.Log("Punch");
			animator.Play("stA");
		}
		else if(ControlMapper.GetButtonDown(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.MediumAttack)){
			Debug.Log("MPunch");
			animator.Play("stB");
		}
		else if(ControlMapper.GetButtonDown(playerInput.GetComponent<InputBuffer>().playerNumber, GameButton.HeavyAttack)){
			animator.Play("stC");
		}
	}

	void PlayAttackAnim(string attackAnim){
		float timer = 2.0f;
		while(timer > 0){
			animator.Play(attackAnim);
			timer -= Time.deltaTime;
		}
	}
}
