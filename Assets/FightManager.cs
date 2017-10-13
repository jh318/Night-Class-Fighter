using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightManager : MonoBehaviour {

	public Image[] roundPlayer1Images;
	public Image[] roundPlayer2Images;

	int roundsPlayer1 = 0;
	int roundsPlayer2 = 0;
	bool winPlayer1 = false;
	bool winPlayer2 = false;


	void OnEnable(){
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
	}

	void OnKnockout(int player){
		if(player == 0){
			Debug.Log("Player 2 Win");
			Debug.Log("Player 1 Lose");
			roundsPlayer1++;
		}
		else if(player == 1){
			Debug.Log("Player 1 Win");
			Debug.Log("Player 2 Lose");
			roundsPlayer2++;
		}

		//SceneDirector.instance.Title();
	}
}
