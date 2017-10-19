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
	[HideInInspector]
	public GameButton direction;
	[HideInInspector]
	public GameButton button;
	[HideInInspector]
	public bool rightSide = false;

	private ControlMapper controlMapper;
	private GameButton lastDirection;
	private GameButton lastButton;
	

	void Start(){
		controlMapper = GetComponent<ControlMapper>();
	}

	void Update(){
		GetDirectionInput();
		GetButtonInput();
		ParseDirection();
		ParseButton();

		if (playerNumber == 0) {
			string bufferString = "";
			for (int i = Mathf.Max(0, inputBuffer.Count - 4); i < inputBuffer.Count; i++) {
				GameButton b = inputBuffer[i];
				bufferString += b.ToString().Replace("Button.", "") + " > ";
			}
			Debug.Log(bufferString);
		}
	}

	void GetButtonInput(){		
		button = GameButton.None;

		if(ControlMapper.GetButtonDown(playerNumber, GameButton.LightAttack))
			button = GameButton.LightAttack;
		if(ControlMapper.GetButtonDown(playerNumber, GameButton.MediumAttack))
			button = GameButton.MediumAttack;
		if(ControlMapper.GetButtonDown(playerNumber, GameButton.HeavyAttack))
			button = GameButton.HeavyAttack;
	}

	void ParseButton(){
		if(button != GameButton.None)
			inputBuffer.Add(button);
	}

	private Vector2 axis;
	public Vector2 dirAxis {
		get { return axis; }
	}

	void GetDirectionInput(){
		lastDirection = direction;

		axis = Vector2.zero;
		if(ControlMapper.GetButton(playerNumber, GameButton.Right)) axis.x = 1;
		else if (ControlMapper.GetButton(playerNumber, GameButton.Left))  axis.x = -1;
		
		//if(rightSide) axis.x = axis.x * -1;
			
		if(ControlMapper.GetButton(playerNumber, GameButton.Up)) axis.y = 1;
		else if (ControlMapper.GetButton(playerNumber, GameButton.Down)) axis.y = -1;

		if (axis.sqrMagnitude > ControlMapper.instance.threshold) {
			if (Vector2.Angle (Vector2.up, axis) < 22.5f) {
				direction = GameButton.Up;
			}
			else if (Vector2.Angle (Vector2.down, axis) < 22.5f) {
				direction = GameButton.Down;
			}
			else if (Vector2.Angle (Vector2.left, axis) < 22.5f) {
				direction = GameButton.Left;
			}
			else if (Vector2.Angle (Vector2.right, axis) < 22.5f) {
				direction = GameButton.Right;
			}
			else if (Vector2.Angle (Vector2.one, axis) < 22.5f) {
				direction = GameButton.UpR;
			}
			else if (Vector2.Angle (new Vector2(1,-1), axis) < 22.5f) {
				direction = GameButton.DownR;
			}
			else if (Vector2.Angle (-Vector2.one, axis) < 22.5f) {
				direction = GameButton.DownL;
			}
			else if (Vector2.Angle (new Vector2(-1,1), axis) < 22.5f) {
				direction = GameButton.UpL;
			}
		}
		else {
			direction = GameButton.None;
		}
	}

	void ParseDirection(){
		if (direction != lastDirection) {
			inputBuffer.Add(direction);
		}
		else {
			inputBuffer.Add(GameButton.None);
		}
	}
}