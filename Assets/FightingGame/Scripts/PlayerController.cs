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
	HitBoxController hitBoxController;

	public delegate void Knockout(int player);
	public static event Knockout knockout = delegate{};

	void OnEnable(){
		FightManager.nextRound += OnNextRound;
	}


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

		hitBoxController = GetComponent<HitBoxController>();

		if(playerNumber == 0) transform.position = FightManager.instance.player1StartPosition;
		else if(playerNumber == 1) transform.position = FightManager.instance.player2StartPosition;
	}

	void Update(){
		transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
		DirectionUpdate();
		ButtonUpdate();
		FlipSide();
		if(hitBoxController.hitBoxHandRight.GetComponent<BoxCollider>()){

		}
	}

	void ButtonUpdate(){
		if (inputBuffer.inputBuffer.Count == 0) return;

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
		if (inputBuffer.inputBuffer.Count == 0) return;

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

	void OnTriggerEnter(Collider c){
		if(c.gameObject == opponent){
			Debug.Log("Hit");
			opponent.GetComponent<HealthController>().healthPointCurr -= 2;
			opponent.GetComponent<HealthController>().healthBarUI.size = (float)opponent.GetComponent<HealthController>().healthPointCurr/(float)opponent.GetComponent<HealthController>().healthPointMax;
			opponent.GetComponent<PlayerController>().CheckHealth();
		}
	}

	void CheckHealth(){
		if(GetComponent<HealthController>().healthPointCurr <= 0){
			Debug.Log("KO");
			gameObject.SetActive(false);
			knockout(playerNumber);

		}
	}

	void OnNextRound(int player){
		if(player == playerNumber){
			gameObject.SetActive(true);
		}
		if(playerNumber == 0) transform.position = FightManager.instance.player1StartPosition;
		else if(playerNumber == 1) transform.position = FightManager.instance.player2StartPosition;
		GetComponent<HealthController>().healthPointCurr = GetComponent<HealthController>().healthPointMax;
		GetComponent<HealthController>().healthBarUI.size = 1;
	}
}
