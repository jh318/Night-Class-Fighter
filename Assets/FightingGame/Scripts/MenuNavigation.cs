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


    SceneTransitionTest STT;

	void Start ()
    {
        nextSelection = defaultSelection;
        STT = GetComponent<SceneTransitionTest>();
	}
	void Update ()
    {
        //preV = v;
        //v = input;
        //if(v.mag > 0.5 && preV.mag <= 0.5) {Go To Next};

        float x = Input.GetAxisRaw("joystick 1 axis 1");
        float x2 = Input.GetAxisRaw("joystick 2 axis 1");
        float y = Input.GetAxisRaw("joystick 1 axis 2");
        float y2 = Input.GetAxis("joystick 2 axis 2");
        Vector2 nav = new Vector2(x, y);
        nextSelection = currentSelection.FindSelectable(nav);

        if (nextSelection != currentSelection && nextSelection != null)
        {
            currentSelection = nextSelection;
            Debug.Log("Moved To The P1 Spot.");
        }

        if (nextSelection != null)
        {
            indicator.transform.position = nextSelection.transform.position;
        }

        if (ControlMapper.GetButton(0, GameButton.LightAttack))
        {
            // scene = ButtonInfo.scene;
            SceneDirector.instance.CharacterSelect(); // put scene in parenthesis;
        }


    }
}
