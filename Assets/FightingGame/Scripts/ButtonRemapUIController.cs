﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class ControlMap
{
    public Button uiButtonName;

    

    [System.NonSerialized] public Control control;

    [System.NonSerialized] public Text buttonText;

    public Image controlButtonSprite;

}

public class ButtonRemapUIController : MonoBehaviour
{
    public ControlMap[] cm;
    public ControlMap[] cmP2;

    public EventSystem eventSystem;

    string[] gameButtonName = System.Enum.GetNames(typeof(GameButton));

    float waitTime = 0;
    int playerControllernumber;


    private void Start()
    {
        for (int i = 0; i < cm.Length; i++)
        {
            cm[i].buttonText = cm[i].uiButtonName.GetComponentInChildren<Text>();
           

            cmP2[i].buttonText = cmP2[i].uiButtonName.GetComponentInChildren<Text>();
            

            //cm[i].controlButtonSprite = cm[i].uiButtonName.GetComponentInChildren<Image>();
        }



        for (int i = 0; i < cm.Length; i++)
        {

            //set p1 stuff
            cm[i].buttonText.text = gameButtonName[i];
            Debug.Log("Set Player 1 Button #" + i + "'s Text");
            cm[i].control = ControlMapper.player1Mapping[(GameButton)i];
            
            if (PictureToButtonMapper.c1ButtonMap.ContainsKey(cm[i].control.keycode))
                cm[i].controlButtonSprite.sprite = PictureToButtonMapper.c1ButtonMap[cm[i].control.keycode];

            //set p2 stuff
            cmP2[i].buttonText.text = gameButtonName[i];
            Debug.Log("Set Player 2 Button #" + i + "'s Text");
            cmP2[i].control = ControlMapper.player2Mapping[(GameButton)i];
            
            if (PictureToButtonMapper.c2ButtonMap.ContainsKey(cmP2[i].control.keycode))
                cmP2[i].controlButtonSprite.sprite = PictureToButtonMapper.c2ButtonMap[cmP2[i].control.keycode];


        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!newButtonSelected)
        {
            eventSystem.sendNavigationEvents = false;
            //EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
            int controlMapIndex = globalControlMapIndex;

            waitTime += Time.deltaTime;

            KeyCode newButtonpressedKeycode = KeyCode.None;
            if(!newButtonSelected && waitTime > 1)
            {
                
                
                    for (int j = 0; j < 20; j++)
                    {
                        if (Input.GetKeyDown("joystick " + playerControllernumber + " button " + j))
                        {
                            Debug.Log("joystick " + playerControllernumber + " button " + j + " pressed. bro");
                            string buttonPressed = "Joystick" + playerControllernumber + "Button" + j;
                            KeyCode buttonPressedKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), buttonPressed);
                            newButtonpressedKeycode = buttonPressedKeycode;
                            
                            newButtonSelected = true;
                            waitTime = 0;
                        }




                    }

                    //for (int j = 1; j < 11; j++)
                    //{
                    //    if (j == 8) continue;
                    //    if (Input.GetAxis("joystick " + i + " axis " + j) > 0.9f || Input.GetAxis("joystick " + i + " axis " + j) < -0.9f)
                    //    {
                    //        Debug.Log("joystick " + i + " axis " + j + " moved");
                    //        string Pressed = "Joystick " + i + " button " + j;
                    //        KeyCode buttonPressedKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), buttonPressed);
                    //        newButtonSelected = true;
                    //    }

                

                
            }


            if (newButtonpressedKeycode != KeyCode.None)
            {
                if (playerControllernumber == 1)
                {
                    cm[controlMapIndex].control.keycode = newButtonpressedKeycode;
                    cm[controlMapIndex].controlButtonSprite.sprite = PictureToButtonMapper.c1ButtonMap[cm[controlMapIndex].control.keycode];
                    eventSystem.sendNavigationEvents = true;
                    ReMapNavigationController.p1selected = false;
                    globalControlMapIndex = -1;
                }
                else if(playerControllernumber == 2)
                {
                    //do the remap stuff for player 2
                    cmP2[controlMapIndex].control.keycode = newButtonpressedKeycode;
                    cmP2[controlMapIndex].controlButtonSprite.sprite = PictureToButtonMapper.c2ButtonMap[cmP2[controlMapIndex].control.keycode];
                    eventSystem.sendNavigationEvents = true;
                    ReMapNavigationController.p2selected = false;
                    globalControlMapIndex = -1;
                }
                else
                {
                    Debug.Log("The Player number is invalid! FIX IT!!");
                }
            }
        }

        if (!newAxisSelected)
        {
            eventSystem.sendNavigationEvents = false;
            
            int controlMapIndex = globalControlMapIndex;

            waitTime += Time.deltaTime;
            float axisValue = 0;
            string axisMoved = "";

            if (!newAxisSelected && waitTime > 1)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (j == 8) continue;
                    if (Input.GetAxis("joystick " + playerControllernumber + " axis " + j) > 0.9f || Input.GetAxis("joystick " + playerControllernumber + " axis " + j) < -0.9f)
                    {
                        float value = Input.GetAxis("joystick " + playerControllernumber + " axis " + j);
                        Debug.Log("joystick " + playerControllernumber + " axis " + j + " moved. Value:" + value );
                        string moved = "joystick " + playerControllernumber + " axis " + j;
                        axisValue = value;
                        axisMoved = moved;
                        newAxisSelected = true;
                    }
                }
            }

            if (axisMoved != "")
            {
                if (playerControllernumber == 1)
                {
                    Debug.Log(cm[controlMapIndex]);
                    cm[controlMapIndex].control.axis = axisMoved;
                    if (axisValue > 0)
                    {
                        cm[controlMapIndex].control.isPositive = true;
                    }
                    else
                    {
                        cm[controlMapIndex].control.isPositive = false;
                    }
                    eventSystem.sendNavigationEvents = true;
                    ReMapNavigationController.p1selected = false;
                    globalControlMapIndex = -1;
                }
                else if(playerControllernumber == 2)
                {
                    Debug.Log(cmP2[controlMapIndex]);
                    cmP2[controlMapIndex].control.axis = axisMoved;
                    if (axisValue > 0)
                    {
                        cmP2[controlMapIndex].control.isPositive = true;
                    }
                    else
                    {
                        cmP2[controlMapIndex].control.isPositive = false;
                    }
                    eventSystem.sendNavigationEvents = true;
                    ReMapNavigationController.p2selected = false;
                    globalControlMapIndex = -1;
                }
            }
        }

    }

    bool newButtonSelected = true;
    int globalControlMapIndex = -1;

    public void ButtonSelected(int controlMapIndex)
    {
        newButtonSelected = false;
        globalControlMapIndex = controlMapIndex;
        

        
        
    }

    public void PlayerNumberSelected(int playernumber)
    {
        playerControllernumber = playernumber;
    }

    bool newAxisSelected = true;

    public void AxisSelected(int controlMapIndex)
    {
        newAxisSelected = false;
        globalControlMapIndex = controlMapIndex;
        Debug.Log("globalControlMapIndex = " + globalControlMapIndex);
        
    }
}


