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
			Debug.Log("Disable rounds");
		}

		FindPlayer(player1, 0);
		FindPlayer(player2, 1);
		//player1.GetComponent<HealthController>().healthPointCurr = 3;
	}

	void Update(){
		if(TimerScript.instance.time < 0.0f && !roundOver){
			Debug.Log("Out of time");
			StartCoroutine(EndOfRound(2.0f, -1));
		}
		if(matchOver){
			Debug.Log("STOP TIME");
			TimerScript.instance.StopCoroutine("Timer");
		}
	}
	
	void OnKnockout(int player){
		if(!matchOver){
			if(player == 0 && player < roundsToWin){
				roundsPlayer2++;
				roundPlayer2Images[roundsPlayer2--].enabled = true;
				if(roundsPlayer2 < roundsToWin && !roundOver) { StartCoroutine(EndOfRound(2.0f, 0)); }
			}
			else if(player == 1 && player < roundsToWin){
				roundsPlayer1++;
				roundPlayer1Images[roundsPlayer1-1].enabled = true;
				if(roundsPlayer1 < roundsToWin && !roundOver) { StartCoroutine(EndOfRound(2.0f, 1)); }			
			}			
		}
				
		if(roundsPlayer1 == roundsToWin){
			Debug.Log("Match over");
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

	IEnumerator EndOfRound(float time, int player){
		Debug.Log("Resetting round");
		roundOver = true;
		while(time > 0.0f){
			time -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		if(roundOver) nextRound(player);
		roundOver = false;
		TimerScript.instance.StopCoroutine("Timer");
		TimerScript.instance.StartCoroutine("Timer");
	}

	void TimeOverWinConditions(){

		//if player 1 health > player 2
			//player 1 wins rounds
		//else if player 2 health > player 1 health
			//player 2 wins
		//else
			//niether player gets a points
	}

	void FindPlayer(GameObject player, int playerNumber){
		GameObject[] tempPlayers;
		tempPlayers = GameObject.FindGameObjectsWithTag("Player");
		
		for(int i = 0; i < tempPlayers.Length; i++){
			if(tempPlayers[i].GetComponent<PlayerController>()){
				if(tempPlayers[i].GetComponent<PlayerController>().playerNumber == playerNumber){
					player = tempPlayers[i];
					Debug.Log("found player");
				}
			}
		}
	}



}
