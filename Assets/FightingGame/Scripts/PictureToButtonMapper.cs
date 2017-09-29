using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ControllerButtons
{
	public KeyCode buttonName;
	public Sprite buttonPic;
}


public class PictureToButtonMapper : MonoBehaviour {

	public ControllerButtons[] controller1Buttons;
	public ControllerButtons[] controller2Buttons;

    
	public static Dictionary<KeyCode,Sprite> c1ButtonMap = new Dictionary<KeyCode, Sprite>();
	public static Dictionary<KeyCode,Sprite> c2ButtonMap = new Dictionary<KeyCode, Sprite>();

	public static PictureToButtonMapper instance;

	void Awake()
	{
		if(instance == null)
        {
            instance = this;

            foreach(ControllerButtons cb in controller1Buttons)
            {
                c1ButtonMap[cb.buttonName] = cb.buttonPic;
            }

            foreach (ControllerButtons cb in controller2Buttons)
            {
                c2ButtonMap[cb.buttonName] = cb.buttonPic;
            }
        }
	}


    
	
}
