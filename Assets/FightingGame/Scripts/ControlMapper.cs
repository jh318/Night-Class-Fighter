using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Control
{
    public GameButton name;
    public KeyCode keycode;
    public string axis;
    public bool isAxis;
    public bool isPositive;

    [System.NonSerialized] public float currentValue;
    [System.NonSerialized] public float previousValue;
}

public enum GameButton
{
    HeavyAttack,
    MediumAttack,
    LightAttack,
    Up,
    Down, 
    Left,
    Right,
    None    
}

public class ControlMapper : MonoBehaviour {

    [Range(0,1)] public float threshold = 0.5f;

    public string player1Xaxis, player1Yaxis, player2Xaxis, player2Yaxis;
    public Control[] player1ControlsArray;
    public Control[] player2ControlsArray;
    public static ControlMapper instance;

    private Dictionary<GameButton, Control> player1Mapping = new Dictionary<GameButton, Control>();
    private Dictionary<GameButton, Control> player2Mapping = new Dictionary<GameButton, Control>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            foreach (Control c in player1ControlsArray)
            {
                player1Mapping[c.name] = c;
            }

            foreach (Control c in player2ControlsArray)
            {
                player2Mapping[c.name] = c;
            }
        }
    }

    public static void ControlChanger(Control[] controlsarray, GameButton controlName, KeyCode desiredKeyCode)
    {
        int arraySpot = -1;

        

        for(int i = 0; i < controlsarray.Length; ++i)
        {
            if(controlsarray[i].name == controlName)
            {
                arraySpot = i;
                break;
            }
        }

        if (arraySpot == -1)
        {
            Debug.Log("Couldn't find a control named " + controlName);
            return;
        }

        controlsarray[arraySpot].keycode = desiredKeyCode;


    }

    public static bool GetButton(int player, GameButton button)
    {
        Control control = (player == 0) ? instance.player1Mapping[button] : instance.player2Mapping[button];
        if (control.isAxis)
        {
            if (control.isPositive && control.currentValue > instance.threshold)
            {
                return true;
            }
            else if (!control.isPositive && control.currentValue < -instance.threshold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return Input.GetKey(control.keycode);
        }
    }

    public static bool GetButtonDown(int player, GameButton button)
    {
        Control control = (player == 0) ? instance.player1Mapping[button] : instance.player2Mapping[button];
        if (control.isAxis)
        {
            if (control.isPositive && control.currentValue > instance.threshold && control.previousValue <= instance.threshold)
            {
                return true;
            }
            else if (!control.isPositive && control.currentValue < -instance.threshold && control.previousValue >= -instance.threshold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return Input.GetKeyDown(control.keycode);
        }
    }

    public static bool GetButtonUp(int player, GameButton button)
    {
        Control control = (player == 0) ? instance.player1Mapping[button] : instance.player2Mapping[button];
        if (control.isAxis)
        {
            if (control.isPositive && control.currentValue < instance.threshold && control.previousValue >= instance.threshold)
            {
                return true;
            }
            else if (!control.isPositive && control.currentValue > -instance.threshold && control.previousValue <= -instance.threshold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return Input.GetKeyUp(control.keycode);
        }
    }

    private void Update()
    {
        foreach (Control c in player1ControlsArray)
        {
            if (c.isAxis)
            {
                c.previousValue = c.currentValue;
                c.currentValue = Input.GetAxisRaw(c.axis);
            }
        }
    }
}
