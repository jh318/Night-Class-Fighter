using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBuffer : MonoBehaviour {

	[HideInInspector]
	public List<string> inputBuffer;
	[HideInInspector]
	public Text inputBufferDisplay;

	private enum Direction {DB, D, DF, B, N, F, UB, U, UF};
	private enum Button {A, B, C, Button1, Button2, Button3, Button4, None};
	private Vector2 lastAxisInput;

	Direction direction;
	Button button;

	void GetDirectionInput(){
		Direction lastDir = Direction.N;
		if(lastAxisInput.sqrMagnitude > 0.25f){
			if(Vector2.Angle(Vector2.up, lastAxisInput) < 22.5f){
				lastDir = Direction.U;
			}
			else if(Vector2.Angle(Vector2.down, lastAxisInput) < 22.5f){
				lastDir = Direction.D;
			}
			else if(Vector2.Angle(Vector2.left, lastAxisInput) < 22.5f){
				lastDir = Direction.B; 
			}
			else if(Vector2.Angle(Vector2.right, lastAxisInput) < 22.5f){
				lastDir = Direction.F;
			}
			else if(Vector2.Angle(Vector2.one, lastAxisInput) < 22.5f){
				lastDir = Direction.UF;
			}
			else if(Vector2.Angle(new Vector2(1, -1), lastAxisInput) < 22.5f){
				lastDir = Direction.DF;
			}
			else if(Vector2.Angle(-Vector2.one, lastAxisInput) < 22.5f){
				lastDir = Direction.DB;
			}
			else if(Vector2.Angle(new Vector2(-1,1), lastAxisInput) < 22.5f){
				lastDir = Direction.UB;
			}
		}

	// 	Direction dir = Direction.N;
	// 	//Vector2 axisInput = new Vector2 (playerInput.Horizontal, playerInput.Vertical);
	// 	if(axisInput.sqrMagnitude > 0.25f){
	// 		if(Vector2.Angle(Vector2.up, axisInput) < 22.5f){
	// 			lastDir = Direction.U;
	// 		}
	// 		else if(Vector2.Angle(Vector2.down, axisInput) < 22.5f){
	// 			dir = Direction.D;
	// 		}
	// 		else if(Vector2.Angle(Vector2.left, axisInput) < 22.5f){
	// 			dir = Direction.B; 
	// 		}
	// 		else if(Vector2.Angle(Vector2.right, axisInput) < 22.5f){
	// 			dir = Direction.F;
	// 		}
	// 		else if(Vector2.Angle(Vector2.one, axisInput) < 22.5f){
	// 			dir = Direction.UF;
	// 		}
	// 		else if(Vector2.Angle(new Vector2(1, -1), axisInput) < 22.5f){
	// 			dir = Direction.DF;
	// 		}
	// 		else if(Vector2.Angle(-Vector2.one, aAxisInput) < 22.5f){
	// 			dir = Direction.DB;
	// 		}
	// 		else if(Vector2.Angle(new Vector2(-1,1), axisInput) < 22.5f){
	// 			dir = Direction.UB;
	// 	}
	// }

	// if(dir != Direction.N && dir != lastDir){
	// 	direction = dir;
	// } 
	// else{
	// 	direction = Direction.N;
	// }

	// lastAxisInput = new Vector2 (playerInput.Horizontal, playerInput.Vertical);
	}


	void GetButtonInput(){
		// button = Button.None;
		// if(playerInput.A)
		// 	button = Button.A;
		// if(playerInput.B)
		// 	button = Button.B;
		// if(playeRInput.C)
		// 	button = Button.C;
	}

	void ButtonParse(){
		switch(button){
			case Button.A:
				inputBuffer.Add("A");
				break;
			case Button.B:
				inputBuffer.Add("B");
				break;
			case Button.C:
				inputBuffer.Add("C");
				break;
			case Button.None:
				//do nothing
				break;
			default:
				// do nothing
				break;
		}
	}


}