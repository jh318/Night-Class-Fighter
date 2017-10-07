using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour {

    [Header("Selected Buttons")]
    public Selectable currentSelection;
    public Selectable defaultSelection;
    Selectable nextSelection;

    [Header("Indicator")]
    public Transform indicator;
    public Animator indicatorAnim;

    [Header("Scene")]
    public string scene;

    Vector2 nav;
    Vector2 preNav;

    bool buttonPressed = false;

    SceneTransitionTest STT;

	void Start ()
    {
        nextSelection = defaultSelection;
        STT = GetComponent<SceneTransitionTest>();
	}
	void Update ()
    {
        float x = Input.GetAxisRaw("joystick 1 axis 1");
        float x2 = Input.GetAxisRaw("joystick 2 axis 1");
        float y = Input.GetAxisRaw("joystick 1 axis 2");
        float y2 = Input.GetAxis("joystick 2 axis 2");
        preNav = nav;
        nav = new Vector2(x, y);
        if ((nav.magnitude > 0.5 && preNav.magnitude <= 0.5))
        {
            nextSelection = currentSelection.FindSelectable(nav);
        }
        

        if (nextSelection != currentSelection && nextSelection != null)
        {
            currentSelection = nextSelection;
            Debug.Log("Moved To The P1 Spot.");
        }

        if (nextSelection != null)
        {
            indicator.transform.position = nextSelection.transform.position;
        }

        if (ControlMapper.GetButton(0, GameButton.LightAttack) && !buttonPressed)
        {
            ScriptableObjectHolder info = currentSelection.GetComponent<ScriptableObjectHolder>();
            scene = info.sceneInfo.scene;
            buttonPressed = true;

            if (scene == "Quit")
            {
                SceneDirector.instance.Quit();
            }
            else
            {
                SceneDirector.instance.ChangeScene(scene); // put scene in parenthesis;
            }
        }


    }
}
