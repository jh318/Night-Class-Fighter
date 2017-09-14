using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBuffer : MonoBehaviour {

	public int playerNumber = 0;
	[HideInInspector]
	public List<GameButton> inputBuffer;
	[HideInInspector]
	public Text inputBufferDisplay;

	private ControlMapper controlMapper;
	private GameButton direction;
	private GameButton button;

	void Start(){
		controlMapper = GetComponent<ControlMapper>();
	}

	void Update(){
		GetDirectionInput();
		GetButtonInput();
		ParseDirection();
		ParseButton();
	}

	void GetButtonInput(){
		button = GameButton.None;
		if(ControlMapper.GetButton(playerNumber, GameButton.LightAttack))
			button = GameButton.LightAttack;
		if(ControlMapper.GetButton(playerNumber, GameButton.MediumAttack))
			button = GameButton.MediumAttack;
		if(ControlMapper.GetButton(playerNumber, GameButton.HeavyAttack))
			button = GameButton.HeavyAttack;
	}

	void ParseButton(){
		inputBuffer.Add(button);
	}

	void GetDirectionInput(){
		//Current Input
		if(ControlMapper.GetButtonDown(playerNumber, GameButton.Up)){
			direction = GameButton.Up;
			Debug.Log("U");
		}
		else if(ControlMapper.GetButtonDown(playerNumber, GameButton.Down)){
			direction = GameButton.Down;
			Debug.Log("D");
		}
		else if(ControlMapper.GetButtonDown(playerNumber, GameButton.Left)){
			direction = GameButton.Left;
			Debug.Log("B");
		}
		else if(ControlMapper.GetButtonDown(playerNumber, GameButton.Right)){
			direction = GameButton.Right;
			Debug.Log("F");
		}
	}

	void ParseDirection(){
		inputBuffer.Add(direction);
	}
}