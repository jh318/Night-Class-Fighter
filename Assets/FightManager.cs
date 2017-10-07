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
	public GameObject player1;
	public GameObject player2;
	public int endOfRoundTimer = 4;

	int roundsPlayer1 = 0;
	int roundsPlayer2 = 0;
	bool winPlayer1 = false;
	bool winPlayer2 = false;
	bool roundOver = false;
	bool matchOver = false;

	public delegate void NextRound(int player);
	public static event NextRound nextRound = delegate{};

	void OnEnable(){
		if(instance == null){ instance = this;}
		PlayerController.knockout += OnKnockout;
	}

	void Start(){
		AudioManager.PlayMusic("MainTheme");
		roundsPlayer1 = 0;
		roundsPlayer2 = 0;

		for(int i = 0; i < roundPlayer1Images.Length; i++){
			roundPlayer1Images[i].enabled = false;
			roundPlayer2Images[i].enabled = false;
		}

		FindPlayer(out player1, 0);
		FindPlayer(out player2, 1);		
	}

	void Update(){
		
		if(TimerScript.instance.time < 0.0f && !roundOver){
			TimeOverWinConditions();			
			StartCoroutine(EndOfRoundCoroutine(endOfRoundTimer, -1));
		}
		if(matchOver){
			TimerScript.instance.StopCoroutine("Timer");
		}
	}
	
	void OnKnockout(int player){
		if(player1.GetComponent<HealthController>().healthPointCurr <= 0 && player2.GetComponent<HealthController>().healthPointCurr <= 0){
			if(!roundOver) StartCoroutine(EndOfRoundCoroutine(endOfRoundTimer, player));
		}
		else if(!matchOver){
			if(player == 0 && roundsPlayer2 < roundsToWin){
				roundsPlayer2++;
				roundPlayer2Images[roundsPlayer2-1].enabled = true;
				if(roundsPlayer2 < roundsToWin && !roundOver) { StartCoroutine(EndOfRoundCoroutine(endOfRoundTimer, 0)); }
			}
			else if(player == 1 && roundsPlayer1 < roundsToWin){
				roundsPlayer1++;
				roundPlayer1Images[roundsPlayer1-1].enabled = true;
				if(roundsPlayer1 < roundsToWin && !roundOver) { StartCoroutine(EndOfRoundCoroutine(endOfRoundTimer, 1)); }			
			}			
		}
				
		if(roundsPlayer1 == roundsToWin){
			matchOver = true;
			StartCoroutine("ResetGameCoroutine", 3.0f);
		}
		else if(roundsPlayer2 == roundsToWin){
			matchOver = true;
			StartCoroutine("ResetGameCoroutine", 3.0f);
		}
		else{
			//if(!roundOver) StartCoroutine(EndOfRound(2.0f, player));
		}
	}

	IEnumerator ResetGameCoroutine(float time){
		TimerScript.instance.StopCoroutine("Timer");		
		while(time >= 0.0f){
			time -= Time.deltaTime;
			Debug.Log("Resetting");
			yield return new WaitForEndOfFrame();
		}
		SceneDirector.instance.Title();
	}

	IEnumerator EndOfRoundCoroutine(float time, int player){
		Debug.Log("Resetting round");
		TimerScript.instance.StopCoroutine("Timer");
		roundOver = true;
		while(time > 0.0f){
			time -= Time.deltaTime;
			if(matchOver) yield break;
			yield return new WaitForEndOfFrame();
		}
		if(roundOver && !matchOver){
		nextRound(player);
		TimerScript.instance.StartCoroutine("Timer");
		}
		roundOver = false;
	}

	void TimeOverWinConditions(){
		if(player1.GetComponent<HealthController>().healthPointCurr > player2.GetComponent<HealthController>().healthPointCurr){
			OnKnockout(1);
		}
		else if(player2.GetComponent<HealthController>().healthPointCurr > player1.GetComponent<HealthController>().healthPointCurr){
			OnKnockout(0);
		}
		else{
			//No rounds rewarded
		}
	}

	void FindPlayer(out GameObject player, int playerNumber){
		player = null;
		GameObject[] tempPlayers;
		tempPlayers = GameObject.FindGameObjectsWithTag("Player");
		
		for(int i = 0; i < tempPlayers.Length; i++){
			Debug.Log("Loop");
			if(tempPlayers[i].GetComponent<PlayerController>()){
				Debug.Log("Finding Players...");
				if(tempPlayers[i].GetComponent<PlayerController>().playerNumber == playerNumber){
					player = tempPlayers[i];
					Debug.Log("found player");
				}
			}
		}
	}
}
