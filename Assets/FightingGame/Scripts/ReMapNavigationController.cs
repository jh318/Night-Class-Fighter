using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReMapNavigationController : MonoBehaviour {

    public Selectable p1Selectable;
    public Selectable p2Selectable;
	public Selectable defaultSelectable;

    Selectable prevSelectable1;
    Selectable prevSelectable2;
    Selectable nextSelectable1;
    Selectable nextSelectable2;

	public static bool p1selected = false;
	public static bool p2selected = false;

    bool exitPressed = false;

    Color defaultColor; 


    
    // Use this for initialization
    void Start () 
    {
        defaultColor = p1Selectable.image.color;
        p1Selectable.image.color = Color.red;
        p2Selectable.image.color = Color.blue;		
	}
	
	// Update is called once per frame
	void Update () {
        if (!exitPressed)
        {   if (!p1selected)
		    {
			    if (ControlMapper.GetButtonDown (0, GameButton.Down)) 
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
                if (ControlMapper.GetButtonDown(0, GameButton.Up))
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
            }

            if (!p2selected)
            {
                if (ControlMapper.GetButtonDown(1, GameButton.Down))
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
                if (ControlMapper.GetButtonDown(1, GameButton.Up))
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
            }

        }
        

	}
}
