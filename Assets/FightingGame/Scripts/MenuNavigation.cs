using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour {

    [Header("UI Groups")]
    public GameObject menuGroup;
    public GameObject optionsGroup;
    public GameObject creditsGroup;

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
    bool waiting = false;
    bool inSubMenu = false;

    SceneTransitionTest STT;

    ScriptableObjectHolder info;


    void Start ()
    {
        nextSelection = defaultSelection;
        STT = GetComponent<SceneTransitionTest>();
    }
	void Update ()
    {
        info = currentSelection.GetComponent<ScriptableObjectHolder>();

        float x = Input.GetAxisRaw("joystick 1 axis 1");
        //float x2 = Input.GetAxisRaw("joystick 2 axis 1");
        float y = Input.GetAxisRaw("joystick 1 axis 2");
        //float y2 = Input.GetAxis("joystick 2 axis 2");
        preNav = nav;
        nav = new Vector2(x, y);
        
        if ((nav.magnitude > 0.5 && preNav.magnitude <= 0.5))
        {
            nextSelection = currentSelection.FindSelectable(nav);
        }
        

        if (nextSelection != currentSelection && nextSelection != null)
        {
            currentSelection = nextSelection;
            //Debug.Log("Moved To The P1 Spot.");
        }

        if (nextSelection != null)
        {
            indicator.transform.position = nextSelection.transform.position;
        }

        if (ControlMapper.GetButton(0, GameButton.LightAttack) && !buttonPressed)
        {
            scene = info.sceneInfo.scene;
            buttonPressed = true;

            if (scene == "Quit")
            {
                SceneDirector.instance.Quit();
                Debug.Log("Quit");
            }
            else if (scene == "Options")
            {
                inSubMenu = true;
                menuGroup.gameObject.SetActive(false);
                optionsGroup.gameObject.SetActive(true);
                // Set the Selectable to the top selection;
                Debug.Log("Got in!");
            }
            else if (scene == "Credits")
            {
                inSubMenu = true;
                menuGroup.gameObject.SetActive(false);
                creditsGroup.gameObject.SetActive(true);
                Debug.Log("I got in too");
            }
            else if (!inSubMenu && !waiting)
            {
                SceneDirector.instance.ChangeScene(scene); // put scene in parenthesis;
            }
            else
            {
                Debug.Log("Error");
            }
        }
        else if (ControlMapper.GetButton(0, GameButton.MediumAttack))
        {
            if (!buttonPressed && !inSubMenu && !waiting)
            {
                StartCoroutine("WaitTime");
                scene = info.sceneInfo.previousScene;
                SceneDirector.instance.ChangeScene(scene);
                buttonPressed = true;
            }
            else
            {
                StartCoroutine("WaitTime");
                optionsGroup.gameObject.SetActive(false);
                creditsGroup.gameObject.SetActive(false);
                menuGroup.gameObject.SetActive(true);
                inSubMenu = false;
                buttonPressed = false;
            }
            
        }
    }
    IEnumerator WaitTime()
    {
        waiting = true;
        yield return new WaitForSeconds(1);
        waiting = false;
    }
}
