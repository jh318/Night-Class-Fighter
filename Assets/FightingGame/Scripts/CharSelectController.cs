using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharSelectController : MonoBehaviour {

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

    public bool p1Ready = false;
    public bool p2Ready = false;

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
    }

	void Update ()
    {
        // Get Input p1
        float p1Hori = Input.GetAxisRaw("Horizontal");
        float p1Vert = Input.GetAxisRaw("Vertical");
        Vector2 p1Dir = new Vector2(p1Hori, p1Vert);
        Debug.Log(p1Dir);

        // Get Input p2
        float p2Hori = Input.GetAxisRaw("Horizontal2");
        float p2Vert = Input.GetAxisRaw("Vertical2");
        Vector2 p2Dir = new Vector2(p2Hori, p2Vert);

        // Initialize the Selectable and look for other selectables in the direction of the players' choice;
        newSelection1 = p1.FindSelectable(p1Dir);
        newSelection2 = p2.FindSelectable(p2Dir);

        //----------------------------------------------------------------------------------------

        if (newSelection1 != null)
        {
            p1Indicator.transform.position = newSelection1.transform.position;
        }

        if (newSelection2 != null)
        {
            p2Indicator.transform.position = newSelection2.transform.position;
        }

        //----------------------------------------------------------------------------------------

        if (newSelection1 != p1 && newSelection1 != null)
        {
            p1 = newSelection1;
            Debug.Log("Moved To The P1 Spot.");
        }

        if (newSelection2 != p2 && newSelection2 != null)
        {
            p2 = newSelection2;
            Debug.Log("Moved To The P2 Spot.");
        }

        //----------------------------------------------------------------------------------------

        CheckSelectionChange();

        //----------------------------------------------------------------------------------------

        if (Input.GetButton("AButton") && !p1Ready)
        {
            // p1Ready = true;
            newSelection1.Select();
            Debug.Log("I'm Selecting YO!");
        }
        //else if (Input.GetButtonDown("BackButton") && p1Ready)
        //{
        //    p1Ready = false;
        //}

        if (Input.GetButtonDown("AButton2") && !p1Ready)
        {
            p1Ready = true;

        }
        //else if (Input.GetButtonDown("BackButton2") && p1Ready)
        //{
        //    p1Ready = false;
        //}

    }

    void CheckSelectionChange()
    {
        if (prevSelect1 != newSelection1 && prevSelect1 != null)
        {
            prevSelect1 = newSelection1;
            // Change music and Animation
        }

        if (prevSelect2 != newSelection2 && prevSelect2 != null)
        {
            prevSelect2 = newSelection2;
            // Change music and Animation
        }
    }

}
