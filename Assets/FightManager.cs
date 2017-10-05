using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightManager : MonoBehaviour {

	public static FightManager instance;

	public Image[] roundPlayer1Images;
	public Image[] roundPlayer2Images;
	public int roundsToWin = 2;
	public Vector3 player1StartPosition = new Vector3(-1.5f,1.0f,0.0f);
	public Vector3 player2StartPosition = new Vector3(1.5f,1.0f,0.0f);

	int roundsPlayer1 = 0;
	int roundsPlayer2 = 0;
	bool winPlayer1 = false;
	bool winPlayer2 = false;
	TimerScript timeComponent;
	float roundTime;

	public delegate void NextRound(int player);
	public static event NextRound nextRound = delegate{};


	void OnEnable(){
		if(instance == null){
			instance = this;
		}
		PlayerController.knockout += OnKnockout;
	}

	void Start(){
		AudioManager.PlayMusic("MainTheme");
		roundsPlayer1 = 0;
		roundsPlayer2 = 0;
		timeComponent = GetComponent<TimerScript>();
		roundTime = GetComponent<TimerScript>().time;

		for(int i = 0; i < roundPlayer1Images.Length; i++){
			roundPlayer1Images[i].enabled = false;
			roundPlayer2Images[i].enabled = false;
			Debug.Log("Disable rounds");
		}
	}

	void Update(){
		if(timeComponent.time < 0.0f){
			Debug.Log("Out of time");
			nextRound(-1);
			timeComponent.time = (int)roundTime;
		}
	}
	

	void OnKnockout(int player){
		if(player == 0 && player < roundsToWin){
			roundsPlayer2++;
			roundPlayer2Images[roundsPlayer2--].enabled = true;
			timeComponent.time = (int)roundTime;
			nextRound(0);
		}
		else if(player == 1 && player < roundsToWin){
			roundsPlayer1++;
			roundPlayer1Images[roundsPlayer1-1].enabled = true;
			timeComponent.time = (int)roundTime;
			nextRound(1);
		}
		
		if(roundsPlayer1 == roundsToWin){
			//ResetGame(3.0f);
			StartCoroutine("ResetGameCoroutine", 3.0f);
		}
		else if(roundsPlayer2 == roundsToWin){
			//ResetGame(3.0f);
			StartCoroutine("ResetGameCoroutine", 3.0f);
		}
		else{
			nextRound(player);
		}
	}

	void ResetGame(float time){
		while(time > 0.0f){
			Debug.Log("GAME OVER");
			time -= Time.deltaTime;
		}
		SceneDirector.instance.Title();
	}

	IEnumerator ResetGameCoroutine(float time){
		while(time >= 0.0f){
			time -= Time.deltaTime;
			Debug.Log("Resetting");
			yield return new WaitForEndOfFrame();
		}
		SceneDirector.instance.Title();
	}
}
