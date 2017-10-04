using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour {

	void OnEnable(){
		PlayerController.knockout += OnKnockout;
	}

	void Start(){
		AudioManager.PlayMusic("MainTheme");
	}

	void OnKnockout(int player){
		if(player == 0){
			Debug.Log("Player 1 Lose");
		}
		else if(player == 1){
			Debug.Log("Player 2 Lose");
		}

		SceneDirector.instance.Title();
	}
}
