using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReMapNavigationController : MonoBehaviour {
    public static ReMapNavigationController instance;

    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public Selectable p1Selectable;
    public Selectable p2Selectable;
	public Selectable defaultSelectable;

    Selectable prevSelectable1;
    Selectable prevSelectable2;
    Selectable nextSelectable1;
    Selectable nextSelectable2;

	public static bool p1selected = false;
	public static bool p2selected = false;

    public bool navigate = true;
    bool waiting = false;
    bool exitPressed = false;

    Color defaultColor; 

    ScriptableObjectHolder p1Info;
    ScriptableObjectHolder p2Info;
    
    void Start () 
    {
        defaultColor = p1Selectable.image.color;
        p1Selectable.image.color = Color.red;
        p2Selectable.image.color = Color.blue;		
	}
	
	void Update () 
    {

        p1Info = p1Selectable.GetComponent<ScriptableObjectHolder>();
        p2Info = p2Selectable.GetComponent<ScriptableObjectHolder>();

        int buttonNumber1 = p1Info.buttonInfo.buttonNumber;
        int controllerNumber1 = p1Info.buttonInfo.controlNumber;

        int buttonNumber2 = p2Info.buttonInfo.buttonNumber;
        int controllerNumber2 = p2Info.buttonInfo.controlNumber;

        if (!exitPressed)
        {   if (!p1selected)
		    {
			    if (ControlMapper.GetButtonDown (0, GameButton.Down) && navigate) 
			    {
                    Debug.Log("Down Pressed");
				    nextSelectable1 = p1Selectable.FindSelectableOnDown();
                    if(nextSelectable1 != null)
                    {
                        prevSelectable1 = p1Selectable;
                        p1Selectable = nextSelectable1;

                        prevSelectable1.image.color = defaultColor;
                        p1Selectable.image.color = Color.red;
                        
                    }
			    }

                if (ControlMapper.GetButtonDown(0, GameButton.Up) && navigate)
                {
                    Debug.Log("Up Pressed");
                    nextSelectable1 = p1Selectable.FindSelectableOnUp();
                    if (nextSelectable1 != null)
                    {
                        prevSelectable1 = p1Selectable;
                        p1Selectable = nextSelectable1;

                        prevSelectable1.image.color = defaultColor;
                        p1Selectable.image.color = Color.red;
                        
                    }
                }

                if (Input.GetButton("P1Select"))
                {
                    Debug.Log("Hey buddy!");
                    ButtonRemapUIController.instance.ButtonSelected(buttonNumber1);
                    ButtonRemapUIController.instance.PlayerNumberSelected(controllerNumber1);    
                    StartCoroutine("WaitTime");               
                }
                else if (Input.GetButton("P1Back") && !waiting)
                {
                    exitPressed = true;
                }
            }

            if (!p2selected)
            {
                if (ControlMapper.GetButtonDown(1, GameButton.Down) && navigate)
                {
                    Debug.Log("Down Pressed");
                    nextSelectable2 = p2Selectable.FindSelectableOnDown();
                    if (nextSelectable2 != null)
                    {
                        prevSelectable2 = p2Selectable;
                        p2Selectable = nextSelectable2;

                        prevSelectable2.image.color = defaultColor;
                        p2Selectable.image.color = Color.blue;

                    }
                }

                if (ControlMapper.GetButtonDown(1, GameButton.Up) && navigate)
                {
                    Debug.Log("Up Pressed");
                    nextSelectable2 = p2Selectable.FindSelectableOnUp();
                    if (nextSelectable2 != null)
                    {
                        prevSelectable2 = p2Selectable;
                        p2Selectable = nextSelectable2;

                        prevSelectable2.image.color = defaultColor;
                        p2Selectable.image.color = Color.blue;

                    }
                }

                if (Input.GetButtonDown("P2Select"))
                {
                    ButtonRemapUIController.instance.ButtonSelected(buttonNumber2);
                    ButtonRemapUIController.instance.PlayerNumberSelected(controllerNumber2);
                    StartCoroutine("WaitTime");
                }
                else if (Input.GetButton("P2Back") && !waiting)
                {
                    exitPressed = true;
                }
            }
        }
        else 
        {
            SceneManager.LoadScene("Main Menu");
        }
	}
    IEnumerator WaitTime()
    {
        waiting = true;
        yield return new WaitForSeconds(1);
        waiting = false;
    }
}
