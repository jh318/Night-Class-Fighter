using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharSelectController : MonoBehaviour {

    [Header("Mug Shots")]
    public RawImage p1MugShot;
    public RawImage p2MugShot;

    [Header("Character Names")]
    public Text p1CharName;
    public Text p2CharName;

    [Header("Ready Indicators")]
    public GameObject p1ReadyUI;
    public GameObject p2ReadyUI;
    public bool p1Ready = false;
    public bool p2Ready = false;
    public bool toFightScene = false;

    [Header("Selected Button")]
    public Selectable p1;
    public Selectable p2;
    public Selectable defaultButton;

    Selectable newSelection1;
    Selectable newSelection2;
    Selectable prevSelect1;
    Selectable prevSelect2;

    [Header("Player Selection Indicators")]
    public Transform p1Indicator;
    public Transform p2Indicator;

    [Header("Indicator Animators")]
    public Animator p1Anim;
    public Animator p2Anim;

    Vector2 p1Dir;
    Vector2 preP1Dir;
    Vector2 p2Dir;
    Vector2 preP2Dir;

    SceneTransitionTest test;


    // To-Do

    // Selection(Transfer to ready state after selecting a character);
    // Make sound when moving or selecting(Play animations);
    // Make a previous newSelection to allow for sounds to play during selection changes;
    // Select Character, change sprite for selection, gray out selection, transfer to ready(give option to go back with (B)), and load character data;

    void Start ()
    {
        p1 = defaultButton;
        p2 = defaultButton;
        newSelection1 = defaultButton;
        newSelection2 = defaultButton;

        test = GetComponent<SceneTransitionTest>();

    }

	void Update ()
    {
        if (!toFightScene)
        {
            if (!p1Ready)
            {
                // Get Input p1
                float p1Hori = Input.GetAxis("joystick 1 axis 1");
                float p1Vert = Input.GetAxis("joystick 1 axis 2");
                preP1Dir = p1Dir;
                p1Dir = new Vector2(p1Hori, p1Vert);

                if (p1Dir.magnitude > 0.5 && preP1Dir.magnitude <= 0.5)
                {
                    newSelection1 = p1.FindSelectable(p1Dir);
                }               

                if (newSelection1 != p1 && newSelection1 != null)
                {
                    p1 = newSelection1;
                    Debug.Log("Moved To The P1 Spot.");
                }

                if (newSelection1 != null)
                {
                    p1Indicator.transform.position = newSelection1.transform.position;
                }

                CheckSelectionChange();
                // Below is the A Button Input;
                if (ControlMapper.GetButton(0, GameButton.LightAttack) && !p1Ready)
                {
                    p1ReadyUI.gameObject.SetActive(true);
                    // Gray out p2Indicator;
                    CharacterInfo p1Info = p1.GetComponent<CharacterInfo>();
                    p1CharName.text = p1Info.characterInfo.characterName;
                    p1MugShot.texture = p1Info.characterInfo.mugShot.texture;
                    Debug.Log("I'm Selecting YO!");
                    p1Ready = true;
                }
            }        // Below is the B Button Input;
            else if (ControlMapper.GetButton(0, GameButton.MediumAttack) && p1Ready)
            {
                p1Ready = false;
                p1ReadyUI.gameObject.SetActive(false);
                // Color p1Indicator;
                Debug.Log("I do exist!");
            }

            //---------------------------------------------------------------------------------

            if (!p2Ready)
            {
                // Get Input p2
                float p2Hori = Input.GetAxisRaw("joystick 2 axis 1");
                float p2Vert = Input.GetAxisRaw("joystick 2 axis 2");
                preP2Dir = p2Dir;
                p2Dir = new Vector2(p2Hori, p2Vert);

                if (p2Dir.magnitude > 0.5 && preP2Dir.magnitude <= 0.5)
                {
                    newSelection2 = p2.FindSelectable(p2Dir);
                }

                if (newSelection2 != p2 && newSelection2 != null)
                {
                    p2 = newSelection2;
                    Debug.Log("Moved To The P2 Spot.");
                }

                if (newSelection2 != null)
                {
                    p2Indicator.transform.position = newSelection2.transform.position;
                }

                CheckSelectionChange();
                // Below is the A Button Input;
                if (ControlMapper.GetButton(1, GameButton.LightAttack) && !p2Ready)
                {
                    p2ReadyUI.gameObject.SetActive(true);
                    // Gray out p2Indicator;
                    CharacterInfo p2Info = p2.GetComponent<CharacterInfo>();
                    p2CharName.text = p2Info.characterInfo.characterName;                   
                    p2MugShot.texture = p2Info.characterInfo.mugShot.texture;
                    p2Ready = true;
                }
            }        // Below is the B Button Input;
            else if (ControlMapper.GetButton(0, GameButton.MediumAttack) && p2Ready)
            {
                p2Ready = false;
                p2ReadyUI.gameObject.SetActive(false);
                // Color p2Indicator;
            }
            TransitionCheck();
        }
        else
        {
            SceneDirector.instance.FightScene();
        }
        

    }

    //---------------------------------------------------------------------------------

    void CheckSelectionChange()
    {
        if (prevSelect1 != newSelection1 && prevSelect1 != null)
        {
            prevSelect1 = newSelection1;
            // Change sound and Animation
        }

        if (prevSelect2 != newSelection2 && prevSelect2 != null)
        {
            prevSelect2 = newSelection2;
            // Change sound and Animation
        }
    }

    void TransitionCheck()
    {
        if (p1Ready && p2Ready)
        {
            toFightScene = true;
        }
    }

}
