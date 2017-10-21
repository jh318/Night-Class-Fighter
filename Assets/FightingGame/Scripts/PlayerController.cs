using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int playerNumber;
	public int jumpCountMax = 1;
	[HideInInspector]
	public int jumpCount = 1;
	public float jumpVelocityY = 2.0f;
	public float jumpVelocityX = 2.0f;
	
	
	bool rightSide = false;
	[HideInInspector]
	public bool isGround = true;
	[HideInInspector]
	public bool isBlocking = false;
	bool canJump = true;
	bool isAttacking = false;
	int attackStrength;
	
	GameObject playerInput;
	InputBuffer inputBuffer;
	Animator animator;
	GameObject opponent;
	Vector3 flipLeftRotate = new Vector3(0.0f, 140.0f, 0.0f);
	Vector3 flipRightRotate = new Vector3(0.0f,250.0f,0.0f);
	Vector3 flipLeftScale = new Vector3(1,1,1);
	Vector3 flipRightScale = new Vector3(-1,1,1);
	HitBoxController hitBoxController;
	Rigidbody body;

	public delegate void Knockout(int player);
	public static event Knockout knockout = delegate{};

	void OnDestroy(){
		FightManager.nextRound -= OnNextRound;
	}

	void Start(){
		FightManager.nextRound += OnNextRound;
		isGround = true;
		playerInput = GameObject.Find("PlayerInput");
		animator = GetComponentInParent<Animator>();
		body = GetComponent<Rigidbody>();
		jumpCount = jumpCountMax;
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
		CheckSide();
		BlockCheck();

		DirectionUpdate();
		ButtonUpdate();				
		FlipSide();	
		if(transform.position.y > 1.04f){
			gameObject.GetComponent<CapsuleCollider>().enabled = false;
		}
		else{
			gameObject.GetComponent<CapsuleCollider>().enabled = true;
		}	
	}

	void ButtonUpdate(){
		if (inputBuffer.inputBuffer.Count == 0) return;

		if(inputBuffer.inputBuffer[inputBuffer.inputBuffer.Count-1] == GameButton.LightAttack && !isAttacking){
			attackStrength = 1;
			animator.SetInteger("attackStrength", attackStrength);
			animator.SetTrigger("attack");
			//StartCoroutine("Attacking");
		}
		else if(inputBuffer.inputBuffer[inputBuffer.inputBuffer.Count-1] == GameButton.MediumAttack && !isAttacking){
			attackStrength = 2;
			animator.SetInteger("attackStrength", attackStrength);
			animator.SetTrigger("attack");
			//StartCoroutine("Attacking");		
		}
		else if(inputBuffer.inputBuffer[inputBuffer.inputBuffer.Count-1] == GameButton.HeavyAttack && !isAttacking){
			attackStrength = 3;
			animator.SetInteger("attackStrength", attackStrength);
			animator.SetTrigger("attack");
			//StartCoroutine("Attacking");
		}
	}

	void DirectionUpdate(){
		float x = inputBuffer.dirAxis.x;
		float y = inputBuffer.dirAxis.y;

		if(rightSide) x *= -1;
		animator.SetFloat("xInput", x);
		animator.SetFloat("yInput", y);

		if(jumpCount > 0 && canJump) JumpCheck(x,y);

		
	}

	void FlipSide(){	
			if(!rightSide  && opponent.GetComponent<PlayerController>().isGround && isGround){
				transform.localEulerAngles = flipLeftRotate;
				transform.localScale = flipLeftScale;
				//if(Mathf.Abs(transform.position.x - opponent.transform.position.x) <= 1.0f)
					//transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
			}
			else if(rightSide && opponent.GetComponent<PlayerController>().isGround && isGround){
				rightSide = true;
				transform.localEulerAngles = flipRightRotate;
				transform.localScale = flipRightScale;
				//if(Mathf.Abs(transform.position.x - opponent.transform.position.x) <= 1.0f)
					//transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
			}
	}

	void CheckSide(){
		if(Mathf.Abs((transform.position - opponent.transform.position).x) < 0.1f && Mathf.Abs((transform.position - opponent.transform.position).y) < 0.1f){
			Debug.Log("Resting");
			if(rightSide) transform.position += new Vector3(1,0,0);
			else if(!rightSide) transform.position -= new Vector3(1,0,0);
		}	
		else if(transform.position.x < opponent.transform.position.x - 0.0f)
			rightSide = false;
		else if(transform.position.x > opponent.transform.position.x + 0.0f)
			rightSide = true;
		
	}

	void BlockCheck(){
		if(!rightSide && inputBuffer.dirAxis.x < -0.5 && !isAttacking && isGround){
			isBlocking = true;
		}
		else if(rightSide && inputBuffer.dirAxis.x > 0.5 && !isAttacking && isGround){
			isBlocking = true;
		}
		else{
			isBlocking = false;
		}
	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject == opponent && isAttacking && !opponent.GetComponent<PlayerController>().isBlocking){
			Debug.Log("Hit");
			GameObject partTemp = Spawner.Spawn("HitEffect");
			partTemp.transform.position = opponent.transform.position;
			if(attackStrength >= 3) StartCoroutine("HitStop", 0.1f);
			opponent.GetComponent<HealthController>().healthPointCurr -= 2;
			opponent.GetComponent<HealthController>().healthBarUI.size = (float)opponent.GetComponent<HealthController>().healthPointCurr/(float)opponent.GetComponent<HealthController>().healthPointMax;
			PlayerController otherPlayer = opponent.GetComponent<PlayerController>();
			otherPlayer.CheckHealth();
			float knockbackPower = (float)attackStrength / 3;
			otherPlayer.KnockBack(new Vector3(knockbackPower, 0, 0));
			opponent.GetComponent<Animator>().Play("HitStun");
			attackStrength = 0;
		}
		else if(c.gameObject == opponent && isAttacking && opponent.GetComponent<PlayerController>().isBlocking){
			PlayerController otherPlayer = opponent.GetComponent<PlayerController>();			
			otherPlayer.KnockBack(new Vector3(1, 0, 0));
		}
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag == "Ground"){
			jumpCount = jumpCountMax;
			animator.applyRootMotion = true;
			animator.Play("JumpLand");
			canJump = true;
			isGround = true;
		}
	}

	void OnCollisionExit(Collision c){
	
	}

	void KnockBack(Vector3 force){
		if(rightSide) force *= 1;
		else if(!rightSide) force *= -1;
		StartCoroutine("KnockBackCoroutine", force);
	}

	IEnumerator KnockBackCoroutine(Vector3 force){
		yield return null;
		for(float t = 0; t < 1; t+=Time.deltaTime){
			transform.position = transform.position + force * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	float JumpVelocity(float height){
		return Mathf.Sqrt(2*height*Mathf.Abs(Physics.gravity.y));
	}

	void JumpCheck(float x, float y){
		if(!rightSide && y > 0.5){
			animator.applyRootMotion = false;
			jumpCount--;
			canJump = false;
			isGround = false;

			if(x > 0.5 && y > 0.5){
				body.velocity = new Vector3(jumpVelocityX, JumpVelocity(jumpVelocityY), body.velocity.z);
			}
			else if(x < -0.5 && y > 0.5){
				body.velocity = new Vector3(-jumpVelocityX, JumpVelocity(jumpVelocityY), body.velocity.z);
			}
			else if(y > 0.5){
				body.velocity = new Vector3(body.velocity.x, JumpVelocity(2), body.velocity.z);
			}
		}
		else if(rightSide && y > 0.5){
			animator.applyRootMotion = false;
			canJump = false;
			isGround = false;

			if(x > 0.5 && y > 0.5){
				body.velocity = new Vector3(-jumpVelocityX, JumpVelocity(jumpVelocityY), body.velocity.z);
			}
			else if(x < -0.5 && y > 0.5){
				body.velocity = new Vector3(jumpVelocityX, JumpVelocity(jumpVelocityY), body.velocity.z);
			}
			else if(y > 0.5){
				body.velocity = new Vector3(body.velocity.x, JumpVelocity(2), body.velocity.z);
			}
		}
	}

	void CheckHealth(){
		if(GetComponent<HealthController>().healthPointCurr <= 0){
			gameObject.SetActive(false);
			knockout(playerNumber);
		}
	}

	void OnNextRound(int player){
		gameObject.SetActive(true);
		if(playerNumber == 0) transform.position = FightManager.instance.player1StartPosition;
		else if(playerNumber == 1) transform.position = FightManager.instance.player2StartPosition;
		GetComponent<HealthController>().healthPointCurr = GetComponent<HealthController>().healthPointMax;
		GetComponent<HealthController>().healthBarUI.size = 1;
	}

	IEnumerator HitStop(float time){
		Time.timeScale = 0.1f;
		yield return new WaitForSeconds(time);
		Time.timeScale = 1.0f;
	}

	bool AnimatorIsPlaying(){
		return animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}

	IEnumerator Attacking(){
		isAttacking = true;
		yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
		isAttacking = false;
	}

	public void IsAttackingTrue(){
		isAttacking = true;
	}

	public void IsAttackingFalse(){
		isAttacking = false;
	}

}
