using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	
	GameObject playerInput;
	InputBuffer inputBuffer;
	Animator animator;
	public int playerNumber;
	GameObject opponent;


	Vector3 flipLeftRotate = new Vector3(0.0f, 140.0f, 0.0f);
	Vector3 flipRightRotate = new Vector3(0.0f,250.0f,0.0f);
	Vector3 flipLeftScale = new Vector3(1,1,1);
	Vector3 flipRightScale = new Vector3(-1,1,1);
	bool rightSide = false;


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
		//Find opponent
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for(int i = 0; i < players.Length; i++){
			if(players[i].GetComponent<PlayerController>()){
				if(players[i].GetComponent<PlayerController>().playerNumber != playerNumber){
					opponent = players[i];
				}
			}
		}	
	}

	void Update(){
		transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
		DirectionUpdate();
		ButtonUpdate();
		FlipSide();

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

		if(rightSide) x *= -1;
		animator.SetFloat("xInput", x);
	}

	void FlipSide(){
		//if(Mathf.Abs(transform.position.x - opponent.transform.position.x) <= 1.0f){
			if(transform.position.x < opponent.transform.position.x - 0.2f){
				rightSide = false;
				transform.localEulerAngles = flipLeftRotate;
				transform.localScale = flipLeftScale;
				//transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);

			}
			else if(transform.position.x > opponent.transform.position.x + 0.2f){
				rightSide = true;
				transform.localEulerAngles = flipRightRotate;
				transform.localScale = flipRightScale;
				//transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);

			}
		//}
	}
}
